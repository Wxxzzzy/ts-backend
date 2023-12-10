using TSCore.Domain.Common.Classes;

namespace TSCore.Domain.Tables;

public class Ticket : BaseAuditableEntity
{
    public string TicketTitle { get; set; }
    public string ShortDescription { get; set; }
    public int TicketStatus { get; set; }
    
    public int TeamId { get; set; }
    public Team Team { get; set; }
    
    public int TicketCreatorId { get; set; }
    public User TicketCreator { get; set; }
    
    public int? AssignedToId { get; set; }
    public User AssignedTo { get; set; }
    
    public ICollection<Comment> TicketComments { get; set; }
}