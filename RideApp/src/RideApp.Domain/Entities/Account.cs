using RideApp.Domain.Utilities;

namespace RideApp.Domain.Entities;

public class Account : Entity
{
    private Account(){}
    
    public Account(string name, string email, string cpf, string carPlate, bool isPassenger, bool isDriver)
    {
        Name = name;
        Email = email;
        Cpf = new Cpf(cpf);
        CarPlate = carPlate;
        IsPassenger = isPassenger;
        IsDriver = isDriver;
    }    
    public Account(Guid accountId, string name, string email, string cpf, string carPlate, bool isPassenger, bool isDriver)
    {
        Id = accountId;
        Name = name;
        Email = email;
        Cpf = new Cpf(cpf);
        CarPlate = carPlate;
        IsPassenger = isPassenger;
        IsDriver = isDriver;
    }

    public string Name {get; private set;}
    public string Email {get; private set;}
    public Cpf Cpf {get; private set;}
    public string CarPlate {get; private set;}
    public bool IsPassenger {get; private set;}
    public bool IsDriver {get; private set;}
}