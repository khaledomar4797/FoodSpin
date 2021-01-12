using FoodSpin.Data;
using System.Collections.Generic;

namespace FoodSpin.WebMVC.Models
{
    public class CartViewModel
    {
        public List<Cart> CartProductsList { get; set; }
        public decimal CartTotalPrice { get; set; }
    }
}