using System.ComponentModel.DataAnnotations;

namespace FoodSpin.Models.Product
{
    public class ProductEdit
    {
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }

        [Display(Name = "Product Price")]
        public decimal ProductPrice { get; set; }

        [Display(Name = "Product Category")]
        public Category ProductCategory { get; set; }

        [Display(Name = "Product Image")]
        public string ProductImage { get; set; }

        [Display(Name = "Product Quantity")]
        public int ProductQuantity { get; set; }
    }
}
