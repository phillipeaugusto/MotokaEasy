namespace MotokaEasy.Core.Infrastructure.Token;

public class TokenData
{
    public TokenData(string token, string refreshToken)
    {
        Token = token;
        RefreshToken = refreshToken;
    }

    public string Token { get; set; }
    public string RefreshToken { get; set; }
}