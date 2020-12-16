using FoodSpin.Data;
using FoodSpin.Models.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSpin.Services
{
    public class ProductService
    {
        private readonly Guid _userId;

        public ProductService (Guid userId)
        {
            _userId = userId;
        }

        public async Task<bool> CreateProductAsync(ProductCreate model)
        {
            var entity =
                new Product()
                {
                    OwnerId = _userId,
                    ProductName = model.ProductName,
                    ProductDescription = model.ProductDescription,
                    ProductPrice = model.ProductPrice,
                    ProductCategory = (Data.Category) model.ProductCategory,
                    ProductImage = model.ProductImage,
                    ProductQuantity = model.ProductQuantity
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Products.Add(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<IEnumerable<ProductListItem>> GetProductsAsync()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    await ctx
                        .Products
                        .Where(p => p.OwnerId == _userId)
                        .Select(
                            p =>
                                new ProductListItem
                                {
                                    ProductId = p.ProductId,
                                    ProductName = p.ProductName,
                                    ProductDescription = p.ProductDescription,
                                    ProductPrice = p.ProductPrice,
                                    ProductImage = p.ProductImage
                                }
                        ).ToListAsync();

                return query;
            }
        }

        public async Task<ProductDetail> GetProductById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    await ctx
                        .Products
                        .Where(p => p.ProductId == id)
                        .FirstOrDefaultAsync();
                return
                    new ProductDetail
                    {
                        ProductId = entity.ProductId,
                        ProductName = entity.ProductName,
                        ProductDescription = entity.ProductDescription,
                        ProductPrice = entity.ProductPrice,
                        ProductImage = entity.ProductImage
                    };
            }
        }

        public async Task<bool> UpdateProduct(ProductEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    await ctx
                        .Products
                        .Where(p => p.ProductId == model.ProductId)
                        .FirstOrDefaultAsync();

                entity.ProductName = model.ProductName;
                entity.ProductDescription = model.ProductDescription;
                entity.ProductPrice = model.ProductPrice;
                entity.ProductCategory = (Data.Category)model.ProductCategory;
                entity.ProductImage = model.ProductImage;
                entity.ProductQuantity = model.ProductQuantity;

                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    await ctx
                        .Products
                        .Where(p => p.ProductId == productId)
                        .FirstOrDefaultAsync();

                ctx.Products.Remove(entity);

                return await ctx.SaveChangesAsync() == 1;
            }
        }
    }
}
