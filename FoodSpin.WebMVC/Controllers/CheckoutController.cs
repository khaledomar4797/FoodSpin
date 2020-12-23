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
        ApplicationDbContext storeDB = new ApplicationDbContext();

        //
        // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            var previousOrder = storeDB.Orders.FirstOrDefault(x => x.Username == User.Identity.Name);

            if (previousOrder != null) {
                var cart = ShoppingCart.GetCart(this.HttpContext);
                ViewBag.Cart = cart.GetCartProducts();
                return View(previousOrder);
            }
            else
            {
                return View();
            }
        }

        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public async Task<ActionResult> AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);
            try
            {
                order.Username = User.Identity.Name;
                order.Email = User.Identity.Name;
                order.OrderDate = DateTime.Now;
                
                //Process the order
                var cart = ShoppingCart.GetCart(this.HttpContext);
                order = cart.CreateOrder(order);

                //Save Order
                storeDB.Orders.Add(order);
                await storeDB.SaveChangesAsync();

                return RedirectToAction("Complete",
                    new { id = order.OrderId });

            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

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