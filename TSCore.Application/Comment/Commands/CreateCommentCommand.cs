using MediatR;
using TSCore.Application.Common.Interfaces;

namespace TSCore.Application.Comment.Commands;

public class CreateCommentCommand : IRequest
{
    public int TicketId { get; set; }
    public string Content { get; set; } 
}

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand>
{
    private readonly ITeamSyncDbContext _context;
    private readonly ITokenService _tokenService;

    public CreateCommentCommandHandler(ITeamSyncDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }
    
    public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var senderId = _tokenService.GetUserId();
        var comment = new Domain.Tables.Comment
        {
            SenderId = senderId,
            Content = request.Content,
            TicketId = request.TicketId,
            CreatedAt = DateTime.Now,
            CreatedBy = "system",
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync(cancellationToken);
    }
}