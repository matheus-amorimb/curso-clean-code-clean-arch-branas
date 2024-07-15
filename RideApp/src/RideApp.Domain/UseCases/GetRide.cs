using RideApp.Domain.Entities;
using RideApp.Domain.Interfaces;

namespace RideApp.Domain.UseCases;

public class GetRide (IRideRepository rideRepository) : IUseCase<Guid, Ride>
{
    private readonly IRideRepository _rideRepository = rideRepository;

    public async Task<Ride> Execute(Guid rideId)
    {
        return await _rideRepository.GetById(rideId);
    }
}