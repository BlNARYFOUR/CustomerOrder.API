namespace CustomerOrder.API.Application.Dtos;

public class OrderGet(
    int id,
    int customerId,
    string description,
    double price,
    DateTime creationDate
) {
    public int Id { get; set; } = id;
    public int CustomerId { get; set; } = customerId;
    public string Description { get; set; } = description;
    public double Price { get; set; } = price;
    public DateTime CreationDate { get; set; } = creationDate;
}
