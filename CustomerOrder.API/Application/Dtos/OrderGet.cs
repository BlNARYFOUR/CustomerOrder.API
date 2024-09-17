namespace CustomerOrder.API.Application.Dtos;

public class OrderGet(
    int id,
    int customerId,
    string description,
    double price,
    DateTime creationDate,
    string status
) {
    public int Id { get; } = id;
    public int CustomerId { get; } = customerId;
    public string Description { get; } = description;
    public double Price { get; } = price;
    public DateTime CreationDate { get; } = creationDate;
    public string Status { get; } = status;
}
