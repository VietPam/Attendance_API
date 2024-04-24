namespace Services.Services.Auth1;
//public class AuthService1(ApplicationDbContext context)
//{
//    public async Task InitAsync()
//    {
//        if (await context.Accounts.AnyAsync())
//        {
//            return;
//        }

//        SqlAccount admin = new()
//        {
//            RoleId = 1,
//            Email = "admin@gmail.com",
//            Password = "admin",
//        };
//        SqlAccount employee = new()
//        {
//            RoleId = 2,
//            Email = "employee@gmail.com",
//            Password = "employee",
//        };
//        await context.Accounts.AddRangeAsync([admin, employee]);
//        await context.SaveChangesAsync();

//    }
//    public async Task<bool> LoginAsync(string email, string password)
//    {
//        SqlAccount? user = await context.Accounts.FirstOrDefaultAsync(s => s.Email == email);

//        if (user == null)
//        {
//            return false;
//        }

//        if (user.Password != password)
//        {
//            return false;
//        }

//        return true;
//    }
//}
