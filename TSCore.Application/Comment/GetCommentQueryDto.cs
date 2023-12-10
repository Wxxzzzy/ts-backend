using AutoMapper;
using TSCore.Application.Common.Mapping;

namespace TSCore.Application.Comment;

public class GetCommentQueryDto : IMapObject<Domain.Tables.Comment>
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int SenderId { get; set; }
    public string SenderUsername { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Tables.Comment, GetCommentQueryDto>()
            .ForMember(x => x.SenderUsername, _ => _.MapFrom(x => x.Sender.Username))
            ;
    }
}