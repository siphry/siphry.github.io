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
        private RequestContext db = new RequestContext();

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

        // POST: Requests/Maitanence
        /// <summary>
        /// POSTS new requests from the maintanence forms page to the database list
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        // GET: Requests
        /// <summary>
        /// Creates an ordered list of the database entities
        /// </summary>
        /// <returns></returns>
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