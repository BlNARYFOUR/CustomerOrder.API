namespace CustomerOrder.API.Application.Dtos;

public class Customer(
    int id,
    string firstName,
    string lastName,
    string email,
    int numberOfOrders
) {
    public int Id { get; set; } = id;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Email { get; set; } = email;
    public int NumberOfOrders { get; set; } = numberOfOrders;

}
