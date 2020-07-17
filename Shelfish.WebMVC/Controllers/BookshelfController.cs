using Microsoft.AspNet.Identity;
using Shelfish.Models.BookshelfModels;
using Shelfish.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shelfish.WebMVC.Controllers
{
    [Authorize]
    public class BookshelfController : Controller
    {
        // GET: Bookshelf
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BookshelfService(userId);
            var model = service.GetBookshelves();

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
        public ActionResult Create(BookshelfCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateBookshelfService();

            if (service.CreateBookshelf(model))
            {
                TempData["SaveResult"] = "Your bookshelf was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Bookshelf could not be created.");

            return View(model);
        }

        // GET: Details
        public ActionResult Details(int id)
        {
            var svc = CreateBookshelfService();
            var model = svc.GetBookshelfById(id);

            return View(model);
        }

        // GET: Edit
        public ActionResult Edit(int id)
        {
            var service = CreateBookshelfService();
            var shelfToBeEdited = service.GetBookshelfById(id);
            var updatedBookshelf =
                new BookshelfEdit
                {
                    ShelfId = shelfToBeEdited.ShelfId,
                    ShelfName = shelfToBeEdited.ShelfName,
                    BooksOnSHelf = shelfToBeEdited.BooksOnShelf
                };
            return View(updatedBookshelf);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookshelfEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ShelfId != id)
            {
                ModelState.AddModelError("", "ID Mismatch, please try again.");
                return View(model);
            }

            var service = CreateBookshelfService();

            if (service.UpdateBookshelf(model))
            {
                TempData["SaveResult"] = "Your bookshelf was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your bookshelf could not be updated.");
            return View(model);
        }

        // GET: Delete
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateBookshelfService();
            var model = svc.GetBookshelfById(id);

            return View(model);
        }

        // POST: Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateBookshelfService();

            service.DeleteBookshelf(id);

            TempData["SaveResult"] = "Your bookshelf was deleted";

            return RedirectToAction("Index");
        }


        private BookshelfService CreateBookshelfService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BookshelfService(userId);
            return service;
        }
    }
}