using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeerOverflowWindowsApp;

namespace WebInterface.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAllBars()
        {
            var result = WebApiAccess.GetAllBars();
            return Json(result);
        }
    }
}