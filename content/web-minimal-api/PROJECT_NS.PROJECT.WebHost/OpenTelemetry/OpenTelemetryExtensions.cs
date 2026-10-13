using System.Reflection;
using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace PROJECT_NS.PROJECT.WebHost.OpenTelemetry;

public static class OpenTelemetryExtensions
{
    private const string DEFAULT_SERVICE_NAME = "PROJECT_LOWER-api";

    // Wildcard so ActivitySources and Meters created in any layer of the solution are picked up.
    private const string SOLUTION_SOURCES = "PROJECT_NS.PROJECT.*";

    private static readonly string[] AdditionalMeters =
    [
        // Built-in .NET 10 meters not registered by the ASP.NET Core instrumentation package.
        "Microsoft.AspNetCore.Authentication",
        "Microsoft.AspNetCore.Authorization",
        "Microsoft.EntityFrameworkCore",
    ];

    private enum Signal { Traces, Metrics, Logs }

    [Flags]
    private enum Exporters
    {
        None = 0,
        Otlp = 1 << 0,
        Console = 1 << 1,
    }

    public static TBuilder UseReorOtel<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        var config = builder.Configuration;
        if (bool.TryParse(config["OTEL_SDK_DISABLED"], out var disabled) && disabled) return builder;

        var traceExporters = GetExporters(config, Signal.Traces);
        var metricExporters = GetExporters(config, Signal.Metrics);
        var logExporters = GetExporters(config, Signal.Logs);

        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddReorDefaults(builder.Environment))
            .WithTracing(tracing =>
            {
                tracing
                    .AddSource(SOLUTION_SOURCES)
                    .AddAspNetCoreInstrumentation(options => options.RecordException = true)
                    .AddHttpClientInstrumentation(options => options.RecordException = true)
                    .AddNpgsql();

                if (traceExporters.HasFlag(Exporters.Otlp)) tracing.AddOtlpExporter();
                if (traceExporters.HasFlag(Exporters.Console)) tracing.AddConsoleExporter();
            })
            .WithMetrics(metrics =>
            {
                metrics
                    .AddMeter(SOLUTION_SOURCES)
                    .AddMeter(AdditionalMeters)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddNpgsqlInstrumentation();

                if (metricExporters.HasFlag(Exporters.Otlp)) metrics.AddOtlpExporter();
                if (metricExporters.HasFlag(Exporters.Console)) metrics.AddConsoleExporter();
            })
            .WithLogging(
                logging =>
                {
                    if (logExporters.HasFlag(Exporters.Otlp)) logging.AddOtlpExporter();
                    if (logExporters.HasFlag(Exporters.Console)) logging.AddConsoleExporter();
                },
                options =>
                {
                    options.IncludeFormattedMessage = true;
                    options.IncludeScopes = true;
                });

        return builder;
    }

    /// <summary>
    /// Replaces the SDK default resource so our defaults are applied first and the environment variable
    /// detector (OTEL_SERVICE_NAME / OTEL_RESOURCE_ATTRIBUTES) runs last and always wins.
    /// </summary>
    private static ResourceBuilder AddReorDefaults(this ResourceBuilder resource, IHostEnvironment environment)
    {
        var assembly = Assembly.GetEntryAssembly();
        var version = assembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
                      ?? assembly?.GetName().Version?.ToString();

        return resource
            .Clear()
            .AddService(DEFAULT_SERVICE_NAME, serviceVersion: version)
            .AddAttributes([
                new KeyValuePair<string, object>("deployment.environment.name", environment.EnvironmentName.ToLowerInvariant()),
            ])
            .AddTelemetrySdk()
            .AddEnvironmentVariableDetector();
    }

    private static Exporters GetExporters(IConfiguration config, Signal signal)
    {
        var signalName = signal.ToString().ToUpperInvariant();
        var value = config[$"OTEL_{signalName}_EXPORTER"];

        if (string.IsNullOrWhiteSpace(value))
        {
            var otlpConfigured = !string.IsNullOrWhiteSpace(config["OTEL_EXPORTER_OTLP_ENDPOINT"])
                                 || !string.IsNullOrWhiteSpace(config[$"OTEL_EXPORTER_OTLP_{signalName}_ENDPOINT"]);
            return otlpConfigured ? Exporters.Otlp : Exporters.None;
        }

        var exporters = Exporters.None;
        foreach (var name in value.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
        {
            switch (name.ToLowerInvariant())
            {
                case "none":
                    return Exporters.None;
                case "otlp":
                    exporters |= Exporters.Otlp;
                    break;
                case "console":
                    exporters |= Exporters.Console;
                    break;
            }
        }

        return exporters;
    }
}
