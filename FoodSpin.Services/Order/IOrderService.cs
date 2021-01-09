using FoodSpin.Models.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodSpin.Services
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(OrderCreate model);
        Task<bool> DeleteOrderAsync(int orderId);
        Task<OrderDetail> GetOrderByIdAsync(int? id);
        Task<IEnumerable<OrderListItem>> GetOrdersAsync();
        Task<bool> UpdateOrderAsync(OrderEdit model);
    }
}