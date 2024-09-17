namespace CustomerOrder.API.Application.Dtos;

public class EmailWebhookUpsert(
    string token,
    string? description
) {
    public string Token { get; } = token;
    public string? Description { get; } = description;
}
