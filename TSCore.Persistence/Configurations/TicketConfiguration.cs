using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSCore.Domain.Tables;

namespace TSCore.Persistence.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("Tickets", "TS");

        builder.Property(e => e.TicketTitle)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(e => e.ShortDescription)
            .HasMaxLength(1024)
            .IsRequired(false);
        builder.Property(e => e.TicketStatus)
            .IsRequired();
        builder.Property(e => e.UpdatedBy)
            .IsRequired(false);
    }
}