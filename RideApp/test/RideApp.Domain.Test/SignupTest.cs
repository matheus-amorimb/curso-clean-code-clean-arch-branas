using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RideApp.Data.Context;
using RideApp.Domain.Entities;
using RideApp.Domain.Interfaces;
using RideApp.Domain.Test.Builders;
using RideApp.Domain.UseCases;
using Xunit;

namespace RideApp.Domain.Test;

public class SignupTest : IDisposable
{
    private readonly AppDbContext _dbContext;
    private readonly SignUp _signUp;
    private readonly AccountService _accountService;
    
    public SignupTest()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql("Host=localhost;Port=5432;Database=cccc;Username=postgres;Password=148036"));
        serviceCollection.AddScoped<SignUp>();
        serviceCollection.AddScoped<AccountService>();
        serviceCollection.AddTransient<IAccountRepository, AccountRepository>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        _signUp = serviceProvider.GetRequiredService<SignUp>();
        _accountService = serviceProvider.GetRequiredService<AccountService>();
        // _dbContext.Database.EnsureCreated();
    }
    
    [Fact]
    public async void Signup_Successfully()
    {
        var accountExpected = new
        {
            Id = Guid.NewGuid(),
            Name = "Matheus Batista",
            Email = "matheus123@gmail.com",
            Cpf = "12345678900",
            CarPlate = "ABC1234",
            IsPassenger = false,
            IsDriver = true
        };
        
        var account = new Account(accountExpected.Id, accountExpected.Name, accountExpected.Email, accountExpected.Cpf, accountExpected.CarPlate, accountExpected.IsPassenger, accountExpected.IsDriver);
        var accountCreatedId = await _signUp.Execute(account);
        var accountCreated = await _accountService.GetAccount(accountCreatedId);
        accountCreatedId.Should().Be(accountExpected.Id);
        accountCreated?.Name.Should().Be(accountExpected.Name);
        accountCreated?.Email.Should().Be(accountExpected.Email);
    }

    [Fact]
    public async void Signup_WithEmailInUse_ThrowsAnException()
    {
        var account1 = AccountBuilder.New().Build();
        var accountWithEmailInUse = AccountBuilder.New().Build();
        await _signUp.Execute(account1);
        Func<Task> action = async() => await _signUp.Execute(accountWithEmailInUse);
        await action.Should().ThrowAsync<ArgumentException>().WithMessage("Email already in use.");
    }

    [Theory]
    [InlineData("")]
    [InlineData("matheus")]
    public async void SignUp_WithAnInvalidName_ThrowsAnException(string invalidName)
    {
        var account = AccountBuilder.New().WithName(invalidName).Build();
        Func<Task> action = async() => await _signUp.Execute(account);
        await action.Should().ThrowAsync<ArgumentException>().WithMessage("Name invalid.");
    }    
    
    [Theory]
    [InlineData("matheus@")]
    [InlineData("matheus@gmail")]
    public async void SignUp_WithAnInvalidEmail_ThrowsAnException(string invalidEmail)
    {
        var account = AccountBuilder.New().WithEmail(invalidEmail).Build();
        Func<Task> action = async() => await _signUp.Execute(account);
        await action.Should().ThrowAsync<ArgumentException>().WithMessage("Email invalid.");
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("000.000.000-00")]
    [InlineData("085.432.125-56")]
    public async void SignUp_WithAnInvalidCpf_ThrowsAnException(string invalidCpf)
    {
        var account = AccountBuilder.New().WithCpf(invalidCpf).Build();
        Func<Task> action = async() => await _signUp.Execute(account);
        await action.Should().ThrowAsync<ArgumentException>().WithMessage("Cpf invalid.");
    }    
    
    [Theory]
    [InlineData("")]
    [InlineData("AB91486")]
    [InlineData("ABCDEFG")]
    public async void SignUp_WithAnInvalidCarPlate_ThrowsAnException(string invalidCarPlate)
    {
        var account = AccountBuilder.New().IsDriver().WithCarPlate(invalidCarPlate).Build();
        Func<Task> action = async() => await _signUp.Execute(account);
        await action.Should().ThrowAsync<ArgumentException>().WithMessage("Car Plate invalid.");
    }

    public void Dispose()
    {
        _dbContext.Accounts.RemoveRange(_dbContext.Accounts);
        _dbContext.SaveChanges();
        _dbContext.Dispose();
    }
}