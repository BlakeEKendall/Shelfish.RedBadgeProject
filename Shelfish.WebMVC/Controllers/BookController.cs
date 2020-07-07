﻿using Shelfish.Models.BookModels;
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

        public ActionResult Details(int id)
        {
            var svc = new BookService();
            var model = svc.GetBookById(id);

            return View(model);
        }
    }
}