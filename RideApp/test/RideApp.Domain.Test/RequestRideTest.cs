using FluentAssertions;
using RideApp.Data.Context;
using RideApp.Domain.Entities;
using RideApp.Domain.Test.Builders;
using RideApp.Domain.Test.Fixtures;
using RideApp.Domain.UseCases;
using RideApp.Domain.ValueObjects;
using Xunit;

namespace RideApp.Domain.Test;

public class RequestRideTest(TestFixture testFixture) : IClassFixture<TestFixture>
{
    private readonly AppDbContext _dbContext = testFixture.DbContext;
    private readonly SignUp _signUp = testFixture.SignUp;
    private readonly RequestRide _requestRide = testFixture.RequestRide;

    [Fact]
    public async void RequestRide_Successfully()
    {
        var email = $"matheus{new Random().Next(100, 1000)}@email.com";
        var account = AccountBuilder.New().WithEmail(email).Build();
        var accountCreatedId = await _signUp.Execute(account);
        var from = new Distance(40.712776, -74.005974);
        var to = new Distance(40.802606, -74.005974);
        var rideId = await _requestRide.Execute(accountCreatedId, from, to);
        rideId.Should().NotBeEmpty();
        _requestRide.ride.Status.Should().Be("requested");
        _requestRide.ride.Timestamp.Date.Should().Be(DateTime.Today);
    }
    
}