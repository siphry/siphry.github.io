using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using assignment8.Models;
using System.Data.Entity;
using System.Data;

namespace assignment8.Controllers
{
    public class BidsController : Controller
    {
        private AuctionContext db = new AuctionContext();

         // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.Buyer = new SelectList(db.Buyers, "BuyerId", "Name");
            ViewBag.Item = new SelectList(db.Items, "ItemId", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Item,Buyer,Price,Timestamp")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                //controll to make sure only bids higher than current bid can be made
                Item item = db.Items.Where(i => i.ItemId.Equals(bid.Item)).FirstOrDefault();
                Bid recent = item.Bids.LastOrDefault();
                if(recent == null || bid.Price > recent.Price)
                {
                    db.Bids.Add(bid);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Items");
                }
                else
                {
                    ViewBag.Buyer = new SelectList(db.Buyers, "BuyerId", "Name", bid.Buyer);
                    ViewBag.Item = new SelectList(db.Items, "ItemId", "Name", bid.Item);
                    ModelState.AddModelError("Price", "A greater bid already exists. Please bid a value greater than: " + recent.Price);
                    return View(bid);
                }
            }
            
            ViewBag.Buyer = new SelectList(db.Buyers, "BuyerId", "Name", bid.Buyer);
            ViewBag.Item = new SelectList(db.Items, "ItemId", "Name", bid.Item);
            return View(bid);
        }
    }
}