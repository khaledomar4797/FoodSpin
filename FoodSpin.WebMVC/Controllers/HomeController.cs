using FoodSpin.Services;
using System.Web.Mvc;

namespace FoodSpin.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Breakfast");
        }

        public ActionResult Breakfast()
        {
            var productService = new ProductService();
            string categoryName = "Breakfast";
            var model = productService.GetProductByCategory(categoryName);
            return View(model);
        }

        public ActionResult Lunch()
        {
            var productService = new ProductService();
            string categoryName = "Lunch";
            var model = productService.GetProductByCategory(categoryName);
            return View(model);
        }

        public ActionResult Dinner()
        {
            var productService = new ProductService();
            string categoryName = "Dinner";
            var model = productService.GetProductByCategory(categoryName);
            return View(model);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}