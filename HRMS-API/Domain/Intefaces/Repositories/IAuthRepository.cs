using Domain.Entities.Accounts;
using Domain.Intefaces.Repositories.Base;

namespace Domain.Intefaces.Repositories;
public interface IAuthRepository : IRepository<SqlAccount>
{
    Task<SqlAccount> GetByUserId(int userId);
}
