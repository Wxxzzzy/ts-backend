using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using TSCore.Application.Common.Interfaces;

namespace TSCore.Infrastructure.Services.Classes;

//TODO: move
public class AuthService : IAuthService
{
    public bool Verify(string password, string hash)
    {
        var createdHash = CreateHash(password);
        return createdHash == hash;
    }

    public string CreateHash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(128 / 8);
        var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8
        ));

        return hash;
    }
}