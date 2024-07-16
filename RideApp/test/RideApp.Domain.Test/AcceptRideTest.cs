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
    
    [Fact]
    public async void NotADriver_AcceptsRide_ThrowAnException()
    {
        //Arrange
        var userAccount = AccountBuilder.New().Build();
        var userAccountId = await _signUp.Execute(userAccount);
        var rideId = await _requestRide.Execute(userAccountId, this._from, this._to);
        
        //Act
        Func<Task> acceptRide = async () =>  await _acceptRide.Execute(rideId, userAccountId);
        
        //Assert
        await acceptRide.Should()
            .ThrowAsync<ArgumentException>()
            .WithMessage("Only drivers can accept a ride.");
    } 
    
    [Fact]
    public async void AcceptsRide_WithStatusNotRequested_ThrowAnException()
    {
        //Arrange
        var userAccount = AccountBuilder.New().Build();
        var driverAccount = AccountBuilder.New()
            .IsDriver()
            .Build();
        var userAccountId = await _signUp.Execute(userAccount);
        var driverAccountId = await _signUp.Execute(driverAccount);
        var rideId = await _requestRide.Execute(userAccountId, this._from, this._to);
        await _acceptRide.Execute(rideId, driverAccountId);
        
        //Act
        Func<Task> acceptRide = async () => await _acceptRide.Execute(rideId, driverAccountId);
        
        //Assert
        await acceptRide.Should().ThrowAsync<ArgumentException>().WithMessage("Only ride with status requested can be accepted.");
    } 
    
    [Fact]
    public async void DriverAcceptsRide_WithUncompletedRide_ThrowAnException()
    {
        //Arrange
        var userAccount = AccountBuilder.New().Build();
        var driverAccount = AccountBuilder.New()
            .IsDriver()
            .Build();
        var userAccountId = await _signUp.Execute(userAccount);
        var driverAccountId = await _signUp.Execute(driverAccount);
        var rideId = await _requestRide.Execute(userAccountId, this._from, this._to);
        await _acceptRide.Execute(rideId, driverAccountId);
        
        //Act
        Func<Task> acceptRide = async () => await _acceptRide.Execute(rideId, driverAccountId);
        
        //Assert
        await acceptRide.Should().ThrowAsync<ArgumentException>().WithMessage("Driver must complete ongoing ride before accepting a new one.");
    } 
    
    
}