using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Interfaces;
using TSCore.Domain.Enums.Authentication;
using TSCore.Domain.Tables;

namespace TSCore.Application.Authentication.Commands;

public class SignUpCommand : IRequest<string>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    
    public SignUpCommand(string username, string password, string email)
    {
        Username = username;
        Password = password;
        Email = email;
    }
}

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, string>
{
    private readonly ITeamSyncDbContext _context;
    private readonly IAuthService _authService;
    
    public SignUpCommandHandler(ITeamSyncDbContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<string> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var userFromDb = await _context.Users
            .FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);

        //TODO: Custom exception
        if (userFromDb != null)
        {
            throw new BadHttpRequestException($"{nameof(User)} {request.Username} already exists");
        }

        var hash = _authService.CreateHash(request.Password);
        var user = new User
        {
            Username = request.Username,
            PasswordHash = hash,
            CreatedAt = DateTime.Now,
            RoleId = (int)ERoles.User,
        };

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return "token";
    }
}