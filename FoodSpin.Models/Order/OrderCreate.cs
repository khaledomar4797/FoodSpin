using System.ComponentModel.DataAnnotations;

namespace FoodSpin.Models.Order
{
    public class OrderCreate
    {
        public string Username { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        public string Country { get; set; }

        [Required]
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal Total { get; set; }

        [Display(Name = "Order Date")]
        public System.DateTime OrderDate { get; set; }
    }
}
