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
    private readonly Distance _from = new Distance(40.712776, -74.005974);
    private readonly Distance _to = new Distance(40.802606, -74.005974);

    [Fact]
    public async void RequestRide_Successfully()
    {
        var account = AccountBuilder.New().Build();
        var accountCreatedId = await _signUp.Execute(account);
        var rideId = await _requestRide.Execute(accountCreatedId, this._from, this._to);
        rideId.Should().NotBeEmpty();
        _requestRide.Ride.Status.Should().Be("requested");
        _requestRide.Ride.Timestamp.Date.Should().Be(DateTime.Today);
    }

    [Fact]
    public async void RequestRide_WithAnDriverAccount_ThrowsAnException()
    {
        var account = AccountBuilder.New()
            .IsDriver().
            Build();
        var accountCreatedId = await _signUp.Execute(account);
        Func<Task> requestRide = async () => await _requestRide.Execute(accountCreatedId, this._from, this._to);
        await requestRide.Should().ThrowAsync<ArgumentException>().WithMessage("Driver cannot request a ride");
    }

    [Fact]
    public async void RequestRide_WithAnUncompletedRide_ThrowsAnException()
    {
        var account = AccountBuilder.New().Build();
        var accountCreatedId = await _signUp.Execute(account);
        await _requestRide.Execute(accountCreatedId, this._from, this._to);
        Func<Task> requestSecondRide = async () => await _requestRide.Execute(accountCreatedId, this._from, this._to);
        await requestSecondRide.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Passenger has an uncompleted ride.");
    }
    
}