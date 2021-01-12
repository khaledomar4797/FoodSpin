using FoodSpin.Services.Dashboard;
using System.Web.Mvc;

namespace FoodSpin.WebMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            var stats = _dashboardService.GetStatistics();

            ViewBag.OrdersCount = stats["OrdersCount"];
            ViewBag.UsersCount = stats["UsersCount"];
            ViewBag.LowStock = stats["LowStock"];

            ViewBag.PieDataPoints = _dashboardService.GetTopCategories();
            ViewBag.LineDataPoints = _dashboardService.GetWeekSales();

            return View();
        }
    }
}