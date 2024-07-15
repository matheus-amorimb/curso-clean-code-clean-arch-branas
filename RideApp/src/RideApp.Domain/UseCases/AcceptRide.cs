using RideApp.Domain.Interfaces;

namespace RideApp.Domain.UseCases;

public class AcceptRide(IRideRepository _rideRepository)
{
    public async Task Execute(Guid rideId, Guid driverId)
    {
        var ride = await _rideRepository.GetById(rideId);
        ride.SetDriverId(driverId);
        ride.SetStatus("accepted");
        _rideRepository.Update(ride);
    }
}