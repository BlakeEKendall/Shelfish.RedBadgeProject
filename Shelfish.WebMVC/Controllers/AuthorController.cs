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


        public ActionResult Details(int id)
        {
            var svc = new AuthorService();
            var model = svc.GetAuthorById(id);

            return View(model);
        }


        public ActionResult Edit(int id)
        {
            AuthorDetail detail = AuthorDetailService(id);
            var model =
                new AuthorEdit
                {
                    AuthorId = detail.AuthorId,
                    Name = detail.Name,
                    CountryName = detail.CountryName
                };
            return View(model);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AuthorEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.AuthorId != id)
            {
                ModelState.AddModelError("", "Id Mismatch. Please try again.");
                return View(model);
            }

            var service = new AuthorService();

            if (service.UpdateAuthor(model))
            {
                TempData["SaveResult"] = "Your author was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your author could not be updated.");
            return View(model);
        }


        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            AuthorDetail detail = AuthorDetailService(id);

            return View(detail);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = new AuthorService();

            service.DeleteAuthor(id);

            TempData["SaveResult"] = "Your author was deleted";

            return RedirectToAction("Index");
        }

        private static AuthorDetail AuthorDetailService(int id)
        {
            var service = new AuthorService();
            var detail = service.GetAuthorById(id);
            return detail;
        }
    }
}