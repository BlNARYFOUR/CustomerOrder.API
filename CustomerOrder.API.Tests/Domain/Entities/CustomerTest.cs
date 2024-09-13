using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Tests.Domain.Entities;

public class CustomerTest
{
    private readonly Customer _customer;

    public CustomerTest()
    {
        _customer = new Customer("first_name", "last_name", "email");
    }

    [Fact]
    public void ItHasAnIdTest()
    {
        Assert.Equal(0, _customer.Id);
    }

    [Fact]
    public void ItHasAFirstNameTest()
    {
        Assert.Same("first_name", _customer.FirstName);
    }

    [Fact]
    public void ItHasALastNameTest()
    {
        Assert.Same("last_name", _customer.LastName);
    }

    [Fact]
    public void ItHasAnEmailTest()
    {
        Assert.Same("email", _customer.Email);
    }

    [Fact]
    public void ItHasANumberOfOrdersTest()
    {
        Assert.Equal(0, _customer.NumberOfOrders);
    }

    [Fact]
    public void ItHasOrdersTest()
    {
        Assert.Equal([], _customer.Orders);
    }

    [Fact]
    public void ItCanSetAnIdTest()
    {
        _customer.Id = 2;
        Assert.Equal(2, _customer.Id);
    }

    [Fact]
    public void ItCanSetAFirstNameTest()
    {
        _customer.FirstName = "first_name_2";
        Assert.Same("first_name_2", _customer.FirstName);
    }

    [Fact]
    public void ItCanSetALastNameTest()
    {
        _customer.LastName = "last_name_2";
        Assert.Same("last_name_2", _customer.LastName);
    }

    [Fact]
    public void ItCanSetAnEmailTest()
    {
        _customer.Email = "email_2";
        Assert.Same("email_2", _customer.Email);
    }

    [Fact]
    public void ItCanSetANumberOfOrdersTest()
    {
        _customer.NumberOfOrders = 2;
        Assert.Equal(2, _customer.NumberOfOrders);
    }

    [Fact]
    public void ItCanSetOrdersTest()
    {
        ICollection<Order> expected = [new Order(1, "description", 1.23)];
        _customer.Orders = expected;
        Assert.Equal(expected, _customer.Orders);
    }
}