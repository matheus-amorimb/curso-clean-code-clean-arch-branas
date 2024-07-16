using RideApp.Domain.Entities;

namespace RideApp.Domain.Interfaces;

public interface IRideRepository : IRepository<Ride>
{
    Task<Boolean> PassengerHasUncompletedRide(Guid passengerId);
    Task<Boolean> DriverHasUncompletedRide(Guid driverId);
}