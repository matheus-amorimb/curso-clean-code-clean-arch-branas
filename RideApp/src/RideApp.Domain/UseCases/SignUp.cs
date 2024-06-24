using System.Text.RegularExpressions;
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
        ValidateEmail(account.Email);
        ValidateEmailIsNotInUse(account.Email);
        ValidateName(account.Name);
        ValidateCpf(account.Cpf);
        // if (account.IsDriver) ValidateCarPlate(account.CarPlate);
        var accountCreated = await _accountRepository.Create(account);
        return accountCreated.Id;
    }


    private void ValidateEmail(string accountEmail)
    {
        var emailPattern = @"^[\w-\.]+\@([\w-]+\.)+[\w-]{2,4}$";
        if(!Regex.IsMatch(accountEmail, emailPattern)) throw new ArgumentException("Email invalid.");
    }
    
    private async void ValidateEmailIsNotInUse(string accountEmail)
    {
        var existingAccount = await _accountRepository.GetByEmail(accountEmail);
        if (existingAccount is not null) throw new ArgumentException("Email already in use.");
    }

    private void ValidateName(string accountName)
    {
        var namePattern = @"^[a-zA-Z]+ [a-zA-Z]+$";
        if(!Regex.IsMatch(accountName, namePattern)) throw new ArgumentException("Name invalid.");
    }
    
    private void ValidateCpf(string accountCpf)
    {
        throw new NotImplementedException();
    }
    private void ValidateCarPlate(string accountCarPlate)
    {
        // var
    }
}
