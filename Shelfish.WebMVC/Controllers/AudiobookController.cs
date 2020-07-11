using Shelfish.Models.AudiobookModels;
using Shelfish.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shelfish.WebMVC.Controllers
{
    [Authorize]
    public class AudiobookController : Controller
    {
        // GET: Audiobook
        public ActionResult Index()
        {
            var service = new AudiobookService();
            var model = service.GetAudiobooks();

            return View(model);
        }

        // GET: AudiobookCreate
        public ActionResult Create()
        {
            return View();
        }

        // POST: AudiobookCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AudiobookCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = new AudiobookService();

            if (service.CreateAudiobook(model))
            {
                TempData["SaveResult"] = "Your audiobook was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Audiobook could not be created.");

            return View(model);
        }

        // GET: Details
        public ActionResult Details(int id)
        {
            var svc = new AudiobookService();
            var model = svc.GetAudiobookById(id);

            return View(model);
        }

        // GET: Edit
        public ActionResult Edit(int id)
        {
            AudiobookDetail detail = GetAudiobookService(id);
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


                    IsAbridged = detail.IsAbridged
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

        private static AudiobookDetail GetAudiobookService(int id)
        {
            var service = new AudiobookService();
            var detail = service.GetAudiobookById(id);
            return detail;
        }
    }
}