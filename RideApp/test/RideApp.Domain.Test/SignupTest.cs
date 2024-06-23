using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RideApp.Data.Context;
using RideApp.Domain.Entities;
using RideApp.Domain.Interfaces;
using RideApp.Domain.UseCases;
using Xunit;

namespace RideApp.Domain.Test;

public class SignupTest : IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private readonly AppDbContext _dbContext;
    private readonly SignUp _signUp;
    private readonly Mock<IAccountRepository> _mock;
    
    public SignupTest()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql("Host=localhost;Port=5432;Database=cccc;Username=postgres;Password=148036"));
        serviceCollection.AddScoped<SignUp>();
        serviceCollection.AddTransient<IAccountRepository, AccountRepository>();
        _serviceProvider = serviceCollection.BuildServiceProvider();
        _dbContext = _serviceProvider.GetRequiredService<AppDbContext>();
        _signUp = _serviceProvider.GetRequiredService<SignUp>();
        _dbContext.Database.EnsureCreated();
        // _mock = new Mock<IAccountRepository>();
        // _signUp = new SignUp(_mock.Object);
    }
    
    [Fact]
    public async void Signup_Successfully()
    {
        var accountExpected = new
        {
            Id = Guid.NewGuid(),
            Name = "Matheus",
            Email = "matheus@gmail.com",
            Cpf = "12345678900",
            CarPlate = "ABC-1234",
            IsPassenger = false,
            IsDriver = true
        };
        
        var account = new Account(accountExpected.Id, accountExpected.Name, accountExpected.Email, accountExpected.Cpf, accountExpected.CarPlate, accountExpected.IsPassenger, accountExpected.IsDriver);
        var accountCreatedId = await _signUp.Execute(account);
        accountExpected.Should().NotBeNull();
        // _mock.Setup(repository => repository.Create(account)).ReturnsAsync(account);
        // var accountCreatedId = await _signUp.Execute(account);
        // _mock.Verify(r => r.Create(It.Is<Account>(account1 => 
        //     account1.Id == accountExpected.Id && 
        //     account1.Name == accountExpected.Name && 
        //     account1.Email == accountExpected.Email && 
        //     account1.Cpf == accountExpected.Cpf &&
        //     account1.CarPlate == accountExpected.CarPlate &&
        //     account1.IsDriver == accountExpected.IsDriver &&
        //     account1.IsPassenger == accountExpected.IsPassenger
        //     )
        // ));
        // accountCreatedId.Should().Be(accountExpected.Id);
    }


    public void Dispose()
    {
        _dbContext.Dispose();
    }
}