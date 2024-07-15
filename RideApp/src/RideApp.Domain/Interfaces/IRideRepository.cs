using RideApp.Domain.Entities;

namespace RideApp.Domain.Interfaces;

public interface IRideRepository : IRepository<Ride>
{
    Task<Boolean> HasUncompletedRide(Guid passengerId);
}