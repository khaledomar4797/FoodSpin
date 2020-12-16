using System.ComponentModel.DataAnnotations;

namespace FoodSpin.Models.Product
{
    public class ProductCreate
    {
        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }

        [Required]
        [Display(Name = "Product Price")]
        public decimal ProductPrice { get; set; }

        [Required]
        [Display(Name = "Product Category")]
        public Category ProductCategory { get; set; }

        [Required]
        [Display(Name = "Product Image")]
        public string ProductImage { get; set; }

        [Required]
        [Display(Name = "Product Quantity")]
        public int ProductQuantity { get; set; }
    }

    public enum Category
    {
        Breakfast,
        Lunch,
        Dinner
    }
}
