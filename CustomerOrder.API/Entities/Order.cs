using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerOrder.API.Entities
{
    public class Order(string description, float price)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; } = description;
        [Required]
        public float Price { get; set; } = price;
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public Customer? Customer { get; set; }
    }
}
