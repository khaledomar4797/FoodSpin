using FoodSpin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FoodSpin.Services
{
    public class OrderService
    {
        public OrderService()
        {

        }

        public static Order GetPreviousOrder(HttpContextBase context)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Orders.FirstOrDefault(o => o.Username == context.User.Identity.Name);

                return entity;
            }
        }

        public async Task AddOrder(Order order)
        {
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Orders.Add(order);
                await ctx.SaveChangesAsync();
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
