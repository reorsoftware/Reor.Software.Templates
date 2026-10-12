using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PROJECT_NS.PROJECT.Domain.Entities;
using PROJECT_NS.PROJECT.Infrastructure.Entities;

namespace PROJECT_NS.PROJECT.Infrastructure.EntityConfigurations;

public class GreetingEventConfiguration : IEntityTypeConfiguration<GreetingEvent>
{
    public void Configure(EntityTypeBuilder<GreetingEvent> builder)
    {
        builder.ToTable("greeting_event");
    }
}