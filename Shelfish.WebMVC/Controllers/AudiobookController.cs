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
            return View();
        }
    }
}