## Homework 5
This assignment was designed in order for us to get comfortable with using databases with MVC. We created a simple database for a mock apartment repaire request page with a single model, using SQL and C#.

### Links
[Home](https://siphry.github.io)  
[Assignment Details](http://www.wou.edu/~morses/classes/cs46x/assignments/HW5_1819.html)  
[Code Repository](https://github.com/siphry/siphry.github.io/tree/master/HW5)

### Step 1 [Setup]
I created a empty MVC project called 'assignment5' and got started changing the home/index page and the styling. Since it's Spooktober (aka October/Halloween time), I wanted to keep my "spooky" aesthetic going in my demos--thus I decided to use Silent Hill as a basic inspiration for the page by calling the apartment complex 'Silent Hills' and used an image from the series as the background for the demo website. The .css file was giving me issues and I could never figure out how to fix it, so all the styling is coded in the html of the layout page.

I tried a variety of ways to ignore my .mdf and .ldf files via my .gitignore, but nothing is working so far. If you check my .gitignore, you'll see that I have *.mdf and *.ldf as ignored files, and I even put in the specific directory/file to ignore as well, but they still upload to github.

### Step 2 [Planning & Design]
The first elements I worked on were the request forms page and the model. 

For the forms page, at first I used the pre-generated html as shown in class but I really didn't like how it looked and I was having problems making all their columns work the way I wanted them too, so I just got rid of all the styling and I actually liked how the Razor code displayed on the page without and styling. After looking at other student's form pages though, I decided I should include some sort of organization beyond just adding the Razor forms so I placed them into two columns. It also took me a while to figure out how to get the fonts and labels to display the way I wanted, so that was originally hardcoded here until I figured out how to set everything with either the layout or with the model ([DisplayName]).

```html
@model assignment5.Models.Request

@{
    ViewBag.Title = "Make a Request";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

@using (Html.BeginForm())
{
    @Html.AntiForgeryToken()

    <h1><font color="white">Silent Hills Apartments</font></h1>
    <h3><font color="white">Maintanence Request Form</font></h3>
    <hr />
    <div class="row">
        <div class="col-md-4">
            @Html.LabelFor(model => Model.FirstName)

            @Html.EditorFor(model => model.FirstName, new { htmlAttributes = new { @class = "form-control" } })

            @Html.LabelFor(model => Model.LastName, "Last name:")

            @Html.EditorFor(model => model.LastName, new { htmlAttributes = new { @class = "form-control" } })

            @Html.LabelFor(model => Model.PhoneNum, "Phone number:")

            @Html.EditorFor(model => model.PhoneNum, new { htmlAttributes = new { @class = "form-control" } })

            @Html.LabelFor(model => Model.AptName, "Apartment Name:")

            @Html.EditorFor(model => model.AptName, new { htmlAttributes = new { @class = "form-control" } })

            @Html.LabelFor(model => Model.UnitNum, "Unit number:")

            @Html.EditorFor(model => model.UnitNum, new { htmlAttributes = new { @class = "form-control" } })
        </div>
        <div class="col-md-8">

            @Html.LabelFor(model => Model.Comments, "Message:")

            @Html.TextAreaFor(model => model.Comments, new { @class = "form-control", rows = 5, cols = 40, placeholder = "Explanation of request, maintanence required, or complaint. Please be specific." })
            <br />
            @Html.ValidationSummary(false, "", new { @class = "text-danger" })

            <br />
            <font color="white"><b>Permission to enter apartment for maintanence.</b></font>
            @Html.CheckBoxFor(model => model.Permission)
            @Html.ValidationMessageFor(model => model.Permission, "", new { @class = "text-danger" })
            <br /><br />
            <input class="btn btn-primary" type="submit" value="Submit" />
            <br />
        </div>
    </div>
}
```

### Steps 3-7 [Content & Coding]
I made my model similar to what we did in class, including all the elements as shown on the assignment page. I set each one to required except for Key and SubmissionTime since they are not inputted by the user. It wasn't until after I had populated my database and auto-generated the controller in Visual Studio that I correctly added the SubmissionTime and Phone regular expression/error message. Adrian let me know how to correctly implement the SubmissionTime field as I was having issues figuring it out, while Nick and I both figured out the regular expression pattern and error message from StackOverflow.

```csharp
namespace assignment5.Models
{
    public class Request
    {
        //sets form/database values 
        //id key for database
        [Key]
        public int ID { get; set; }
        //first name input for form
        [Required][Display(Name = "First Name")]
        public string FirstName { get; set; }
        //last name input
        [Required][Display(Name = "Last Name")]
        public string LastName { get; set; }
        //phone number -- phone number structure required
        [Required]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Please enter phone number in 555-555-5555 format."), Display(Name = "Phone Number")]
        public string PhoneNum { get; set; }
        //apartment name input string
        [Required][Display(Name = "Apartment Name")]
        public string AptName { get; set; }
        //unit number input
        [Required][Display(Name = "Unit Name")]
        public int UnitNum { get; set; }
        //string for comments/messages on request
        [Required][Display(Name = "Comments")]
        public string Comments { get; set; }
        //permission checkbox/bool for entering apt
        [Display(Name = "Permission")]
        public bool Permission { get; set; }
        //gets current time from system for database
        private DateTime date = DateTime.Now;
        [Display(Name = "Submission Time")]
        public DateTime SubmissionTime {
           get { return date; }
           set { date = value; }
        }

        //add a ToString method for testing

    }
}
```
Then, I made my context in class while Scot demo'd how to do it. This was pretty simple and I just followed along making the database and context while he showed us in class.

```csharp
//ACCESS TO DATABASE
namespace assignment5.DAL
{
    public class RequestContext : DbContext
    {
        public RequestContext() : base("name=OurRequests")
        {

        }

        public virtual DbSet<Request> Request { get; set; } 
    }
}
```

Now that I had all of these elements, I created my table and seeded it with data for testing. I based this off of the example code given by Scot via bitbucket. Since my page is 'Silent Hill' themed most of the names and situations are based off of characters or scenarios from the game. I added the data with submission times out of order so I could test my ordering logic in my controller.

```sql
CREATE TABLE [dbo].[Requests]
(
    [ID]				INT IDENTITY (0,1)		NOT NULL,
    [FirstName]			NVARCHAR(64)			NOT NULL,
    [LastName]			NVARCHAR(128)		    NOT NULL,
    [PhoneNum]			NVARCHAR(15)            NOT NULL,
    [AptName]			NVARCHAR(40)			NOT NULL,
    [UnitNum]			INT						NOT NULL,
    [Comments]			NVARCHAR(1000)			NOT NULL,
    [Permission]		bit						NOT NULL,
    [SubmissionTime]	DateTime				NOT NULL
	CONSTRAINT [PK_dbo.Requests] PRIMARY KEY CLUSTERED ([ID] ASC)
);

INSERT INTO [dbo].[Requests] (FirstName, LastName, PhoneNum, AptName, UnitNum, Comments, Permission, SubmissionTime) VALUES
    ('Alessa','Gillespie','555-838-3298', 'Silent Hills Apartments', '1', 'There is a hole in the wall.', '1', '09/14/2018 14:24:00'),
    ('Cybil','Bennet','555-838-6701', 'Silent Hills Apartments', '2', 'The walls are rusted...out of nowhere...', '1', '09/14/2018 12:45:00'),
	('Heather','Mason','555-838-8923', 'Silent Hills Apartments', '6', 'I think our refridgerator is broken.', '1', ' 09/14/2018 17:30:00'),
	('Alessa','Gillespie','555-838-1076', 'Silent Hills Apartments', '1', 'The hole is gone now.', '1', '09/14/2018 16:14:00'),
	('Matthew','Mercer','555-838-0000', 'Silent Hills Apartments', '5', 'My next door neighbor is loud at night.', '1', '09/14/2018 13:14:00')
GO
```

I then added my connection string in my Web.config settings and auto-generated a new controller based on my database/context to use as an example for my main page. Thankfully I had somehow already added a different connection string in my messing around with this project so I just got to copy/paste the set up.

```html
<connectionStrings>
    <add name="OurRequests" connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\staci\Desktop\CS460\siphry.github.io\HW5\assignment5\App_Data\OurRequests.mdf;Integrated Security=True" providerName="System.Data.SqlClient" />
</connectionStrings>
  ```

  With the auto-generated controller, I used the code I needed from that and copy/pasted them into my Home controller and Views as needed. Nick showed me how to order the list via the controller, I was originally thinking of making some sort of query and doing it that way but had no idea if it would have worked so this was a nice and simple way to order the table by submission time.

  ```csharp
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
```

### Step 8 [Testing]
My seeded database table was extremely simple since I just used the one that Visual Studio auto-generates. It has a nice, clean look. But when I transfered the code to my home controller I forgot to change the name of the POST method to match my views, so that gave me a bit of a freak out why it suddenly wasn't working anymore! But I found the error quickly. 

### Demo Screenshots
![index](https://siphry.github.io/HW5/images/index.PNG)  

![request_list](https://siphry.github.io/HW5/images/request_list.PNG)  

![maintanence](https://siphry.github.io/HW5/images/maintanence.PNG)  

![request_list2](https://siphry.github.io/HW5/images/request_list2.PNG)  


Since I misunderstood the first few lectures and for some reason focused more on the lecture and less on the instructions, I got very lost for this assignment but in the end...it was much more simpler than I had originally felt. The most common issue I saw others run into were naming convention problems with their model and database and connecting them together. I tried to work on everyone on my own by following the instructions on Mon/Tues, but got stuck in my head since I heard multiple times that this was the hardest assignment so I got lost trying to find walkthroughs or help online. It wasn't until Thursday that everything fell together and I was able to wrap up the webpage on Friday.