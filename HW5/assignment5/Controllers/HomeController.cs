using assignment5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using assignment5.DAL;

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

        // POST: Requests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Maintanence([Bind(Include = "ID,FirstName,LastName,PhoneNum,AptName,UnitNum,Comments,Permission,SubmissionTime")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Request.Add(request);
                db.SaveChanges();
                return RedirectToAction("RequestList");
            }

            return View(request);
        }

        private RequestContext db = new RequestContext();

        // GET: Requests
        public ActionResult RequestList()
        {
            var list = db.Request.ToList();
            var orderedList = list.OrderBy(item => item.SubmissionTime);
            return View(orderedList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}