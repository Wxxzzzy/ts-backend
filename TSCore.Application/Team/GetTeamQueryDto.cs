using AutoMapper;
using TSCore.Application.Common.Mapping;

namespace TSCore.Application.Team;

public class GetTeamQueryDto : IMapObject<Domain.Tables.Team>
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public int Owner { get; set; }
    public string OwnerName { get; set; }
    public List<string> TeamMembers { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Tables.Team, GetTeamQueryDto>()
            .ForMember(x => x.TeamMembers, _ => _.MapFrom(x => x.TeamMembers.Select(x => x.User.Username)))
            .ForMember(x => x.OwnerName, _ => _.MapFrom(x => x.User.Username))
            ;
    }
}