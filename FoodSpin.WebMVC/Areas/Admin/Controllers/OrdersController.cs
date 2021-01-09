using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using FoodSpin.Services;
using FoodSpin.Models.Order;

namespace FoodSpin.WebMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: Admin/Orders
        public async Task<ActionResult> Index()
        {
            var model = await _orderService.GetOrdersAsync();
            return View(model);
        }

        // GET: Admin/Orders/Details/{id}
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = await _orderService.GetOrderByIdAsync(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Admin/Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrderCreate model)
        {
            if (ModelState.IsValid)
            {
                if (await _orderService.CreateOrderAsync(model))
                {
                    TempData["SaveResult"] = "Your order was created.";

                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Order could not be created.");

            return View(model);
        }

        // GET: Admin/Orders/Edit/{id}
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            var model =
                new OrderEdit()
                {
                    OrderId = order.OrderId,
                    Username = order.Username,
                    FirstName = order.FirstName,
                    LastName = order.LastName,
                    Address = order.Address,
                    City = order.City,
                    State = order.State,
                    PostalCode = order.PostalCode,
                    Country = order.Country,
                    Phone = order.Phone,
                    Email = order.Email,
                    Total = order.Total,
                    OrderDate = order.OrderDate
                };

            return View(model);
        }

        // POST: Admin/Orders/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, OrderEdit model)
        {
            if (ModelState.IsValid)
            {
                if (await _orderService.UpdateOrderAsync(model))
                {
                    TempData["SaveResult"] = "Your order was updated.";
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Your order could not be updated.");

            return View(model);
        }

        // GET: Admin/Orders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _orderService.DeleteOrderAsync(id);

            return RedirectToAction("Index");
        }
    }
}
