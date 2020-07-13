using Microsoft.AspNet.Identity;
using Shelfish.Models.ReviewModels;
using Shelfish.Services;
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
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ReviewService(userId);
            var model = service.GetReviews();

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
        public ActionResult Create(ReviewCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateReviewService();

            if (service.CreateReview(model))
            {
                TempData["SaveResult"] = "Your review was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Review could not be created.");

            return View(model);
        }


        // GET: Details
        public ActionResult Details(int id)
        {
            var svc = CreateReviewService();
            var model = svc.GetReviewById(id);

            return View(model);
        }


        // EDIT
        public ActionResult Edit(int id)
        {
            var service = CreateReviewService();
            var reviewToBeEdited = service.GetReviewById(id);
            var updatedReview =
                new ReviewEdit
                {
                    ReviewId = reviewToBeEdited.ReviewId,
                    Title = reviewToBeEdited.Title,
                    Content = reviewToBeEdited.Content
                };
            return View(updatedReview);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ReviewEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ReviewId != id)
            {
                ModelState.AddModelError("", "ID Mismatch, please try again.");
                return View(model);
            }

            var service = CreateReviewService();

            if (service.UpdateReview(model))
            {
                TempData["SaveResult"] = "Your review was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your review could not be updated.");
            return View(model);
        }

        // GET: Delete
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateReviewService();
            var model = svc.GetReviewById(id);

            return View(model);
        }

        // POST: Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateReviewService();

            service.DeleteReview(id);

            TempData["SaveResult"] = "Your review was deleted";

            return RedirectToAction("Index");
        }


        private ReviewService CreateReviewService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ReviewService(userId);
            return service;
        }
    }
}