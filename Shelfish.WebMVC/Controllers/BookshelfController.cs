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

        //BOOKS
        // GET: BooksToAdd
        public ActionResult AddBooks(int id)
        {
            var svc = CreateBookshelfService();
            var model = svc.GetBookshelfById(id);
            
            var books = new ApplicationDbContext().Books.ToList();
            var bookList = new SelectList(books.Select(item => new SelectListItem
            {
                Text = item.Title,
                Value = item.BookId.ToString()
            }).ToList(), "Value", "Text");

            var viewModel = new AddBookToShelfViewModel()
            {
                SelectedShelfId = model.ShelfId,
                BookListItems = bookList,
            };

            return View(viewModel);
        }


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
            var bookList = new SelectList(books.Select(item => new SelectListItem
            {
                Text = item.Title,
                Value = item.BookId.ToString()
            }).ToList(), "Value", "Text");


            if (service.AddBookToShelf(model))
            {
                TempData["SaveResult"] = "Your book was added to the shelf.";
                return RedirectToAction("Details", new { id = model.SelectedShelfId});
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
        [ActionName("DeleteBook")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBookPost(int id)
        {
            var service = CreateBookshelfService();

            service.DeleteBookFromShelf(id);

            TempData["SaveResult"] = "Your book was deleted from the shelf";

            return RedirectToAction("Index");
        }

        
        //TODO: ADD VIEWS FOR AUDIOBOOKS!!!
        //AUDIOBOOKS
        // GET: AudiobooksToAdd
        public ActionResult AddAudiobooks(int id)
        {
            var svc = CreateBookshelfService();
            var model = svc.GetBookshelfById(id);

            var audiobooks = new ApplicationDbContext().Audiobooks.ToList();
            var audiobookList = new SelectList(audiobooks.Select(item => new SelectListItem
            {
                Text = item.Title,
                Value = item.AudiobookId.ToString()
            }).ToList(), "Value", "Text");

            var viewModel = new AddAudiobookToShelfViewModel()
            {
                SelectedShelfId = model.ShelfId,
                AudiobookListItems = audiobookList,
            };

            return View(viewModel);
        }


        // POST: AudiobooksToAdd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAudiobooks(int id, AddAudiobookToShelfViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.SelectedShelfId != id)
            {
                ModelState.AddModelError("", "ID Mismatch, please try again.");
                return View(model);
            }

            var service = CreateBookshelfService();
            var audiobooks = new ApplicationDbContext().Audiobooks.ToList();
            var audiobookList = new SelectList(audiobooks.Select(item => new SelectListItem
            {
                Text = item.Title,
                Value = item.AudiobookId.ToString()
            }).ToList(), "Value", "Text");


            if (service.AddAudiobookToShelf(model))
            {
                TempData["SaveResult"] = "Your audiobook was added to the shelf.";
                return RedirectToAction("Details", new { id = model.SelectedShelfId });
            }

            ModelState.AddModelError("", "Your audiobook could not be added.");
            return View(model);
        }

        // GET: AudiobooksOnShelf
        public ActionResult AudiobooksOnShelf(int id)
        {
            var svc = CreateBookshelfService();
            var model = svc.GetAudiobooksOnShelf(id);

            return View(model);
        }

        // GET: DeleteAudiobook
        public ActionResult DeleteAudiobook(int id)
        {
            var svc = CreateBookshelfService();
            var model = svc.GetSingleAudiobookOnShelf(id);

            return View(model);
        }

        // POST: DeleteAudiobook
        [HttpPost]
        [ActionName("DeleteAudiobook")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAudiobookPost(int id)
        {
            var service = CreateBookshelfService();

            service.DeleteAudiobookFromShelf(id);

            TempData["SaveResult"] = "Your audiobook was deleted from the shelf";

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