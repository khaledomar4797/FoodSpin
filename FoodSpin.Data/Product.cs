using System;
using System.ComponentModel.DataAnnotations;

namespace FoodSpin.Data
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

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
