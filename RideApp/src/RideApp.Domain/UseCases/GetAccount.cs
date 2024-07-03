using RideApp.Domain.Entities;
using RideApp.Domain.Interfaces;

namespace RideApp.Domain.UseCases;

public class GetAccount(IAccountRepository accountRepository) : IUseCase<Guid, Account?>
{
    private readonly IAccountRepository _accountRepository = accountRepository;
    public async Task<Account?> Execute(Guid accountId)
    {
        return await _accountRepository.GetById(accountId);
    }
    
    // TODO: It should return an output instead of returning the entity 
}