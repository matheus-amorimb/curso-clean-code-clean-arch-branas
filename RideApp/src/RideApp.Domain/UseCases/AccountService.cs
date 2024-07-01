using RideApp.Domain.Entities;
using RideApp.Domain.Interfaces;

namespace RideApp.Domain.UseCases;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account?> GetAccount(Guid accountId)
    {
        var account = await _accountRepository.GetById(accountId);
        return account;
    }
}