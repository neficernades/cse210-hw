using System;
using System.Collections.Generic;

abstract class Activity
{
    public string Date { get; private set; }
    public int Length { get; private set; } // in minutes

    protected Activity(string date, int length)
    {
        Date = date;
        Length = length;
    }

    public abstract float GetDistance(); // in miles
    public abstract float GetSpeed(); // in mph
    public abstract float GetPace(); // in minutes per mile

    public string GetSummary()
    {
        return $"{Date} {GetType().Name} ({Length} min) - Distance: {GetDistance():0.00} miles, Speed: {GetSpeed():0.00} mph, Pace: {GetPace():0.00} min/mile";
    }
}

class Running : Activity
{
    private float Distance { get; set; } // in miles

    public Running(string date, int length, float distance) : base(date, length)
    {
        Distance = distance;
    }

    public override float GetDistance()
    {
        return Distance;
    }

    public override float GetSpeed()
    {
        return (Distance / Length) * 60; // miles per hour
    }

    public override float GetPace()
    {
        return Length / Distance; // minutes per mile
    }
}

class Cycling : Activity
{
    private float Speed { get; set; } // in mph

    public Cycling(string date, int length, float speed) : base(date, length)
    {
        Speed = speed;
    }

    public override float GetDistance()
    {
        return (Speed * Length) / 60; // distance in miles
    }

    public override float GetSpeed()
    {
        return Speed;
    }

    public override float GetPace()
    {
        return 60 / Speed; // minutes per mile
    }
}

class Swimming : Activity
{
    private int Laps { get; set; }
    private const float LapLength = 50; // length of each lap in meters

    public Swimming(string date, int length, int laps) : base(date, length)
    {
        Laps = laps;
    }

    public override float GetDistance()
    {
        return (Laps * LapLength) / 1000 * 0.62f; // convert meters to miles
    }

    public override float GetSpeed()
    {
        return (GetDistance() / Length) * 60; // miles per hour
    }

    public override float GetPace()
    {
        return Length / GetDistance(); // minutes per mile
    }
}

class Program
{
    static void Main(string[] args)
    {
        var running = new Running("2023-07-10", 30, 3); // 3 miles in 30 minutes
        var cycling = new Cycling("2023-07-11", 45, 15); // 15 mph for 45 minutes
        var swimming = new Swimming("2023-07-12", 30, 20); // 20 laps in 30 minutes

        var activities = new List<Activity> { running, cycling, swimming };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
