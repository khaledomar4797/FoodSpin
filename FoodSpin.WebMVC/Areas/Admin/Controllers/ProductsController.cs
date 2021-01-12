using FoodSpin.Models.Product;
using FoodSpin.Services;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FoodSpin.WebMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Admin/Products
        public async Task<ActionResult> Index()
        {
            var model = await _productService.GetProductsAsync();
            return View(model);
        }

        // GET: Admin/Products/Details/{id}
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = await _productService.GetProductByIdAsync(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductCreate model)
        {
            if (ModelState.IsValid)
            {
                if (await _productService.CreateProductAsync(model))
                {
                    TempData["SaveResult"] = "Your product was created.";

                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Product could not be created.");

            return View(model);
        }

        // GET: Admin/Products/Edit/{id}
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            var model =
                new ProductEdit()
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    ProductPrice = product.ProductPrice,
                    ProductImage = product.ProductImage,
                    ProductQuantity = product.ProductQuantity,
                    ProductCategory = product.ProductCategory

                };

            return View(model);
        }

        // POST: Admin/Products/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProductEdit model)
        {
            if (ModelState.IsValid)
            {
                if (await _productService.UpdateProductAsync(model))
                {
                    TempData["SaveResult"] = "Your product was updated.";
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Your product could not be updated.");

            return View(model);
        }

        // GET: Admin/Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteProductAsync(id);

            return RedirectToAction("Index");
        }

    }
}
