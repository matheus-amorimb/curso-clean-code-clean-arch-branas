using RideApp.Domain.Entities;
using RideApp.Domain.Interfaces;

namespace RideApp.Domain.UseCases;

public class SignUp
{
    private readonly IAccountRepository _accountRepository;
    public SignUp(IAccountRepository account)
    {
        _accountRepository = account;
    }

    public async Task<Guid> Execute(Account account)
    {
        var accountCreated = await _accountRepository.Create(account);
        return accountCreated.Id;
    }
    
}