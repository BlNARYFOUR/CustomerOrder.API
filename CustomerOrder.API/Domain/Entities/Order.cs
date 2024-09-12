using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerOrder.API.Domain.Entities;

public class Order(int customerId, string description, double price)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Description { get; set; } = description;
    [Required]
    public double Price { get; set; } = price;
    [Required]
    [DataType(DataType.Date)]
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public int CustomerId { get; set; } = customerId;
    [ForeignKey("CustomerId")]
    public Customer? Customer { get; set; }
    [Required]
    [DefaultValue(OrderStatus.CREATED)]
    public OrderStatus Status { get; set; } = OrderStatus.CREATED;
}
