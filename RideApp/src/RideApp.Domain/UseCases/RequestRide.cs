using RideApp.Domain.Entities;
using RideApp.Domain.Interfaces;

namespace RideApp.Domain.UseCases;

public class RequestRide
{
    private readonly IRideRepository _rideRepository;

    public RequestRide(IRideRepository rideRepository)
    {
        _rideRepository = rideRepository;
    }

    public Guid Execute(Ride ride)
    {
        return Guid.NewGuid();
    }
}