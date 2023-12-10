using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using TSCore.Application.Common.Interfaces;

namespace TSCore.Infrastructure.Services.Classes;

public class AuthService : IAuthService
{
    public bool Verify(string password, string hash, byte[] salt = null)
    {
        salt ??= RandomNumberGenerator.GetBytes(128 / 8); // get salt from user or generate new

        var createdHash = CreateHash(password, salt);
        return createdHash.Equals(hash);
    }

    public string CreateHash(string password, byte[] salt)
    {
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