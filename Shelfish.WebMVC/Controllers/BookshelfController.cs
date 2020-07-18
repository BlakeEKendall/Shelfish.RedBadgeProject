using Microsoft.AspNet.Identity;
using Shelfish.Data;
using Shelfish.Models.BookshelfModels;
using Shelfish.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

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

        // Links on Details page can take me to Controls below.

        // GET: BOOK & ADD TO LIST -- Needs its own view page as well --> Find and add book from dropdown
        // POST: BOOK TO SHELF LIST -- Submits change, and return (RedirectToAction to GET: Details page after posted?)

        // GET: BooksToAdd
        public ActionResult AddBooks(int id)
        {
            var svc = CreateBookshelfService();
            var model = svc.GetBookshelfById(id);
            var books = new ApplicationDbContext().Books.ToList();
            ViewBag.BookId = new SelectList(books, "BookId", "Title");
            return View(model);
        }

        //THis part is still not working: sometimes error says that dropdown contains no id/value? Even though I can see the dropdown list in the view just fine.
        // POST: BooksToAdd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBooks(int bookId, int shelfId)
        {
            var service = CreateBookshelfService();

            if (service.AddBookToShelf(bookId, shelfId))
            {
                TempData["SaveResult"] = "Your book was added to the shelf.";
                return RedirectToAction("Details");
            }

            ModelState.AddModelError("", "Your book could not be added.");
            return View();
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