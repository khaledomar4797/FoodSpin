using FoodSpin.Models.Product;
using FoodSpin.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FoodSpin.WebMVC.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        // GET: Product
        public async Task<ActionResult> Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ProductService(userId);
            var model = await service.GetProductsAsync();

            return View(model);
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateProductService();

            if (await service.CreateProductAsync(model))
            {
                TempData["SaveResult"] = "Your product was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Product could not be created.");

            return View(model);
        }

        // GET: Details
        public async Task<ActionResult> Details(int id)
        {
            var service = CreateProductService();
            var model = await service.GetProductByIdAsync(id);

            return View(model);
        }

        // GET: Edit
        public async Task<ActionResult> Edit(int id)
        {
            var service = CreateProductService();
            var detail = await service.GetProductByIdAsync(id);

            var model =
                new ProductEdit()
                {
                    ProductId = detail.ProductId,
                    ProductName = detail.ProductName,
                    ProductDescription = detail.ProductDescription,
                    ProductPrice = detail.ProductPrice,
                    ProductImage = detail.ProductImage,
                    ProductQuantity = detail.ProductQuantity,
                    ProductCategory = detail.ProductCategory

                };

            return View(model);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProductEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ProductId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateProductService();

            if (await service.UpdateProductAsync(model))
            {
                TempData["SaveResult"] = "Your product was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your product could not be updated.");

            return View(model);
        }

        // GET: Delete
        [ActionName("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var service = CreateProductService();
            var model = await service.GetProductByIdAsync(id);

            return View(model);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePost(int id)
        {
            var service = CreateProductService();

            await service.DeleteProductAsync(id);

            TempData["SaveResult"] = "Your product was deleted";

            return RedirectToAction("Index");
        }

        private ProductService CreateProductService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ProductService(userId);
            return service;
        }
    }
}
