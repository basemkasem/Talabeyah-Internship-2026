namespace Domain.Common;

public class JwtOptions
{
    public string Issuer { get; private set; }
    public string Audience { get; private set; }
    public string SigningKey { get; private set; }
    public int ExpireInMinutes { get; private set; }

    public JwtOptions(string issuer, string audience, string signingKey, int expireInMinutes)
    {
        Issuer = issuer;
        Audience = audience;
        SigningKey = signingKey;
        ExpireInMinutes = expireInMinutes;
    }
}