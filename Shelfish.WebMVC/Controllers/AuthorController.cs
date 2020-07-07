using Shelfish.Models.AuthorModels;
using Shelfish.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shelfish.WebMVC.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult Index()
        {
            var service = new AuthorService();
            var model = service.GetAuthors();

            return View(model);
        }

        // GET: Author
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthorCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = new AuthorService();

            if (service.CreateAuthor(model))
            {
                TempData["SaveResult"] = "Your author was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Author could not be created.");

            return View(model);
        }
    }
}