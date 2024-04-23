using Domain.Entities.Accounts;
using Domain.Intefaces.Repositories.Base;

namespace Domain.Intefaces.Repositories;
public interface IAuthRepository : IRepository<Account>
{
    Task<Account> GetByUserId(int userId);
}
