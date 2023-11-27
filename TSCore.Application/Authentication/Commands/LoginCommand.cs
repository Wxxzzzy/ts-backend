using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Exceptions;
using TSCore.Application.Common.Interfaces;

namespace TSCore.Application.Authentication.Commands;

public class LoginCommand : IRequest<string>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly ITeamSyncDbContext _context;
    private readonly IAuthService _authService;

    public LoginCommandHandler(ITeamSyncDbContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);

        NotFoundException.ThrowIfNull(user);
        
        var isVerified = _authService.Verify(request.Password, user.PasswordHash);
        return isVerified 
            ? "token"
            : string.Empty;
    }
}