using Domain.Entities.Accounts;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.ResultExtension;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services.Services.Token;
public class TokenService(ApplicationDbContext context)
{
    public async Task<Result<TokenItem>> GenerateToken(SqlAccount account, string role)
    {
        try
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(ConfigKey.JWT_KEY);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                        new Claim("id", account.Id.ToString()),
                        new Claim("role", role),
                    }),
                Issuer = ConfigKey.VALID_ISSUER,
                Audience = ConfigKey.VALID_AUDIENCE,
                Expires = ConfigKey.getATExpiredTime(),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey
                    (secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = jwtTokenHandler.CreateToken(tokenDescription);
            string accessToken = jwtTokenHandler.WriteToken(token);
            string refreshToken = GenerateRefreshToken();

            Result<SqlToken> createTokenResult = await CreateToken(accessToken, refreshToken, account.Id);

            return createTokenResult.IsSuccess ?
                Result.Success(new TokenItem { accessToken = accessToken, refreshToken = refreshToken }) :
                Result.Failure<TokenItem>(createTokenResult.Error);

        }
        catch (Exception ex)
        {
            return Result.Failure<TokenItem>(TokenError.GenerateFail);
        }
    }

    public TokenDecodedInfo DecodeToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(ConfigKey.JWT_KEY);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = ConfigKey.VALID_AUDIENCE,
                ValidIssuer = ConfigKey.VALID_ISSUER,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = true
            };

            SecurityToken securityToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            TokenDecodedInfo tokenInfo = new()
            {
                id = int.Parse(principal.FindFirst("id")?.Value ?? "-1"),
                //id = principal.FindFirst("id")?.Value,
                role = principal.FindFirst("roleId")?.Value ?? "",
            };

            return tokenInfo;
        }
        catch (Exception ex)
        {
            //Log.Error($"func: DecodeToken -> with token: {token} -> failed , Exception: {ex.InnerException}");
            return new TokenDecodedInfo();
        }
    }

    #region helper function
    private string GenerateRefreshToken()
    {
        var random = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(random);
            return Convert.ToBase64String(random);
        }
    }
    public async Task<Result<SqlToken>> CreateToken(string accessToken, string refreshToken, int accountId)
    {
        try
        {
            SqlAccount? account = context.Accounts.Where(s => s.Id.CompareTo(accountId) == 0)
                                    .Include(s => s.Tokens)
                                    .FirstOrDefault();
            if (account is null)
            {
                return Result.Failure<SqlToken>(TokenError.NotFound(accountId));
            }
            SqlToken? existingToken = context.Tokens.Where(s => s.accessToken.CompareTo(accessToken) == 0 &&
                                               s.refreshToken.CompareTo(refreshToken) == 0 &&
                                               s.isExpired == false)
                                   .FirstOrDefault();
            if (existingToken is not null)
            {
                return Result.Failure<SqlToken>(TokenError.TokenNotUnique);
            }

            SqlToken token = new()
            {
                accessToken = accessToken,
                refreshToken = refreshToken,
                createTime = DateTime.UtcNow,
                expiredTime = ConfigKey.getRTExpiredTime(),
                isExpired = false
            };

            context.Tokens.Add(token);

            await context.SaveChangesAsync();

            return Result.Success(token);
        }
        catch (Exception ex)
        {
            return Result.Failure<SqlToken>(TokenError.CreateFail);
        }
    }
    #endregion

}
