using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Exceptions;
using TSCore.Application.Common.Interfaces;

namespace TSCore.Application.Authentication.Commands;

public class LoginCommand : IRequest<UserAccessDataDto>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, UserAccessDataDto>
{
    private readonly ITeamSyncDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;

    public LoginCommandHandler(ITeamSyncDbContext context, IAuthService authService, IMapper mapper)
    {
        _context = context;
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<UserAccessDataDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);
        
        NotFoundException.ThrowIfNull(user);

        var salt = Convert.FromBase64String(user.Salt);
        var isVerified = _authService.Verify(request.Password, user.PasswordHash, salt);

        if (!isVerified)
        {
            throw new NotAllowedException("Invalid password");
        }

        if (user.isBlocked)
        {
            throw new NotAllowedException("User is blocked");
        }

        var result = _mapper.Map<UserAccessDataDto>(user);
        return result;
    }
}