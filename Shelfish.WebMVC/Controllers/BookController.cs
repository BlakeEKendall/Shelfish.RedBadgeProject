using Shelfish.Models.BookModels;
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
            var model = new BookListItem[0];
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
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }
    }
}