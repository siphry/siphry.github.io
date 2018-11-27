using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using assignment8.Models;

namespace assignment8.Controllers
{
    public class HomeController : Controller
    {
        private AuctionContext db = new AuctionContext();
        // GET: Home
        public ActionResult Index()
        {
            var recent10Bids = db.Bids.OrderByDescending(bid => bid.Timestamp).Take(10).ToList();
            return View(recent10Bids);
        }
    }
}