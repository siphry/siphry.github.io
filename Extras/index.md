# HW8 and HW9 Code and Steps

## Setting up the Database and CRUD
1. Create MDF file and connect to server.
2. Create a table into connected database.
3. Use auto generator to create up script -- BUILD. 
4. Run up script, use generated tables to scaffold models and context.
5. Do not move Context to DAL folder!!
6. Scaffold the CRUD for whatever model required.
7. Write the rest of the requirements as needed. 
8. To publish: 
    *Change Azure DB Connection String name to name in local Webconfig.
    *Rebuild
    *Connect to Azure DB in VS
    *Run up script on Azure DB
    *Publish!

## Code Snippets  

### Up and down scripts:

```sql
CREATE TABLE [dbo].[Buyers]
(
	[BuyerId] INT IDENTITY (0, 1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(128) NOT NULL
)

CREATE TABLE [dbo].[Sellers]
(
	[SellerId] INT IDENTITY (0, 1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(128) NOT NULL
)

CREATE TABLE [dbo].[Items]
(
	[ItemId] INT IDENTITY (1001, 1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(100) NOT NULL, 
	[Seller] INT NOT NULL,
    CONSTRAINT [FK_Items_Sellers] FOREIGN KEY ([Seller]) REFERENCES [Sellers]([SellerId])
)

CREATE TABLE [dbo].[Bids]
(	
	[Item] INT NOT NULL,
    CONSTRAINT [FK_Bids_Items] FOREIGN KEY ([Item]) REFERENCES [Items]([ItemId]),
	[Buyer] INT NOT NULL,
    CONSTRAINT [FK_Bids_Buyers] FOREIGN KEY ([Buyer]) REFERENCES [Buyers]([BuyerId]), 
    [Price] INT NOT NULL, 
    [Timestamp] DATETIME NOT NULL
)

INSERT INTO [dbo].[Buyers](Name) VALUES
('Jane Stone'),
('Tom McMasters'),
('Otto Vanderwall');

INSERT INTO [dbo].[Sellers](Name) VALUES
('Gayle Hardy'),
('Lyle Banks'),
('Pearl Greene');

INSERT INTO [dbo].[Items](Name, Description, Seller) VALUES
('Abraham Lincoln Hammer','A bench mallet fashioned from a broken rail-splitting maul in 1829 and owned by Abraham Lincoln', 2),
('Albert Einsteins Telescope','A brass telescope owned by Albert Einstein in Germany, circa 1927', 0),
('Bob Dylan Love Poems','Five versions of an original unpublished, handwritten, love poem by Bob Dylan', 1);

INSERT INTO [dbo].[Bids](Item, Buyer, Price, Timestamp) VALUES
(1001, 2, 250000,'12/04/2017 09:04:22'),
(1003, 0, 95000,'12/04/2017 08:44:03');
```

```sql
ALTER TABLE [dbo].[Bids] DROP CONSTRAINT  [FK_Bids_Items];
ALTER TABLE [dbo].[Bids] DROP CONSTRAINT  [FK_Bids_Buyers];
ALTER TABLE [dbo].[Items] DROP CONSTRAINT  [FK_Items_Sellers];

-- Remove Table from OurItems database
DROP TABLE [dbo].[Buyers];

-- Remove Table from OurItems database
DROP TABLE [dbo].[Sellers];

-- Remove Table from OurItems database
DROP TABLE [dbo].[Items];

-- Remove Table from OurItems database
DROP TABLE [dbo].[Bids];
```

### Controllers

Home Controller:
```csharp
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
```

Items Controller:
```csharp
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using assignment8.Models;

namespace assignment8.Controllers
{
    public class ItemsController : Controller
    {
        private AuctionContext db = new AuctionContext();

        // GET: Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Seller1);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        public JsonResult Update(int id)
        {
            Item item = db.Items.Where(i => i.ItemId.Equals(id)).FirstOrDefault();
            Bid recent = item.Bids.LastOrDefault();
            var latestBid = new
            {
                buyer = recent.Buyer1.Name,
                price = recent.Price
            };
            
            return Json(latestBid, JsonRequestBehavior.AllowGet);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.Seller = new SelectList(db.Sellers, "SellerId", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,Name,Description,Seller")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Seller = new SelectList(db.Sellers, "SellerId", "Name", item.Seller);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.Seller = new SelectList(db.Sellers, "SellerId", "Name", item.Seller);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,Name,Description,Seller")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Seller = new SelectList(db.Sellers, "SellerId", "Name", item.Seller);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
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
```

Bids Controller:

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using assignment8.Models;

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
```

### Views

Main home index

```csharp
@model List<assignment8.Models.Bid>

