using FoodSpin.Data;
using FoodSpin.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OpenOrderFramework.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        // GET: /Checkout/Index
        public ActionResult Index()
        {
            var previousOrder = OrderService.GetPreviousOrder(this.HttpContext);
            var cart = CartService.GetCart(this.HttpContext);
            ViewBag.Cart = cart.GetCartProducts();
            
            if (previousOrder != null) {
                
                return View(previousOrder);
            }
            else
            {
                return View();
            }
        }

        // POST: /Checkout/PlaceOrder
        [HttpPost]
        public ActionResult PlaceOrder(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);
            try
            {
                order.Username = User.Identity.Name;
                order.Email = User.Identity.Name;
                order.OrderDate = DateTime.Now;
                
                var cart = CartService.GetCart(this.HttpContext);
                order = cart.CreateOrder(order);

                return RedirectToAction("Complete", new { id = order.OrderId });
            }
            catch
            {
                return View(order);
            }
        }

        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            bool isValid = OrderService.IsValid(id, this.HttpContext);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}