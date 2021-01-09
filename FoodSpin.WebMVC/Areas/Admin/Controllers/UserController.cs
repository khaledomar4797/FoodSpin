using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodSpin.Data;
using FoodSpin.Services.User;
using FoodSpin.Models.User;

namespace FoodSpin.WebMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Admin/User
        public async Task<ActionResult> Index()
        {
            var model = await _userService.GetUsersAsync();
            return View(model);
        }

        // GET: Admin/User/Details/{id}
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = await _userService.GetUserByIdAsync(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Admin/User/Edit/{id}
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            var model =
                new UserEdit()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };

            return View(model);
        }

        // POST: Admin/User/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserEdit model)
        {
            if (ModelState.IsValid)
            {
                if (await _userService.UpdateUserAsync(model))
                {
                    TempData["SaveResult"] = "Your user was updated.";
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Your user could not be updated.");

            return View(model);
        }

        // GET: Admin/User/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            await _userService.DeleteUserAsync(id);

            return RedirectToAction("Index");
        }
    }
}
