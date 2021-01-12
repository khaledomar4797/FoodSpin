using FoodSpin.Models.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodSpin.Services
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(ProductCreate model);
        Task<bool> DeleteProductAsync(int productId);
        IEnumerable<ProductListItem> GetProductByCategory(string category);
        Task<ProductDetail> GetProductByIdAsync(int? id);
        Task<IEnumerable<ProductListItem>> GetProductsAsync();
        Task<bool> UpdateProductAsync(ProductEdit model);
    }
}