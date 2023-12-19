using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Interfaces;

namespace TSCore.Application.Authentication.Commands;

public class BlockOrUnblockUserCommand : IRequest
{
    public string Username { get; set; }
    public bool IsBlocked { get; set; }
}

public class BlockOrUnblockUserCommandHandler : IRequestHandler<BlockOrUnblockUserCommand>
{
    private readonly ITeamSyncDbContext _context;

    public BlockOrUnblockUserCommandHandler(ITeamSyncDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(BlockOrUnblockUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.Where(x => x.Username == request.Username).FirstAsync(cancellationToken);
        user.isBlocked = request.IsBlocked;
        await _context.SaveChangesAsync(cancellationToken);
    }
}