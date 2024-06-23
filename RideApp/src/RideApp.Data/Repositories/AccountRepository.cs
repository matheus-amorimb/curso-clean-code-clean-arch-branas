using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RideApp.Data.Context;
using RideApp.Domain.Entities;
using RideApp.Domain.Interfaces;

namespace RideApp.Domain;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext context) : base(context){}
    
    public async Task<Account?> GetByEmail(string email)
    {
        return await Context.Set<Account>().FirstOrDefaultAsync(acc => acc.Email == email);
    }
}
// private readonly SqliteConnectionStringBuilder _connectionStringBuilder;
    // private readonly SqliteConnection _connection;
    //
    // public AccountRepository()
    // {
    //     _connectionStringBuilder = new SqliteConnectionStringBuilder();
    //     _connectionStringBuilder.DataSource =
    //         "/home/matheus/matheus-dev/code/software-engineering/curso-clean-code-clean-arch-branas/RideApp/src/RideApp.Domain/Data/account.db";
    // }
    // public void CreateTable()
    // {
    //     using var connection = new SqliteConnection(_connectionStringBuilder.ConnectionString);
    //     connection.Open();
    //     var command = connection.CreateCommand();
    //     command.CommandText = @"
    //             CREATE TABLE IF NOT EXISTS account (
    //                 account_id TEXT PRIMARY KEY,
    //                 name TEXT NOT NULL,
    //                 email TEXT NOT NULL,
    //                 cpf TEXT NOT NULL,
    //                 car_plate TEXT,
    //                 is_passenger BOOLEAN NOT NULL DEFAULT FALSE,
    //                 is_driver BOOLEAN NOT NULL DEFAULT FALSE
    //             );
    //         ";
    //     command.ExecuteNonQuery();
    // }
    //
    // public void InsertAccount(Account account)
    // {
    //     using var connection = new SqliteConnection(_connectionStringBuilder.ConnectionString);
    //     connection.Open();
    //     using var transaction = connection.BeginTransaction();
    //     var command = connection.CreateCommand();
    //     command.CommandText = @"
    //         INSERT INTO account (account_id, name, email, cpf, car_plate, is_passenger, is_driver)
    //         VALUES ($accountId, $name, $email, $cpf, $carPlate, $isPassenger, $isDriver);
    //     ";
    //     command.Parameters.AddWithValue("$accountId", account.AccountId);
    //     command.Parameters.AddWithValue("$name", account.Name);
    //     command.Parameters.AddWithValue("$email", account.Email);
    //     command.Parameters.AddWithValue("$cpf", account.Cpf);
    //     command.Parameters.AddWithValue("$carPlate", account.CarPlate);
    //     command.Parameters.AddWithValue("$isPassenger", account.IsPassenger);
    //     command.Parameters.AddWithValue("$isDriver", account.IsDriver);
    //     command.ExecuteNonQuery();
    //     transaction.Commit();
    // }
    