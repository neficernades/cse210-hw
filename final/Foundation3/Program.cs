using System;
using System.Collections.Generic;

class Address
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }

    public Address(string street, string city, string state, string country)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
    }

    public string GetFullAddress()
    {
        return $"{Street}, {City}, {State}, {Country}";
    }
}

class Event
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Date { get; private set; }
    public string Time { get; private set; }
    public Address Address { get; private set; }

    public Event(string title, string description, string date, string time, Address address)
    {
        Title = title;
        Description = description;
        Date = date;
        Time = time;
        Address = address;
    }

    public string GetStandardDetails()
    {
        return $"Title: {Title}\nDescription: {Description}\nDate: {Date}\nTime: {Time}\nAddress: {Address.GetFullAddress()}";
    }

    public virtual string GetFullDetails()
    {
        return GetStandardDetails();
    }

    public string GetShortDescription()
    {
        return $"Type: {GetType().Name}\nTitle: {Title}\nDate: {Date}";
    }
}

class Lecture : Event
{
    public string Speaker { get; private set; }
    public int Capacity { get; private set; }

    public Lecture(string title, string description, string date, string time, Address address, string speaker, int capacity)
        : base(title, description, date, time, address)
    {
        Speaker = speaker;
        Capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nSpeaker: {Speaker}\nCapacity: {Capacity}";
    }
}

class Reception : Event
{
    public string RsvpEmail { get; private set; }

    public Reception(string title, string description, string date, string time, Address address, string rsvpEmail)
        : base(title, description, date, time, address)
    {
        RsvpEmail = rsvpEmail;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nRSVP Email: {RsvpEmail}";
    }
}

class OutdoorGathering : Event
{
    public string Weather { get; private set; }

    public OutdoorGathering(string title, string description, string date, string time, Address address, string weather)
        : base(title, description, date, time, address)
    {
        Weather = weather;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nWeather: {Weather}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        var address = new Address("789 Pine St", "Metropolis", "NY", "USA");

        var lecture = new Lecture("Python Programming", "Learn Python", "2023-07-21", "10:00 AM", address, "Dr. Smith", 100);
        var reception = new Reception("Company Meet and Greet", "Annual event", "2023-08-15", "6:00 PM", address, "rsvp@company.com");
        var outdoor = new OutdoorGathering("Summer Picnic", "Join us for a fun day", "2023-06-30", "1:00 PM", address, "Sunny");

        var events = new List<Event> { lecture, reception, outdoor };

        foreach (var evt in events)
        {
            Console.WriteLine(evt.GetStandardDetails());
            Console.WriteLine(evt.GetFullDetails());
            Console.WriteLine(evt.GetShortDescription());
            Console.WriteLine();
        }
    }
}
