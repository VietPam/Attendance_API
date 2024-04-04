namespace Domain.Intefaces.Repositories.Base;
public interface IUnitOfWork
{
    IAuthRepository AuthRepository { get; }
    void CreateTransaction();
    void Commit();
    void Rollback();
    Task SaveChanges();
}
