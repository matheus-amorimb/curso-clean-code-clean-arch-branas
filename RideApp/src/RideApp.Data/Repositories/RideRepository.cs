using Microsoft.EntityFrameworkCore;
using RideApp.Data.Context;
using RideApp.Domain.Entities;
using RideApp.Domain.Interfaces;

namespace RideApp.Domain;

public class RideRepository(AppDbContext context) : Repository<Ride>(context), IRideRepository
{
    public async Task<bool> HasUncompletedRide(Guid passengerId)
    {
        var uncompletedRide = await Context.Rides.FromSqlRaw("SELECT * FROM \"Rides\" WHERE \"Status\" != 'completed' AND \"PassengerId\" = {0}", passengerId).ToListAsync();
        return uncompletedRide.Any();
        // return await Context.Rides.AnyAsync(ride => ride.Status != "completed");
    }
}