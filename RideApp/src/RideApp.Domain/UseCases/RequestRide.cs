using RideApp.Domain.Entities;
using RideApp.Domain.Interfaces;

namespace RideApp.Domain.UseCases;

public class RequestRide(IRideRepository rideRepository) : IUseCase<Ride, Guid>
{
    private readonly IRideRepository _rideRepository = rideRepository;

    public async Task<Guid> Execute(Ride ride)
    {
        return Guid.NewGuid();
    }
}