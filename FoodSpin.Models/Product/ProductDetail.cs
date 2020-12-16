using System.ComponentModel.DataAnnotations;

namespace FoodSpin.Models.Product
{
    public class ProductDetail
    {
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }

        [Display(Name = "Product Price")]
        public decimal ProductPrice { get; set; }

        [Display(Name = "Product Image")]
        public string ProductImage { get; set; }
    }
}
