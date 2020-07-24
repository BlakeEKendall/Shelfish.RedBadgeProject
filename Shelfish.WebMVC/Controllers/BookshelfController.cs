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
using System.Runtime.InteropServices.ComTypes;
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

        

        // GET: BOOK & ADD TO LIST -- Needs its own view page as well --> Find and add book from dropdown
        // POST: BOOK TO SHELF LIST -- Submits change, and return (RedirectToAction to GET: Details page after posted?)

        // GET: BooksToAdd
        public ActionResult AddBooks(int id)
        {
            var svc = CreateBookshelfService();
            var model = svc.GetBookshelfById(id);
            //var shelfRecord = new AddBookToShelfViewModel
            //{
            //    SelectedShelfId = model.ShelfId
            //};
            var books = new ApplicationDbContext().Books.ToList();
            ViewData["SelectedBookId"] = new SelectList(books, "BookId", "Title");
            return View();
        }

        //THis part is still not working: sometimes error says that dropdown contains no id/value? Even though I can see the dropdown list in the view just fine.

        // POST: BooksToAdd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBooks(int id, AddBookToShelfViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.SelectedShelfId != id)
            {
                ModelState.AddModelError("", "ID Mismatch, please try again.");
                return View(model);
            }

            var service = CreateBookshelfService();
            var books = new ApplicationDbContext().Books.ToList();
            ViewData["SelectedBookId"] = new SelectList(books, "BookId", "Title");
             
            if (service.AddBookToShelf(model))
            {
                TempData["SaveResult"] = "Your book was added to the shelf.";
                return RedirectToAction("Details");
            }

            ModelState.AddModelError("", "Your book could not be added.");
            return View(model);
           
        } 

        // GET: BooksOnShelf
        public ActionResult BooksOnShelf(int id)
        {
            var svc = CreateBookshelfService();
            var model = svc.GetBooksOnShelf(id);

            return View(model);
        }

        // GET: DeleteBook
        public ActionResult DeleteBook(int id)
        {
            var svc = CreateBookshelfService();
            var model = svc.GetSingleBookOnShelf(id);

            return View(model);
        }

        // POST: DeleteBook
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBookPost(int id)
        {
            var service = CreateBookshelfService();

            service.DeleteBookFromShelf(id);

            TempData["SaveResult"] = "Your book was deleted from the shelf";

            return RedirectToAction("Details");
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