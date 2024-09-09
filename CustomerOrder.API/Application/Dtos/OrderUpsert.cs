namespace CustomerOrder.API.Application.Dtos;

public class OrderUpsert(
    int customerId,
    string description,
    double price
) {
    public int CustomerId { get; set; } = customerId;
    public string Description { get; set; } = description;
    public double Price { get; set; } = price;
}
