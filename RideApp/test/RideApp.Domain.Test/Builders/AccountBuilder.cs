using RideApp.Domain.Entities;

namespace RideApp.Domain.Test.Builders;

public class AccountBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _name = "Matheus Batista";
    private string _email = "matheus@gmail.com";
    private string _cpf = "12345678900";
    private string _carPlate = "ABC-1234";
    private bool _isPassenger = false;
    private bool _isDriver = true;


    public static AccountBuilder New()
    {
        return new AccountBuilder();
    }

    public AccountBuilder WithName(string name)
    {
        _name = name;
        return this;
    }    
    
    public AccountBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }   
    
    public AccountBuilder WithCpf(string cpf)
    {
        _cpf = cpf;
        return this;
    }   
    
    public AccountBuilder WithCarPlate(string carPlate)
    {
        _carPlate = carPlate;
        return this;
    }

    public Account Build()
    {
        return new Account(_id, _name, _email, _cpf, _carPlate, _isPassenger, _isDriver);
    }
    
}