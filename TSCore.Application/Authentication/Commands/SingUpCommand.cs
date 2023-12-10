using System.Security.Cryptography;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Interfaces;
using TSCore.Domain.Enums;
using TSCore.Domain.Tables;

namespace TSCore.Application.Authentication.Commands;

public class SignUpCommand : IRequest<UserAccessDataDto>
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

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, UserAccessDataDto>
{
    private readonly ITeamSyncDbContext _context;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    
    public SignUpCommandHandler(ITeamSyncDbContext context, IAuthService authService, IMapper mapper)
    {
        _context = context;
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<UserAccessDataDto> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var userFromDb = await _context.Users
            .FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);

        if (userFromDb != null)
        {
            throw new BadHttpRequestException($"{nameof(User)} {request.Username} already exists");
        }

        var salt = RandomNumberGenerator.GetBytes(128 / 8);
        var hash = _authService.CreateHash(request.Password, salt);
        var user = new User
        {
            Username = request.Username,
            PasswordHash = hash,
            Salt = Convert.ToBase64String(salt),
            Email = request.Email,
            isBlocked = false,
            CreatedBy = request.Username,
            CreatedAt = DateTime.Now,
            RoleId = (int)ERoles.User,
        };

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<UserAccessDataDto>(user);
        return result;
    }
}