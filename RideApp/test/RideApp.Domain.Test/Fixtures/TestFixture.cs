using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RideApp.Data.Context;
using RideApp.Domain.Entities;
using RideApp.Domain.Interfaces;
using RideApp.Domain.UseCases;

namespace RideApp.Domain.Test.Fixtures;

public class TestFixture : IDisposable
{
    public AppDbContext DbContext { get; }
    public SignUp SignUp { get; }
    public RequestRide RequestRide { get; }

    public TestFixture()
    {
        //SERVICE COLLECTION
        var serviceCollection = new ServiceCollection();
        //Context
        serviceCollection.AddDbContext<AppDbContext>(builder =>
        {
            builder.UseNpgsql("Host=localhost;Port=5432;Database=cccc;Username=postgres;Password=148036");
        });
        //UseCases
        serviceCollection.AddScoped<SignUp>();
        serviceCollection.AddScoped<GetAccount>();
        serviceCollection.AddScoped<RequestRide>();
        //Repositories
        serviceCollection.AddTransient<IAccountRepository, AccountRepository>();
        serviceCollection.AddTransient<IRideRepository, RideRepository>();
        
        //SERVICE PROVIDER
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        DbContext = serviceProvider.GetService<AppDbContext>()!;
        SignUp = serviceProvider.GetService<SignUp>()!;
        RequestRide = serviceProvider.GetService<RequestRide>()!;
    }
    
    public void Dispose()
    {
        DbContext.SaveChanges();
        DbContext.Dispose();
    }
}