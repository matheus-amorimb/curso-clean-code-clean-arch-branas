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
    public GetRide GetRide { get; }
    public AcceptRide AcceptRide { get; }

    public TestFixture()
    {
        //SERVICE COLLECTION
        var serviceCollection = new ServiceCollection();
        
        //Context
        serviceCollection.AddDbContext<AppDbContext>(builder =>
        {
            builder.UseNpgsql("Host=localhost;Port=5432;Database=cccc;Username=postgres;Password=148036")
                .UseSnakeCaseNamingConvention();
        });
        
        //UseCases
        serviceCollection.AddScoped<SignUp>();
        serviceCollection.AddScoped<GetAccount>();
        serviceCollection.AddScoped<RequestRide>();
        serviceCollection.AddScoped<GetRide>();
        serviceCollection.AddScoped<AcceptRide>();
        
        //Repositories
        serviceCollection.AddTransient<IAccountRepository, AccountRepository>();
        serviceCollection.AddTransient<IRideRepository, RideRepository>();
        
        //SERVICE PROVIDER
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        DbContext = serviceProvider.GetService<AppDbContext>()!;
        SignUp = serviceProvider.GetService<SignUp>()!;
        RequestRide = serviceProvider.GetService<RequestRide>()!;
        AcceptRide = serviceProvider.GetService<AcceptRide>();
        GetRide = serviceProvider.GetService<GetRide>()!;
    }
    
    public async void Dispose()
    {
        await DbContext.SaveChangesAsync();
        await DbContext.DisposeAsync();
    }
}