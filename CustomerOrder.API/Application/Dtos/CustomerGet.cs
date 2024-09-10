namespace CustomerOrder.API.Application.Dtos;

public class CustomerGet(
    int id,
    string firstName,
    string lastName,
    string email,
    int numberOfOrders
) {
    public int Id { get; } = id;
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public string Email { get; } = email;
    public int NumberOfOrders { get; } = numberOfOrders;
}
