## Homework 6
The purpose of this assignment was for us to become comfortable with working with existing databases with MVC ASP.NET by restoring and displaying the pre-existing database via SMSS and MVC. 

### Links
[Home](https://siphry.github.io)  
[Assignment Details](http://www.wou.edu/~morses/classes/cs46x/assignments/HW6_1819.html)  
[Code Repository](https://github.com/siphry/siphry.github.io/tree/master/HW6)

### Step 1 & 2 [Setup]
I started by downloading the database from the provided link, and downloading Entity Framework and SqlServer.Types via NuGet for this assignment, as well as adding the code provided on the homework page in the Application_Start() method in Global.asax.

I did not restore the database in class while Scot demonstrated so I had to get help from my classmates to get that part up on running -- I kept trying to add the database to Visual without restoring it first with SMSS and following the links provided only confused me more until I got help from Nick.

Once the database was restored, adding it to Visual Studio was a simple process, and I ended up with 31 tables as well. Then I moved the `DBContext` to the DAL folder and moved on.

### Step 3 [Content/Coding]
The first thing I wrote for this assignment was the search landing page. 

```html
@model IEnumerable<assignment6.Models.ViewModels.PersonVM>

@{
    ViewBag.Title = "World Wide Importers";
}

@using (Html.BeginForm("Index", "Home", FormMethod.Get))
{
    <div class="row">
        <div class="col-sm-2"></div>
        <div class="col-sm-8">
            <div class="form-group" align="center">
                <h3>Client Search</h3>
                <div class="searchbar">
                    <input class="form-control" type="text" name="searchname" placeholder="Search by client name" required />
                    <button type="submit" class="btn btn-primary button btn">Search</button>
                </div>
            </div>
        </div>
        <div class="col-sm-2"></div>
    </div>
}

<!--if there are zero results, post message-->
@if (ViewBag.notFound != null)
{
    <p>@ViewBag.notFound</p>
}
//toggle to display result list for non-null search
else if (ViewBag.notNull == 1)
{
    //posts message stating what was searched
    if (ViewBag.search != null)
    {
        <p>@ViewBag.search</p>
    }

    <ul style="list-style-type: none;">
        <!--displays all results for search-->
        @foreach (var person in Model)
        {
            <li> @Html.ActionLink(person.FullName, "Details", "Home", new { personName = person.FullName }, null)</li>
        }
    </ul>
}
```

A single text input and button for my search bar and I changed the navbar to just be a basic logo and color. On the bottom I have my message handling for if there are no results for the search or to post what was searched along with the results. When we (classmates and I working together) were doing out testing for the search bars, we spent a while trying to figure out how to make it so a empty search would not display every person in the database...we spent a long time before realizing that, at least for client side, we just needed to add "required" to our input element.

For the database aspects, I started with creating my new ViewModel for the database for feature #1 of the assignment (PersonVM). My original ViewModel did not include the last field since I had not started working on feature #2 yet.

```csharp
/// <summary>
/// ViewModel class to hold the details of a client from the database
/// </summary>

namespace assignment6.Models.ViewModels
{
    public class PersonVM
    {
        //Basic details
        public int PersonID { get; set; }
        public string FullName { get; set; }
        public string PreferredName { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime ValidFrom { get; set; }
        //List of clients who are also customers
        public IEnumerable<Customer> Customer { get; set; }
    }
}
```

Then we started designing our queries around this. The basic search to list of names was pretty simple to write, since we just need to populate a list of names that fulfill the inputed search value. I did the same process as I did for the miles converter assignment by passing the inputted value to a query string and requested it in the controller to a separate string.

```csharp
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
        ...
```

With that resulting string, I used Linq methods to create a list of PeopleVMs objects in which the FullName field of a Person from the People table in the database contains the searched value. I also populate my search result messages in this controller. Then, we return the list back to the into the view.

Once our search results populated the list properly, we added some more validation techniques -- if someone edited the query string or if for some reason an empty search happened, we redirect back to the search page. 

If you go back to the html code above, you can see that when we added the names to our list of search results, the links create a new variable "personName" that contains the full name of that person which we send to the Details action method. Then, in the details action method, we use that variable to create a single item list of PersonVM that holds all the details of the Person object of that name.

```csharp
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
    ...
```

I use this list to posts the information to my details page by returning the list to the view. 

```html
@model assignment6.Models.ViewModels.FullDetailsVM

@{
    ViewBag.Title = "Details on Client";
}
<h2 align="center">@ViewBag.Title</h2>
    <div class="container">
        <div class="row">
            <div class="col-lg-9">
                <hr />
                <div class="detail_content">
                    <h3><b>@Model.Person.FirstOrDefault().FullName</b></h3>
                    <hr />
                    <p><b>Full name:</b> @Model.Person.FirstOrDefault().FullName</p>
                    <p><b>Preferred name: </b>@Model.Person.FirstOrDefault().PreferredName</p>
                    <p><b>Phone number:</b> @Model.Person.FirstOrDefault().PhoneNumber</p>
                    <p><b>Fax number:</b> @Model.Person.FirstOrDefault().FaxNumber</p>
                    <p>
                        <b>Email address:</b> <a href="mailto: @Model.Person.FirstOrDefault().EmailAddress">
                            @Model.Person.FirstOrDefault().EmailAddress
                        </a>
                    </p>
                    <p><b>Member since:</b> @Model.Person.FirstOrDefault().ValidFrom</p>
                </div>
```

Displaying the information here was the easiest part of the assignment since we had basically already did this in the last assignment (though I had forgotten since I used the scoffold generated code, so I had a mini freak out until Nick pointed out it was the same thing as before). I like to keep things simple so I just made a two column display, the left side displays all the details and the right holds a placehold image for the "photo" of the client. I placed each chunk of details into their own containers so they displayed separately. 

### Step 4 [Content & Coding]

While the first feature was fairly simple to implement, I had an *extremely* difficult time with feature #2. I knew that once the queries and any other extra ViewModels were written, displaying the data is easy, and adding some more testing/error handling is also not very difficult. But writing these queries was practically impossible for me -- I kept looking at the database from an SQL perspective, I understood how to tackle it with that syntax but could not get a grasp on how to navigate the tables and linked tables with LINQ for the life of me. I googled around and got hints from Nick and reviewed the class notes/code, but I just couldn't figure anything out. Dom eventually helped me out Sunday night with some examples. This was where I was eventually heading into -- creating separate ViewModels per section of information and adding everything to new variables. I just couldn't think of a way to link everything to the PersonID and PrimaryContactID until Dom helped me out...with a simple solution of just passing the ID into separate methods directly. 

All of these new methods are in my HomeController. 

```csharp
...
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
```

Each method sends their information to a different ViewModel (CustomerVM and InvoiceVM which I will post at the bottom of the page along with FullDetailsVM), except for the methods dealing strictly with integer/decimal values. I use these methods to populate my ViewModel obj lists, linking everything via the CustomerID (except for generating the CustomerVM list, where I match the CustomerID to the PersonID from the PersonVM list). PersonVM contains a list of CustomerVMs for all clients that are customers, and then FullDetailsVM contains lists of PersonVM, CustomerVM, and InvoiceVM as well as the fields for the basic number values. I use FullDetailsVM to display the information for both customer and employee clients of the database.

I couldn't figure out how to get the right sales person name without talking to Nick, who explained how to use .Include in the navigation property, so my InvoiceVM method generates two lists and then takes the information from the salesPerson list and adds it to the InvoiceVM list. 

```csharp
...
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
            }

            return View(resultDetails);
        }
```

Then, I just displayed the information in the same pattern as before -- each section gets it's own div container using the same .css styling, and the top 10 items list is displayed in a table. The code is fairly similar to the example from Details.chtml above.

### Final Thoughts & Video Demo
This was a hard assignment. I really struggled with it all weekend until getting help from my classmates. I don't think I would have ever figured it out without them...this was the first time I struggled this hard with anything related to computer science. Usually after some researching I can at least figure out SOMETHING on my own, but this time I just couldn't get a grasp on anything. 

[![Video Demo](https://siphry.github.io/HW6/images/youtube.PNG)](https://www.youtube.com/watch?v=jicumTlESiE)

I also added the extra credit after recording my video, see screenshots below.

![extra_credit](https://siphry.github.io/HW6/images/extra_credits.PNG)  

CustomerVM -- 

```csharp
/// <summary>
/// ViewModel class to hold the details of a client from the database who is a customer
/// </summary>
namespace assignment6.Models.ViewModels
{
    public class CustomerVM
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string WebsiteURL { get; set; }
        public DateTime AccountStarted { get; set; }
    }
}
```

InvoiceVM --

```csharp
/// <summary>
/// ViewModel class to hold the details of a client's order history, profit, and sales person
/// </summary>
/// 
namespace assignment6.Models.ViewModels
{
    public class InvoiceVM
    {
        [Display(Name = "Stock ID")]
        public int StockID { get; set; }
        public string Description { get; set; }
        [Display(Name = "Total Profit")]
        public decimal TotalProfit { get; set; }
        [Display(Name = "Sales Person")]
        public string SalesPerson { get; set; }

    }
}
```

FullDetailsVM --

```csharp
/// <summary>
/// ViewModel class to hold the details of a client's orders and total profit -- accesses prior VM lists
/// </summary>
namespace assignment6.Models.ViewModels
{
    public class FullDetailsVM
    {
        public IEnumerable<PersonVM> Person { get; set; }
        public IEnumerable<CustomerVM> Customer { get; set; }
        public IEnumerable<InvoiceVM> Invoice { get; set; }
        public int OrderCount { get; set; }
        public decimal GrossSales { get; set; }
        public decimal GrossProfit { get; set; }
    }
}
```
