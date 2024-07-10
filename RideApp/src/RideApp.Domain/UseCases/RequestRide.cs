using RideApp.Domain.Entities;
using RideApp.Domain.Interfaces;
using RideApp.Domain.ValueObjects;

namespace RideApp.Domain.UseCases;

public class RequestRide(IRideRepository rideRepository)
{
    private readonly IRideRepository _rideRepository = rideRepository;
    public Ride ride { get; set; }

    public async Task<Guid> Execute(Guid passengerId, Distance from, Distance to)
    {
        ride = await _rideRepository.Create(new Ride(passengerId, from, to));
        return ride.Id;
    }
}