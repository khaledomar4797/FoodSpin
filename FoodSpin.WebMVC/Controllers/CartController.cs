using FoodSpin.Data;
using FoodSpin.Services;
using FoodSpin.WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FoodSpin.WebMVC.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart/Index
        public ActionResult Index()
        {
            var cart = CartService.GetCart(this.HttpContext);

            var viewModel = new CartViewModel
            {
                CartProductsList = cart.GetCartProducts(),
                CartTotalPrice = cart.GetCartTotalPrice()
            };

            return View(viewModel);
        }

        // Post: Cart/AddToCart/{id}
        [HttpPost]
        public async Task<ActionResult> AddToCart(int id)
        {
            var productService = new ProductService();
            var product = await productService.GetProductByIdAsync(id);

            var cart = CartService.GetCart(this.HttpContext);

            int numberOfProductsInCart = cart.AddToCart(product);

            var model = new CartRemoveViewModel
            {
                CartTotalPrice = cart.GetCartTotalPrice(),
                CartTotalProducts = cart.GetCartTotalProducts(),
                NumberOfProductsInCart = numberOfProductsInCart,
                DeleteId = id
            };

            return Json(model);

            // return RedirectToAction("Index");
        }

        // AJAX: Cart/RemoveFromCart/{id}
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = CartService.GetCart(this.HttpContext);

            int productCount = cart.RemoveFromCart(id);

            var model = new CartRemoveViewModel
            {
                CartTotalPrice = cart.GetCartTotalPrice(),
                CartTotalProducts = cart.GetCartTotalProducts(),
                NumberOfProductsInCart = productCount,
                DeleteId = id
            };

            return Json(model);
        }

        // GET: Cart/CartStatus
        [ChildActionOnly]
        public ActionResult CartStatus()
        {
            var cart = CartService.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCartTotalProducts();

            return PartialView("CartStatus");
        }
    }
}