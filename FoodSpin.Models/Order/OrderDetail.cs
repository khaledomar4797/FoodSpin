using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodSpin.Models.Order
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public string Username { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal Total { get; set; }

        [Display(Name = "Order Date")]
        public System.DateTime OrderDate { get; set; }

        [Display(Name = "List of products")]
        public List<OrderProductList> OrderDetails { get; set; }
    }
}
