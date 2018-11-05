## Homework 6
This assignment was designed in order for us to get comfortable with ASP.NET MVC 5, HTTP GET, and HTTP POST by creating two simple webpages requiring user input. We did this in two different ways -- via query strings and passing information via GET/POST parameters.  

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
```

With that resulting string, I used Linq methods to create a list of PeopleVMs objects in which the FullName field of a Person from the People table in the database contains the searched value. I also populate my search result messages in this controller. Then, we return the list back to the into the view.

If you go back to the html code above, you can see that when we added the names to our list of search results, the links create a new variable "personName" that contains the full name of that person which we send to the Details action method.

Once our search results populated the list properly, we added some more validation techniques -- if someone edited the query string or if for some reason an empty search happened, we redirect back to the search page. 

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

