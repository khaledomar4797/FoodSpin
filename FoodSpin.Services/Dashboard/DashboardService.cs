using FoodSpin.Data;
using FoodSpin.Models.Dashboard;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace FoodSpin.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private ConcurrentDictionary<string, int> stats = new ConcurrentDictionary<string, int>();

        public ConcurrentDictionary<string, int> GetStatistics()
        {
            int ordersCount = GetOrdersCount();
            int usersCount = GetUsersCount();
            int lowStock = GetLowStockCount();

            stats.AddOrUpdate("OrdersCount", ordersCount, (k, v) => v);
            stats.AddOrUpdate("UsersCount", usersCount, (k, v) => v);
            stats.AddOrUpdate("LowStock", lowStock, (k, v) => v);

            return stats;
        }

        public string GetTopCategories()
        {
            var list = GetCategoriesPercentage();

            var jsonResult = JsonConvert.SerializeObject(list);

            return jsonResult;
        }

        public string GetWeekSales()
        {
            var list = WeekSales();

            var jsonResult = JsonConvert.SerializeObject(list);

            return jsonResult;
        }

        private int GetOrdersCount()
        {
            using (var ctx = new ApplicationDbContext())
            {
                int ordersCount = ctx.Orders.Count();

                return ordersCount;
            }
        }

        private int GetUsersCount()
        {
            using (var ctx = new ApplicationDbContext())
            {
                int usersCount = ctx.Users.Count();

                return usersCount;
            }
        }

        private int GetLowStockCount()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var lowStockCount = ctx.Products.Where(p => p.ProductQuantity < 10).Count();

                return lowStockCount;
            }
        }

        private List<DataPoint> GetCategoriesPercentage()
        {
            using (var ctx = new ApplicationDbContext())
            {
                List<DataPoint> dataPoints = new List<DataPoint>();

                int count = 0;

                foreach (Data.Category category in Enum.GetValues(typeof(Data.Category)))
                {
                    count = ctx.OrderDetails.Where(o => o.Product.ProductCategory == category).Count();
                    dataPoints.Add(new DataPoint(category.ToString(), count));
                }

                return dataPoints;
            }
        }

        private List<DataPoint> WeekSales()
        {
            using (var ctx = new ApplicationDbContext())
            {
                List<DataPoint> dataPoints = new List<DataPoint>();

                int count = 0;

                var week = Enumerable.Range(0, 7).Select(i => DateTime.Now.Date.AddDays(-i)).ToList();

                week.Reverse();

                foreach (var date in week)
                {
                    count = ctx.Orders
                        .Where(o =>
                        o.OrderDate.Day == date.Day &&
                        o.OrderDate.Month == date.Month &&
                        o.OrderDate.Year == date.Year).Count();

                    dataPoints.Add(new DataPoint(date.Date.ToString("dd/MM/yyyy"), count));
                }

                return dataPoints;
            }
        }
    }
}
