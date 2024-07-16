using RideApp.Domain.Entities;
using RideApp.Domain.Interfaces;
using RideApp.Domain.ValueObjects;

namespace RideApp.Domain.UseCases;

public class RequestRide(IRideRepository rideRepository, IAccountRepository accountRepository)
{
    private readonly IRideRepository _rideRepository = rideRepository;
    private readonly IAccountRepository _accountRepository = accountRepository;
    public Ride Ride { get; private set; }

    public async Task<Guid> Execute(Guid passengerId, Distance from, Distance to)
    {
        var account = await _accountRepository.GetById(passengerId);
        if (account.IsDriver) throw new ArgumentException("Driver cannot request a ride");
        var hasUncompletedRide = await _rideRepository.PassengerHasUncompletedRide(passengerId);
        if (hasUncompletedRide)
            throw new ArgumentException("Passenger has an uncompleted ride.");
        Ride = await _rideRepository.Create(new Ride(passengerId, from, to));
        return Ride.Id;
    }
}