# PROJECT

This project was initialised by the template `Reor.Software.Templates.MinApi`

# Structure
PROJECT is structured as follows:
```text
PROJECT_NS.PROJECT
├── PROJECT_NS.PROJECT.Domain
├── PROJECT_NS.PROJECT.Application
├── PROJECT_NS.PROJECT.Infrastructure
├── PROJECT_NS.PROJECT.Presentation
└── PROJECT_NS.PROJECT.WebHost
```

# Observability
OpenTelemetry traces, metrics and logs are enabled in `Program.cs` with `builder.UseReorOtel()`
(see `PROJECT_NS.PROJECT.WebHost/OpenTelemetry`). Everything is configured with the standard `OTEL_*`
environment variables, which can also be set in `appsettings.json`.

| Variable | Purpose |
|---|---|
| `OTEL_EXPORTER_OTLP_ENDPOINT` | Collector endpoint. OTLP export is switched on when this (or a signal-specific `OTEL_EXPORTER_OTLP_{TRACES,METRICS,LOGS}_ENDPOINT`) is set |
| `OTEL_EXPORTER_OTLP_PROTOCOL` | `grpc` (default) or `http/protobuf` |
| `OTEL_EXPORTER_OTLP_HEADERS` | e.g. `x-api-key=abc123` |
| `OTEL_SERVICE_NAME` | Defaults to `PROJECT_LOWER-api` |
| `OTEL_RESOURCE_ATTRIBUTES` | Extra resource attributes, e.g. `deployment.environment.name=staging` |
| `OTEL_TRACES_SAMPLER` / `OTEL_TRACES_SAMPLER_ARG` | e.g. `parentbased_traceidratio` / `0.1` |
| `OTEL_{TRACES,METRICS,LOGS}_EXPORTER` | Comma separated `otlp`, `console` or `none` |
| `OTEL_SDK_DISABLED` | `true` turns OpenTelemetry off entirely |

To print traces locally without a collector:
```bash
OTEL_TRACES_EXPORTER=console dotnet run --project PROJECT_NS.PROJECT.WebHost
```

Custom `ActivitySource`s and `Meter`s named `PROJECT_NS.PROJECT.*` are collected automatically.
