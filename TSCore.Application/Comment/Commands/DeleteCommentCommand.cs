using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Exceptions;
using TSCore.Application.Common.Interfaces;

namespace TSCore.Application.Comment.Commands;

public class DeleteCommentCommand : IRequest
{
    public int CommentId { get; set; }
}

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
{
    private readonly ITeamSyncDbContext _context;

    public DeleteCommentCommandHandler(ITeamSyncDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments
            .Where(x => x.Id == request.CommentId)
            .FirstOrDefaultAsync(cancellationToken);

        NotFoundException.ThrowIfNull(comment);

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync(cancellationToken);
    }
}