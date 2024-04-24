using Domain.Entities.Accounts;
using Infrastructure.Data;
using Infrastructure.ResultExtension;
using Microsoft.EntityFrameworkCore;
using Services.Services.Email;
using Services.Services.Token;
using System.Security.Cryptography;
namespace Services.Services.Auth;
public class AuthService(
    ApplicationDbContext context,
    TokenService _token,
    EmailService _email)
{
    public async Task<Result<LoginDTO>> LoginAsync(string Email, string Password)
    {
        SqlAccount? user = await context.Accounts.Where(x => x.Email == Email).Include(u => u.Role).FirstOrDefaultAsync();

        if (user == null || Password.CompareTo(user.Password) != 0)
            return Result.Failure<LoginDTO>(AuthErrors.InvalidLogin);

        if (!user.IsVerified)
            return Result.Failure<LoginDTO>(AuthErrors.AccountYetVerified);

        Result<TokenItem> tokenResult = await _token.GenerateToken(user, user.Role.Name);

        return tokenResult.IsSuccess ?
           Result.Success(new LoginDTO
           {
               Id = user.Id,
               role = user.Role.Name,
               token = tokenResult.Value
           }) :
           Result.Failure<LoginDTO>(tokenResult.Error);
    }

    public async Task<Result<int>> RegisterAsync(string email, string password, string origin, string roleCode = "user") // add confirm pw
    {
        bool existAccount = context.Accounts.Any(s => s.Email.ToLower().CompareTo(email.ToLower()) == 0);

        if (existAccount) // should send email Already registered instead
        {
            return Result.Failure<int>(AuthErrors.EmailNotUnique);
        }
        var test = await context.Roles.ToListAsync();
        SqlRole? role = await context.Roles.FirstOrDefaultAsync(r => r.Code == roleCode);
        if (role == null)
        {
            return Result.Failure<int>(RoleErrors.NotFound(roleCode));
        }

        SqlAccount newAccount = new()
        {
            Email = email,
            Password = password,
            VerificationToken = GenerateVerificationToken(),
            Role = role
        };

        context.Accounts.Add(newAccount);
        int rowsAffected = await context.SaveChangesAsync();
        if (rowsAffected <= 0)
        {
            return Result.Failure<int>(AuthErrors.RegisterFail);
        }
        // send email to verify email
        //_ = Task.Run(() => SendVerificationEmail(newAccount, origin));
        await SendVerificationEmail(newAccount, origin);

        return Result.Success(newAccount.Id);
    }

    #region helper function
    private string GenerateVerificationToken()
    {
        // token is a cryptographically strong random sequence of values
        var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

        // ensure token is unique by checking against db
        var tokenIsUnique = !context.Accounts.Any(x => x.VerificationToken == token);
        if (!tokenIsUnique)
            return GenerateVerificationToken();

        return token;
    }


    private string? GenerateResetToken()
    {
        // token is a cryptographically strong random sequence of values
        var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

        // ensure token is unique by checking against db
        var tokenIsUnique = !context.Accounts.Any(x => x.ResetToken == token);
        if (!tokenIsUnique)
            return GenerateResetToken();

        return token;
    }

    private async Task SendVerificationEmail(SqlAccount user, string origin)
    {
        string message = File.ReadAllTextAsync("HtmlEmails/VerifyEmail.html").GetAwaiter().GetResult();
        message = message.Replace("[[name]]", user.Email);
        if (!string.IsNullOrEmpty(origin))
        {
            // origin exists if request sent from browser single page app (e.g. Angular or React)
            // so send link to verify via single page app
            var verifyUrl = $"{origin}/verify-email?token={user.VerificationToken}";
            message = message.Replace("[[link]]", verifyUrl);
        }
        else
        {
            // origin missing if request sent directly to api (e.g. from Postman)
            // so send instructions to verify directly with api
            message =
                $@"<p>Please use the below token to verify your email address with the <code>/accounts/verify-email</code> api route:</p>
                            <p><code>{user.VerificationToken}</code></p>";
        }

        await _email.Send(
            to: user.Email,
            subject: "Sign-up Verification API - Verify Email",
            html: message
        );
    }

    private async Task<Result> SendPasswordResetEmail(SqlAccount account, string origin)
    {
        string message = File.ReadAllTextAsync("HtmlEmails/PasswordReset.html").GetAwaiter().GetResult();
        message = message.Replace("[[name]]", account.Email);

        if (!string.IsNullOrEmpty(origin))
        {
            var resetUrl = $"{origin}/reset-password?token={account.ResetToken}";
            message = message.Replace("[[link]]", resetUrl);
        }
        else
        {
            message =
                $@"<p>Please use the below token to reset your password with the <code>/accounts/reset-password</code> api route:</p>
                            <p><code>{account.ResetToken}</code></p>";
        }

        await _email.Send(
            to: account.Email,
            subject: "Sign-up Verification API - Reset Password",
            html: message
        );
        return Result.Success();
    }


    private async Task<Result<SqlAccount>> GetAccountByResetToken(string token)
    {
        SqlAccount? account = await context.Accounts.FirstOrDefaultAsync(x =>
            x.ResetToken == token && x.ResetTokenExpires > DateTime.UtcNow);

        if (account == null) return Result.Failure<SqlAccount>(AuthErrors.InvalidToken(token));

        return Result.Success(account);
    }

    #endregion

}
