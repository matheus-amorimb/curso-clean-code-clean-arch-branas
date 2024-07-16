using Microsoft.EntityFrameworkCore;
using RideApp.Data.Context;
using RideApp.Domain.Entities;
using RideApp.Domain.Interfaces;

namespace RideApp.Domain;

public class RideRepository(AppDbContext context) : Repository<Ride>(context), IRideRepository
{
    public async Task<bool> PassengerHasUncompletedRide(Guid passengerId)
    {
        var uncompletedRide = await Context
            .Rides
            .FromSqlRaw("SELECT * FROM rides WHERE status != 'completed' AND passenger_id = {0}", passengerId)
            .ToListAsync();
        return uncompletedRide.Any();
    }

    public async Task<bool> DriverHasUncompletedRide(Guid driverId)
    {
        var uncompletedRide = await Context
            .Rides
            .FromSql($"SELECT * FROM rides WHERE status != 'completed' AND driver_id = {driverId}")
            .ToListAsync();
        return uncompletedRide.Any();
    }
}