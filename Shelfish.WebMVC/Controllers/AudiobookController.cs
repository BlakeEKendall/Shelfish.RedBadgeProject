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
    }
}