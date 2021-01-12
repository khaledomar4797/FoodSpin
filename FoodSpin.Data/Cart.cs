using System.ComponentModel.DataAnnotations;

namespace FoodSpin.Data
{
    public class Cart
    {
        [Key]
        public int ID { get; set; }
        public string CartId { get; set; }
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
