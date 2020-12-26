using FoodSpin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodSpin.WebMVC.Models
{
    public class CartViewModel
    {
        public List<Cart> CartProductsList { get; set; }
        public decimal CartTotalPrice { get; set; }
    }
}