using assignment5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace assignment5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //GET-POST-Redirect to GET
        [HttpGet]
        public ActionResult Maintanence()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Maintanence(Request request)
        {
            return View();
        }
    }
}