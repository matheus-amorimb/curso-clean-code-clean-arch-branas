using RideApp.Domain.ValueObjects;

namespace RideApp.Domain.Entities;
public class Ride : Entity
{
    public Guid PassengerId { get; private set;}
    public Guid DriverId { get; private set;}
    public string? Status { get; private set;}
    public double Fare { get; private set;}
    public double Distance { get; private set;}
    public double FromLat { get; private set;}
    public double FromLong { get; private set;}
    public double ToLat { get; private set;}
    public double ToLong { get; private set;}
    public DateTime Timestamp { get; private set;}
    
    public Ride(){}
    public Ride(Guid passengerId, Distance from, Distance to)
    {
        PassengerId = passengerId;
        FromLat = from.Latitude;
        FromLong = from.Longitude;
        ToLat = from.Latitude;
        ToLong = from.Longitude;
    }
}