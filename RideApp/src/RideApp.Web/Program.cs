using Microsoft.EntityFrameworkCore;
using RideApp.Data.Context;
using RideApp.Domain;
using RideApp.Domain.Entities;
using RideApp.Domain.Interfaces;
using RideApp.Domain.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    var postgresConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    optionsBuilder.UseNpgsql(postgresConnectionString);
});

builder.Services.AddTransient<IAccountRepository, AccountRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();