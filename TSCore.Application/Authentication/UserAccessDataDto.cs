using AutoMapper;
using TSCore.Application.Common.Mapping;
using TSCore.Domain.Enums.Authentication;
using TSCore.Domain.Tables;

namespace TSCore.Application.Authentication;

public class UserAccessDataDto : IMapObject<User>
{
    public int Id { get; set; }
    public string Username { get; set; }
    public int UserRoleId { get; set; }
    public DateTime? Exp { get; set; }
    
    public string Token { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserAccessDataDto>()
            .ForMember(x => x.UserRoleId, _ => _.MapFrom(x => x.RoleId));
    }
}