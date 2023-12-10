using AutoMapper;
using TSCore.Application.Common.Mapping;

namespace TSCore.Application.Ticket;

public class GetTicketQueryDto : IMapObject<Domain.Tables.Ticket>
{
    public int Id { get; set; }
    public string TicketTitle { get; set; }
    public string ShortDescription { get; set; }
    public int TicketStatus { get; set; }
    
    public int TeamId { get; set; }
    public int TicketCreatorId { get; set; }
    public int? AssignedToId { get; set; }
    
    public string AssignedToName { get; set; }
    public string CreatorName { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Tables.Ticket, GetTicketQueryDto>()
            .ForMember(x => x.CreatorName, _ => _.MapFrom(x => x.TicketCreator.Username))
            .ForMember(x => x.AssignedToName, _ => _.MapFrom(x => x.AssignedTo.Username))
            ;
    }
}