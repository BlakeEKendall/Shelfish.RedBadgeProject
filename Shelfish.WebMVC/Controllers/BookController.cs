using Shelfish.Data;
using Shelfish.Models.BookModels;
using Shelfish.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shelfish.WebMVC.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            var service = new BookService();
            var model = service.GetBooks();

            return View(model);
        }


        // GET: BookCreate
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = new BookService();

            if (service.CreateBook(model))
            {
                TempData["SaveResult"] = "Your book was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Book could not be created.");

            return View(model);
        }

        // GET: Details
        public ActionResult Details(int id)
        {
            var svc = new BookService();
            var model = svc.GetBookById(id);

            return View(model);
        }

        // GET: Edit
        public ActionResult Edit(int id)
        {
            BookDetail detail = GetBookService(id);
            var model =
                new BookEdit
                {
                    BookId = detail.BookId,
                    Title = detail.Title,
                    SeriesTitle = detail.SeriesTitle,
                    Isbn = detail.Isbn,
                    Rating = detail.Rating,
                    Genre = detail.Genre,
                    Language = detail.Language,
                    Publisher = detail.Publisher,
                    IsEbook = detail.IsEbook
                };
            return View(model);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookEdit bookToBeEdited)
        {
            if (!ModelState.IsValid) return View(bookToBeEdited);

            if (bookToBeEdited.BookId != id)
            {
                ModelState.AddModelError("", "ID does not match an existing item, please try again.");
                return View(bookToBeEdited);
            }

            var service = new BookService();

            if (service.UpdateBook(bookToBeEdited))
            {
                TempData["SaveResult"] = "Your book was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your book could not be updated.");
            return View(bookToBeEdited);
        }

        // GET: Delete
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            BookDetail bookToBeDeleted = GetBookService(id);

            return View(bookToBeDeleted);
        }

        // POST: Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = new BookService();

            service.DeleteBook(id);

            TempData["SaveResult"] = "Your book was deleted";

            return RedirectToAction("Index");
        }


        private static BookDetail GetBookService(int id)
        {
            var service = new BookService();
            var detail = service.GetBookById(id);
            return detail;
        }
    }
}