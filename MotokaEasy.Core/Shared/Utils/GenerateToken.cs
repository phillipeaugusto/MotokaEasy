using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MotokaEasy.Core.Infrastructure.Token;
using static System.String;

namespace MotokaEasy.Core.Shared.Utils;

public static class TokenService
{
    private static string GenerateTokenAplicacaoAcesso(Guid id, string role, string tokenRoot)
    {
        if (id == Guid.Empty || role == Empty)
            return Empty;
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Actor, id.ToString()),
                new(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenRoot)), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static TokenData ReturnTokenData(Guid id, string role, string token)
    {
        if (id == Guid.Empty || role == Empty)
            return new TokenData(Empty, Empty);
        
        return new TokenData(GenerateTokenAplicacaoAcesso(id, role, token), GenerateRefreshToken());
    }
    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}