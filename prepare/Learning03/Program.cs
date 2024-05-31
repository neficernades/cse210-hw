using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; private set; }
    public string ProductId { get; private set; }
    public float Price { get; private set; }
    public int Quantity { get; private set; }

    public Product(string name, string productId, float price, int quantity)
    {
        Name = name;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }

    public float GetTotalCost()
    {
        return Price * Quantity;
    }
}

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

    public bool IsInUSA()
    {
        return Country.ToLower() == "usa";
    }

    public string GetFullAddress()
    {
        return $"{Street}\n{City}, {State}\n{Country}";
    }
}

class Customer
{
    public string Name { get; private set; }
    public Address Address { get; private set; }

    public Customer(string name, Address address)
    {
        Name = name;
        Address = address;
    }

    public bool IsInUSA()
    {
        return Address.IsInUSA();
    }
}

class Order
{
    private List<Product> Products { get; set; }
    public Customer Customer { get; private set; }

    public Order(List<Product> products, Customer customer)
    {
        Products = products;
        Customer = customer;
    }

    public float GetTotalCost()
    {
        float shippingCost = Customer.IsInUSA() ? 5 : 35;
        float totalProductCost = 0;
        foreach (var product in Products)
        {
            totalProductCost += product.GetTotalCost();
        }
        return totalProductCost + shippingCost;
    }

    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (var product in Products)
        {
            label += $"{product.Name} (ID: {product.ProductId})\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{Customer.Name}\n{Customer.Address.GetFullAddress()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        var address1 = new Address("123 Main St", "Springfield", "IL", "USA");
        var address2 = new Address("456 Elm St", "Toronto", "ON", "Canada");

        var customer1 = new Customer("John Doe", address1);
        var customer2 = new Customer("Jane Smith", address2);

        var product1 = new Product("Laptop", "A001", 999.99f, 1);
        var product2 = new Product("Mouse", "A002", 25.50f, 2);
        var product3 = new Product("Keyboard", "A003", 45.00f, 1);

        var order1 = new Order(new List<Product> { product1, product2 }, customer1);
        var order2 = new Order(new List<Product> { product2, product3 }, customer2);

        var orders = new List<Order> { order1, order2 };

        foreach (var order in orders)
        {
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine($"Total Cost: ${order.GetTotalCost():0.00}");
            Console.WriteLine();
        }
    }
}
