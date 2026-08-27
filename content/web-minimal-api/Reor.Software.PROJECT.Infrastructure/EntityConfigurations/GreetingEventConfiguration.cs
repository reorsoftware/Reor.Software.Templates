using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reor.Software.PROJECT.Domain.Entities;
using Reor.Software.PROJECT.Infrastructure.Entities;

namespace Reor.Software.PROJECT.Infrastructure.EntityConfigurations;

public class GreetingEventConfiguration : IEntityTypeConfiguration<GreetingEvent>
{
    public void Configure(EntityTypeBuilder<GreetingEvent> builder)
    {
        builder.ToTable("greeting_event");
    }
}