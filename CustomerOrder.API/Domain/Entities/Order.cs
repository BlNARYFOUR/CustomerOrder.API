using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerOrder.API.Domain.Entities;

public class Order(string description, double price)
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
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public int CustomerId { get; set; }
    [ForeignKey("CustomerId")]
    public Customer? Customer { get; set; }
}
