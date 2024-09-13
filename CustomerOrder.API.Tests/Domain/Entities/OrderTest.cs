using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Tests.Domain.Entities;

public class OrderTest
{
    private readonly Order _order;

    public OrderTest()
    {
        _order = new Order(2, "description", 1.99);
    }

    [Fact]
    public void ItHasAnIdTest()
    {
        Assert.Equal(0, _order.Id);
    }

    [Fact]
    public void ItHasADescriptionTest()
    {
        Assert.Same("description", _order.Description);
    }

    [Fact]
    public void ItHasAPriceTest()
    {
        Assert.Equal(1.99, _order.Price);
    }

    [Fact]
    public void ItHasACreationDateTest()
    {
        Assert.IsType<DateTime>(_order.CreationDate);
    }

    [Fact]
    public void ItHasACustomerIdTest()
    {
        Assert.Equal(2, _order.CustomerId);
    }

    [Fact]
    public void ItHasACustomerTest()
    {
        Assert.Null(_order.Customer);
    }

    [Fact]
    public void ItHasAStatusTest()
    {
        Assert.Equal(OrderStatus.CREATED, _order.Status);
    }

    [Fact]
    public void ItCanSetAnIdTest()
    {
        _order.Id = 2;
        Assert.Equal(2, _order.Id);
    }

    [Fact]
    public void ItCanSetADescriptionTest()
    {
        _order.Description = "description_2";
        Assert.Same("description_2", _order.Description);
    }

    [Fact]
    public void ItCanSetAPriceTest()
    {
        _order.Price = 2.99;
        Assert.Equal(2.99, _order.Price);
    }

    [Fact]
    public void ItCanSetACreationDateTest()
    {
        _order.CreationDate = new DateTime(1999, 1, 1);
        Assert.Equal(new DateTime(1999, 1, 1), _order.CreationDate);
    }

    [Fact]
    public void ItCanSetACustomerIdTest()
    {
        _order.CustomerId = 3;
        Assert.Equal(3, _order.CustomerId);
    }

    [Fact]
    public void ItCanSetACustomerTest()
    {
        var expected = new Customer("", "", "");
        _order.Customer = expected;
        Assert.Equal(expected, _order.Customer);
    }

    [Fact]
    public void ItCanSetAStatusTest()
    {
        _order.Status = OrderStatus.CANCELLED;
        Assert.Equal(OrderStatus.CANCELLED, _order.Status);
    }
}