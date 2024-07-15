using FluentAssertions;
using RideApp.Domain.Interfaces;
using RideApp.Domain.Test.Builders;
using RideApp.Domain.Test.Fixtures;
using RideApp.Domain.UseCases;
using RideApp.Domain.ValueObjects;
using Xunit;

namespace RideApp.Domain.Test;

public class AcceptRideTest(TestFixture testFixture) : IClassFixture<TestFixture>
{
    private readonly AcceptRide _acceptRide = testFixture.AcceptRide;
    private readonly SignUp _signUp = testFixture.SignUp;
    private readonly RequestRide _requestRide = testFixture.RequestRide;
    private readonly GetRide _getRide = testFixture.GetRide;
    private readonly Distance _from = new Distance(40.712776, -74.005974);
    private readonly Distance _to = new Distance(40.802606, -74.005974);
    
    [Fact]
    public async void Driver_AcceptsRide_Successfully()
    {
        //Arrange
        var userAccount = AccountBuilder.New().Build();
        var driverAccount = AccountBuilder.New()
            .IsDriver()
            .Build();
        var userAccountId = await _signUp.Execute(userAccount);
        var driverAccountId = await _signUp.Execute(driverAccount);
        var rideId = await _requestRide.Execute(userAccountId, this._from, this._to);
        
        //Act
        
        await _acceptRide.Execute(rideId, driverAccountId);
        var ride = await _getRide.Execute(rideId);
        
        //Assert
        ride.DriverId.Should().Be(driverAccountId);
        ride.Status.Should().Be("accepted");
    } 
}