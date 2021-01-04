using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodSpin.WebMVC.Models
{
    public class CartRemoveViewModel
    {
        public decimal CartTotalPrice { get; set; }
        public int CartTotalProducts { get; set; }
        public int NumberOfProductsInCart { get; set; }
        public int DeleteId { get; set; }
    }
}