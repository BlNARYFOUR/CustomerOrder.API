namespace CustomerOrder.API.Application.Dtos;

public class OrderUpsert(
    string description,
    double price
) {
    public string Description { get; } = description;
    public double Price { get; } = price;
}
