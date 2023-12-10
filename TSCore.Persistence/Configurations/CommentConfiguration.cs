using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TSCore.Domain.Tables;

namespace TSCore.Persistence.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("Comments", "TS");

        builder.Property(e => e.Content)
            .HasMaxLength(2048)
            .IsRequired();

        builder.Property(e => e.UpdatedBy)
            .HasMaxLength(255)
            .IsRequired(false);
        builder.Property(e => e.UpdatedAt)
            .IsRequired(false);
        
        builder.HasOne(e => e.Ticket)
            .WithMany(e => e.TicketComments)
            .HasForeignKey(e => e.TicketId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.Sender)
            .WithMany(e => e.UserComments)
            .HasForeignKey(e => e.SenderId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}