using FoodSpin.Data;
using FoodSpin.Models.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FoodSpin.Services
{
    public class ProductService : IProductService
    {
        private readonly Guid _userId;

        public ProductService(Guid userId)
        {
            _userId = userId;
        }

        public ProductService()
        {

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
                    ProductCategory = (Data.Category)model.ProductCategory,
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
                        .Select(
                            p =>
                                new ProductListItem
                                {
                                    ProductId = p.ProductId,
                                    ProductName = p.ProductName,
                                    ProductDescription = p.ProductDescription,
                                    ProductPrice = p.ProductPrice,
                                    ProductQuantity = p.ProductQuantity
                                }
                        ).ToListAsync();

                return query;
            }
        }

        public async Task<ProductDetail> GetProductByIdAsync(int? id)
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
                        ProductImage = entity.ProductImage,
                        ProductQuantity = entity.ProductQuantity,
                        ProductCategory = (Models.Product.Category)entity.ProductCategory
                    };
            }
        }

        public IEnumerable<ProductListItem> GetProductByCategory(string category)
        {
            using (var ctx = new ApplicationDbContext())
            {
                Data.Category categoryName = (Data.Category)Enum.Parse(typeof(Data.Category), category, true);
                var query =
                        ctx
                        .Products
                        .Where(p => p.ProductCategory == categoryName)
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
                        ).ToList();

                return query;
            }
        }

        public async Task<bool> UpdateProductAsync(ProductEdit model)
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

        public async Task<bool> DeleteProductAsync(int productId)
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
