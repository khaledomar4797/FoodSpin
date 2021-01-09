using FoodSpin.Data;
using FoodSpin.Models.Order;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FoodSpin.Services
{
    public class OrderService : IOrderService
    {
        public OrderService()
        {

        }

        public async Task<bool> CreateOrderAsync(OrderCreate model)
        {
            var entity =
                new Order()
                {
                    Username = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    City = model.City,
                    State = model.State,
                    PostalCode = model.PostalCode,
                    Country = model.Country,
                    Phone = model.Phone,
                    Email = model.Email,
                    Total = model.Total,
                    OrderDate = model.OrderDate
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Orders.Add(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<IEnumerable<OrderListItem>> GetOrdersAsync()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    await ctx
                        .Orders
                        .Select(
                            o =>
                                new OrderListItem
                                {
                                    OrderId = o.OrderId,
                                    FirstName = o.FirstName,
                                    LastName = o.LastName,
                                    Phone = o.Phone,
                                    Email = o.Email,
                                    Total = o.Total,
                                    OrderDate = o.OrderDate
                                }
                        ).ToListAsync();

                return query.OrderByDescending(o => o.OrderDate);
            }
        }

        public async Task<Models.Order.OrderDetail> GetOrderByIdAsync(int? id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    await ctx
                        .Orders
                        .Where(o => o.OrderId == id)
                        .FirstOrDefaultAsync();
                return
                    new Models.Order.OrderDetail
                    {
                        OrderId = entity.OrderId,
                        Username = entity.Username,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Address = entity.Address,
                        City = entity.City,
                        State = entity.State,
                        PostalCode = entity.PostalCode,
                        Country = entity.Country,
                        Phone = entity.Phone,
                        Email = entity.Email,
                        Total = entity.Total,
                        OrderDate = entity.OrderDate,
                        OrderDetails = entity.OrderDetails.Where(or => or.OrderId == entity.OrderId).Select(o => new OrderProductList 
                        {
                            ProductName = o.Product.ProductName,
                            ProductPrice = o.UnitPrice
                        }).ToList()
                    };
            }
        }

        public async Task<bool> UpdateOrderAsync(OrderEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    await ctx
                        .Orders
                        .Where(p => p.OrderId == model.OrderId)
                        .FirstOrDefaultAsync();

                entity.Username = model.Username;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Address = model.Address;
                entity.City = model.City;
                entity.State = model.State;
                entity.PostalCode = model.PostalCode;
                entity.Country = model.Country;
                entity.Phone = model.Phone;
                entity.Email = model.Email;
                entity.Total = model.Total;
                entity.OrderDate = model.OrderDate;

                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    await ctx
                        .Orders
                        .Where(p => p.OrderId == orderId)
                        .FirstOrDefaultAsync();

                ctx.Orders.Remove(entity);

                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public static Order GetPreviousOrder(HttpContextBase context)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Orders.FirstOrDefault(o => o.Username == context.User.Identity.Name);

                return entity;
            }
        }

        public static bool IsValid(int id, HttpContextBase context)
        {
            using (var ctx = new ApplicationDbContext())
            {
                bool isValid = ctx.Orders.Any(
                o => o.OrderId == id &&
                o.Username == context.User.Identity.Name);

                return isValid;
            }
        }
    }
}
