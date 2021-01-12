using System.ComponentModel.DataAnnotations;

namespace FoodSpin.Models.Order
{
    public class OrderListItem
    {
        public int OrderId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal Total { get; set; }

        [Display(Name = "Order Date")]
        public System.DateTime OrderDate { get; set; }
    }
}
