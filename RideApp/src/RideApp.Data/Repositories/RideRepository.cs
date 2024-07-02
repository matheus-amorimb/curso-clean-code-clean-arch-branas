using RideApp.Data.Context;
using RideApp.Domain.Entities;
using RideApp.Domain.Interfaces;

namespace RideApp.Domain;

public class RideRepository : Repository<Ride>, IRideRepository
{
    protected RideRepository(AppDbContext context) : base(context)
    { }
}