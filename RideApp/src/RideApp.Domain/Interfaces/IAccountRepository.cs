using RideApp.Domain.Entities;

namespace RideApp.Domain.Interfaces;

public interface IAccountRepository : IRepository<Account>
{
    Task<Account?> GetByEmail(string email);
}