@{
    ViewBag.Title = "REGINALD'S ANCIENT ANTIQUITIES AUCTION HOUSE";
}

<h2>WELCOME TO REGINALD'S ANCIENT ANTIQUITIES AUCTION HOUSE</h2>

<hr /><br />
<h4>Ten Most Recent Bids</h4>
<div class="detail_content">
    <table class="table">
        <tr>
            <th>
                Item ID
            </th>
            <th>
                Item Name
            </th>
            <th>
                Buyer
            </th>
            <th>
                Bid Amount
            </th>
            <th>
                Bid Time
            </th>
        </tr>
        @foreach (var recentBid in Model)
        {
        <tr>
            <td>
                @recentBid.Item1.ItemId
            </td>
            <td>
               @Html.ActionLink(recentBid.Item1.Name, "Details", "Items", new { id = recentBid.Item1.ItemId}, null)
            </td>
            <td>
                @recentBid.Buyer1.Name
            </td>
            <td>
                @recentBid.Price
            </td>
            <td>
                @if (recentBid.Timestamp.Date == DateTime.Today)
                {
                    DateTime today = recentBid.Timestamp;
                    @today.ToString("h:mm:ss tt")
                }
                else
                { @recentBid.Timestamp }
            </td>
        </tr>
        }
    </table>
</div>
```

Details View for Items

```csharp
@model assignment8.Models.Item

@{
    ViewBag.Title = "Item Details";
}

<h2>Item Details</h2>
<body onload="refresh();">
    <div class="row">
    <div class="col-md-6">
        <h4> @Html.DisplayFor(model => model.Name)</h4>
        <hr />
        <dl class="dl-horizontal">
            <dt>
                @Html.DisplayNameFor(model => model.Seller)
            </dt>

            <dd>
                @Html.DisplayFor(model => model.Seller1.Name)
            </dd>

            <dt>
                @Html.DisplayNameFor(model => model.Name)
            </dt>

            <dd>
                @Html.DisplayFor(model => model.Name)
            </dd>

            <dt>
                @Html.DisplayNameFor(model => model.Description)
            </dt>

            <dd>
                @Html.DisplayFor(model => model.Description)
            </dd>
        </dl>
        </div>
        <div class="col-md-6">
            <h4>Current Bids</h4><hr />
            <div class="table-responsive">
                <table class="table">
                    <tr>
                        <th>Bidder Name</th>
                        <th>Price</th>
                    </tr>
                    <tbody id="inner">
                        @for (int i = Model.Bids.Count() - 1; i >= 0; i--)
                        {
                            <tr>
                                <td>
                                    @Html.DisplayFor(model => model.Bids.ElementAt(i).Buyer1.Name)
                                </td>
                                <td id="price">
                                    @Html.DisplayFor(model => model.Bids.ElementAt(i).Price)
                                </td>
                            </tr>
                            }
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    <p>
        @Html.ActionLink("Edit", "Edit", new { id = Model.ItemId }) |
        @Html.ActionLink("Back to List", "Index")
    </p>
</body>

@section scriptSection{
    <script src="~/Scripts/jquery-3.3.1.min.js"></script>
    <script src="~/Scripts/update.js"></script>
}
```

### Javascript

Update script for Items Details

```javascript
//finds the item id from the url
var pageURL = window.location.href;
var id = pageURL.substr(pageURL.lastIndexOf('/') + 1);
var source = "/Items/Update/" + id
//gets the latest price from the item's bids for comparrison
var latestPrice = $("#price").html();

var ajax_call = function () {
    //jQuery ajax code
    $.ajax({
        method: "GET",
        dataType: "json",
        url: source,
        success: successUpdate,
        error: errorAjax
    })
};

function successUpdate(latestBid) {
    //add new bids to table
    if (latestBid.price > latestPrice) {
        $("#inner").prepend("<tr><td>" + latestBid.buyer + "</td><td>" + latestBid.price + "</td></tr>");
        latestPrice = latestBid.price;
    }
}

function errorAjax() {
    console.log("error in ajax");
}

function refresh() {
    var interval = 1000 * 5; // where X is your timer interval in X seconds
    window.setInterval(ajax_call, interval);
}
```

### Links
[Git Cheat Sheet](https://siphry.github.io/Cheats)  
[W3Schools - Razor](https://www.w3schools.com/ASP/webpages_razor.asp)  
[W3Schools - Javascript](https://www.w3schools.com/js/default.asp)  
[W3Schools - HTML](https://www.w3schools.com/html/default.asp)  
[Microsoft - Razor](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-2.1)  
[JQuery Cheat Sheet](https://oscarotero.com/jquery/)  
[Scot's Azure DB Video](http://www.wou.edu/~morses/classes/cs46x/lecture/Videos.html)  
