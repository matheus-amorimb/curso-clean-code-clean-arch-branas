using RideApp.Domain.Interfaces;
using RideApp.Domain.Utilities;

namespace RideApp.Domain.UseCases;

public class AcceptRide(IRideRepository rideRepository, GetAccount getAccount)
{
    public async Task Execute(Guid rideId, Guid driverId)
    {
        var ride = await rideRepository.GetById(rideId);
        if (ride is null) throw new ArgumentException("Ride not found.");
        if (ride.Status != RideStatus.Requested) throw new ArgumentException("Only ride with status requested can be accepted.");
        var account = await getAccount.Execute(driverId);
        if (account is null) throw new ArgumentException("Driver not found.");
        if (account?.IsDriver == false) throw new ArgumentException("Only drivers can accept a ride.");
        if(await rideRepository.DriverHasUncompletedRide(driverId)) throw new ArgumentException("Driver must complete ongoing ride before accepting a new one.");
        ride.SetDriverId(driverId);
        ride.SetStatus(RideStatus.Accepted);
        rideRepository.Update(ride);
    }
}