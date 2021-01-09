using System.Collections.Concurrent;

namespace FoodSpin.Services.Dashboard
{
    public interface IDashboardService
    {
        ConcurrentDictionary<string, int> GetStatistics();
        string GetTopCategories();
        string GetWeekSales();
    }
}