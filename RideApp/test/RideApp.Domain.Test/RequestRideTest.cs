using FluentAssertions;
using RideApp.Domain.Entities;
using RideApp.Domain.UseCases;
using RideApp.Domain.ValueObjects;
using Xunit;

namespace RideApp.Domain.Test;

public class RequestRideTest
{
    public RequestRideTest()
    {
        
    }
    
    [Fact]
    public void RequestRide_Successfully()
    {
        var from = new Distance(40.712776, -74.005974);
        var to = new Distance(40.802606, -74.005974);
        var ride = new Ride(Guid.NewGuid(), from, to);
        // var requestRide = new RequestRide();
        // var rideId = requestRide.Execute(ride);
        // rideId.Should().NotBeEmpty();
    }
    
}