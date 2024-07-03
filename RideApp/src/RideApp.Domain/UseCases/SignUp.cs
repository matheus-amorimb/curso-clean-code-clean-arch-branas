using System.Text.RegularExpressions;
using RideApp.Domain.Entities;
using RideApp.Domain.Interfaces;
using RideApp.Domain.Utilities;

namespace RideApp.Domain.UseCases;

public class SignUp(IAccountRepository accountRepository) : IUseCase<Account, Guid>
{
    private readonly IAccountRepository _accountRepository = accountRepository;
    
    public async Task<Guid> Execute(Account account)
    {
        ValidateEmail(account.Email);
        await ValidateEmailIsNotInUse(account.Email);
        ValidateName(account.Name);
        if (account.IsDriver) ValidateCarPlate(account.CarPlate);
        var accountCreated = await _accountRepository.Create(account);
        return accountCreated.Id;
    }


    private void ValidateEmail(string accountEmail)
    {
        var emailPattern = @"^[\w-\.]+\@([\w-]+\.)+[\w-]{2,4}$";
        if(!Regex.IsMatch(accountEmail, emailPattern)) throw new ArgumentException("Email invalid.");
    }
    
    private async Task ValidateEmailIsNotInUse(string accountEmail)
    {
        var existingAccount = await _accountRepository.GetByEmail(accountEmail);
        if (existingAccount is not null) throw new ArgumentException("Email already in use.");
    }

    private void ValidateName(string accountName)
    {
        var namePattern = @"^[a-zA-Z]+ [a-zA-Z]+$";
        if(!Regex.IsMatch(accountName, namePattern)) throw new ArgumentException("Name invalid.");
    }
    
    private void ValidateCarPlate(string accountCarPlate)
    {
        var carPlatePattern = @"[A-Z]{3}[0-9]{4}";
        if(!Regex.IsMatch(accountCarPlate, carPlatePattern)) throw new ArgumentException("Car Plate invalid.");
    }
}
