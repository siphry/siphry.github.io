using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using assignment6.Models;
using assignment6.Models.ViewModels;

namespace assignment6.Controllers
{
    public class HomeController : Controller
    {
        private WorldWideImportersContext db = new WorldWideImportersContext();
        List<PersonVM> searchResultDetails;

        [HttpGet]
        public ActionResult Index()
        {
            //request the query string value
            string searchName = Request.QueryString["searchname"];

            //if the search string is null or empty, return view (if edited in query string in url)
            if (searchName == null || searchName == "")
            {
                return View();
            }
            else
            {
                //generates a list of all clients whose full name contains inputted search value
                searchResultDetails = db.People.Where(p => p.FullName.Contains(searchName)).Where(p => p.PersonID != 1).Select(p => new PersonVM { FullName = p.FullName}).ToList();

                //adds message for searches with no results
                if (searchResultDetails.FirstOrDefault() == null)
                {
                    ViewBag.notFound = "No results found for: " + searchName;
                    string test = "No results found for: " + searchName;
              
                }
                //add message designating what was searched above the result list
                else if (searchName != null)
                {
                    ViewBag.search = "Search results for: " + searchName;
                   
                }
                ViewBag.notNull = 1;

                return View(searchResultDetails);
            }
        }
       [HttpGet]
       public ActionResult Details(string personName)
        {
            if(personName == null || personName == "")
            {
                return RedirectToAction("Index");
            }

            //initialize empty model
            var resultDetails = new FullDetailsVM();

            //basic details for non customers
            resultDetails.Person = db.People.Where(p => p.FullName == personName).Select(p => new PersonVM { PersonID = p.PersonID, FullName = p.FullName, PreferredName = p.PreferredName, PhoneNumber = p.PhoneNumber, FaxNumber = p.FaxNumber, EmailAddress = p.EmailAddress, ValidFrom = p.ValidFrom, Customer = p.Customers2 }).ToList();

            //if someone changes the query string to empty or different, redirects back to search page
            if(resultDetails.Person.FirstOrDefault() == null)
            {
                return RedirectToAction("Index");
            }

            //prevents 404 error
            if (resultDetails.Person.FirstOrDefault().Customer.Count() > 0)
            {
                //if searched client is a customer, fill in the rest of the details
                resultDetails.Customer = CompDetails(resultDetails.Person.First().PersonID);

                //if they are a customer, use their customer id to find information on their orders/profits/top 10 items
                resultDetails.OrderCount = OrdersCount(resultDetails.Customer.First().CustomerID);

                //total gross sales
                resultDetails.GrossSales = TotalGross(resultDetails.Customer.First().CustomerID);

                //total profit
                resultDetails.GrossProfit = TotalProfit(resultDetails.Customer.First().CustomerID);

                //top 10 most profitable items purchase
                resultDetails.Invoice = Top10Items(resultDetails.Customer.First().CustomerID);

                resultDetails.Latitude = Latitude(resultDetails.Customer.First().CustomerID);
                resultDetails.Longitude = Longitude(resultDetails.Customer.First().CustomerID);

            }

            return View(resultDetails);
        }

        /// <summary>
        /// Searches database for customer id, and if they are one return the company details
        /// </summary>
        /// <param name="ID">searched Customer's ID</param>
        /// <returns>Returns list of customers resulting from search via ID</returns>
        private List<CustomerVM> CompDetails(int ID)
        {
            var companyDetails = db.Customers
                                   .Where(c => c.PrimaryContactPersonID.Equals(ID))
                                   .Select(c => new CustomerVM
                                   {
                                       CustomerID = c.CustomerID,
                                       CustomerName = c.CustomerName,
                                       PhoneNumber = c.PhoneNumber,
                                       FaxNumber = c.FaxNumber,
                                       WebsiteURL = c.WebsiteURL,
                                       AccountStarted = c.AccountOpenedDate
                                   }).ToList();

            return companyDetails;
        }

        /// <summary>
        /// Returns the total amount of orders made by specific customer via their ID
        /// </summary>
        /// <param name="ID">searched Customer's ID</param>
        /// <returns>Total number of orders placed by customer</returns>
        private int OrdersCount(int ID)
        {
            int ordersCount = db.Orders
                                .Where(o => o.CustomerID.Equals(ID))
                                .Count();

            return ordersCount;
        }

        /// <summary>
        /// Returns the total gross profit of all orders made by searched customer via their ID
        /// </summary>
        /// <param name="ID">searched Customer's ID</param>
        /// <returns>Total gross of all orders made by searched customer</returns>
        private decimal TotalGross(int ID)
        {
            decimal profitGross = db.Orders
                               .Where(o => o.CustomerID.Equals(ID))
                               .SelectMany(i => i.Invoices)
                               .SelectMany(il => il.InvoiceLines)
                               .Sum(s => s.ExtendedPrice);

            return profitGross;
        }

        /// <summary>
        /// Returns total profit of all orders made by searched customer via their ID
        /// </summary>
        /// <param name="ID">searched Customer's ID</param>
        /// <returns>Total profit of all orders made by searched customer</returns>
        private decimal TotalProfit(int ID)
        {
            decimal profitTotal = db.Orders
                               .Where(o => o.CustomerID.Equals(ID))
                               .SelectMany(i => i.Invoices)
                               .SelectMany(il => il.InvoiceLines)
                               .Sum(s => s.LineProfit);

            return profitTotal;
        }

        private List<InvoiceVM> Top10Items(int ID)
        {
            var top10 = db.Orders
                          .Where(o => o.CustomerID.Equals(ID))
                          .SelectMany(i => i.Invoices)
                          .SelectMany(il => il.InvoiceLines)
                          .OrderByDescending(lp => lp.LineProfit)
                          .Take(10)
                          .Select(il => new InvoiceVM
                          {
                              StockID = il.StockItemID,
                              Description = il.Description,
                              TotalProfit = il.LineProfit,
                          }).ToList();

            var salesPerson = db.Orders
                          .Where(o => o.CustomerID.Equals(ID))
                          .SelectMany(i => i.Invoices)
                          .SelectMany(il => il.InvoiceLines)
                          .OrderByDescending(lp => lp.LineProfit)
                          .Take(10)
                          .Include("InvoiceID")
                          .Select(x => x.Invoice)
                          .Include("SalespersonID")
                          .Select(x => x.Person4.FullName).ToList();

            for(int i = 0; i < 10; i++)
            {
                top10.ElementAt(i).SalesPerson = salesPerson.ElementAt(i);
            }

            return top10;
        }

        /// <summary>
        /// Returns the latitude of the client's location
        /// </summary>
        /// <param name="ID">Customer ID of client</param>
        /// <returns>Latitude</returns>
        public double? Latitude(int ID)
        {
            //find the geographical location of the client
            var Latitude = db.Customers
                 .Where(o => o.CustomerID.Equals(ID))
                 .Select(x => x.City)
                 .Include("City")
                 .Select(x => x.Location.Latitude).First();

            return Latitude;
        }

        /// <summary>
        /// Returns the longitude of the client's location
        /// </summary>
        /// <param name="ID">Customer ID of client</param>
        /// <returns>Longitude</returns>
        public double? Longitude(int ID)
        {
            //find the geographical location of the client
            var Longitude = db.Customers
                      .Where(o => o.CustomerID.Equals(ID))
                      .Select(x => x.City)
                      .Include("City")
                      .Select(x => x.Location.Longitude).First();

            return Longitude;
        }
    }
}