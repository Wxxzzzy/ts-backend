using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Exceptions;
using TSCore.Application.Common.Interfaces;

namespace TSCore.Application.Comment.Commands;

public class UpdateCommentCommand : IRequest
{
    public int CommentId { get; set; }
    public string Content { get; set; }
}

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
{
    private readonly ITeamSyncDbContext _context;

    public UpdateCommentCommandHandler(ITeamSyncDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments
            .Where(x => x.Id == request.CommentId)
            .FirstOrDefaultAsync(cancellationToken);
        
        NotFoundException.ThrowIfNull(comment);

        comment.Content = request.Content;

        await _context.SaveChangesAsync(cancellationToken);
    }
}