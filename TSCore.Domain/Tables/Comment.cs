using TSCore.Domain.Common.Classes;

namespace TSCore.Domain.Tables;

public class Comment : BaseAuditableEntity
{
    public string Content { get; set; }
    
    public int SenderId { get; set; }
    public User Sender { get; set; }
    
    public int TicketId { get; set; }
    public Ticket Ticket { get; set; }
}