using Shelfish.Models.ReviewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shelfish.WebMVC.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        // GET: Review
        public ActionResult Index()
        {
            return View();
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewCreate model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }
    }
}