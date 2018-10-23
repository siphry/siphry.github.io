## Homework 4
This assignment was designed in order for us to get comfortable with ASP.NET MVC 5, HTTP GET, and HTTP POST by creating two simple webpages requiring user input. We did this in two different ways -- via query strings and passing information via GET/POST parameters.  

### Links
[Home](https://siphry.github.io)  
[Assignment Details](http://www.wou.edu/~morses/classes/cs46x/assignments/HW4_1819.html)  
[Code Repository](https://github.com/siphry/siphry.github.io/tree/master/HW4)  

### Step 1 & 2 [Setup & Planning]
First I created a new project in Visual Studio named assignment4, and used [this](https://gist.github.com/indyfromoz/4109296) example as the basis for my .gitignore for my HW4 folder. Then, I started working on the index.cshtml and layout to get familiar with the project and the syntax before starting a feature branch called converter for the Miles to Metric section of the webpage. I thought I finished the index before switching, but I guess I did not so I ended up finishing the "Home" landing page and navbar in the converter feature branch. 

![home](https://siphry.github.io/HW4/images/home.PNG)

### Step 3 [Content/Coding]
```html
@{
    ViewBag.Title = "Miles to Metric Converter";
}

<h2>Convert Miles to Metric</h2>
<div>
    <div class="container">
        <form method="get" form action="/Home/Converter">
            <div class="row">
                <div class="col-sm-6">
                    <h4><b>Miles</b></h4>
                    <p>
                        <!--Basic number input with decimal step-->
                        <input type="number" name="miles" min="1" step=".01" required>
                    </p>
                </div>
                <div class="col-sm-6">
                    <h3>Select a unit</h3>
                    <hr>
                    <!--Basic radio buttons for metric selection-->
                    <input type="radio" name="units" value="millimeters" checked> Millimeters<br>
                    <input type="radio" name="units" value="centimeters"> Centimeters<br>
                    <input type="radio" name="units" value="meters"> Meters<br>
                    <input type="radio" name="units" value="kilometers"> Kilometers<br>
                </div>
            </div>
            <br><br>
            <div align="center"><p><input class="btn btn-primary" type="submit" value="Convert" /></p></div>
            <br><br>
            <p id="result"></p>
        </form>
    </div>
</div>
<!--ViewBag section for page update with results-->
@if (ViewBag.conversion != null)
{
    <h4><b><font color="#8b0000">@ViewBag.conversion</font></b></h4>
    <h4><b><font color="#8b0000">@ViewBag.message</font></b></h4>
}
```

I started with re-creating the Miles to Metric page in html, which was simple enough to reproduce. Except it took me a while to realize that "number" inputs included the increment/decrement arrows and tried to find a bootstrap spinner or something. At first I gave each radio button a different name value which broke the selection (all would be checked at the same time), and forgot about adding decimal capabilies to the number input until I started testing the math in the HomeController view. Unfortunately, I apparently forgot what the purpose of the assignment was and did the conversion part in Javascript and wasted some time with that before reviewing the requirements and scrapping all that code. Thankfully, Wednesday's lecture covered all my questions I had surrounding how to make this work so I took my notes from that lecture and applied them to this assignment.

```csharp
        /// <summary>
        /// Converts user input miles to user selected metric value
        /// </summary>
        /// <returns>The metric value of whatever miles</returns>
        //Controller Action Method
        [HttpGet]
        public ActionResult Converter()
        {
            //Get query strings
            string strMiles = Request.QueryString["miles"];
            string metric = Request.QueryString["units"];

            //only does this/posts messages to webpage if user has inputted a value into miles
            if(strMiles != null)
            {
                //check to make sure user hasn't inputed a NaN in the query string URL
                char firstChar = strMiles[0];
                bool isNum = Char.IsDigit(firstChar);
       
                if (!isNum)
                {
                    ViewBag.message = "Please do not input non-numbers in the query string.";
                }
                else
                {
                    //math goes here
                    double result = 0;
                    double miles = Convert.ToDouble(strMiles);
                    string warning = "Please do not change the metric value in the query string.";

                    switch (metric)
                    {
                        case "millimeters":
                            result = miles * 1609344;
                            break;
                        case "centimeters":
                            result = miles * 160934.4;
                            break;
                        case "meters":
                            result = miles * 1609.344;
                            break;
                        case "kilometers":
                            result = miles * 1.609344;
                            break;
                        default:
                            ViewBag.message = warning;
                            break;
                    }

                    //if user hasn't inputted something else into the units query string
                    if(ViewBag.message != warning)
                    { 
                        //message goes here
                        string conversion = miles + " miles is equal to " + Convert.ToString(result) + " " + metric;

                        //viewbag statement
                        ViewBag.conversion = conversion;
                    }
                }
            }
            return View();
        }
```
Since I had already written all of this in Javascript the night before, re-doing this was simple once I figured out how to use query strings and the Request object. I used the same logic we discussed in class in my if-statement to control when the math and ViewBag.conversion would occur. Originally I had written if-statements for my conversion logic, but did not like how that looked so Nick, Alex, and I decied a switch case would be better. After that, I tested to make sure everything seemed to work: I kept getting 404 errors if the user did not input a value into the miles input after they had already made a conversion and fixed this by adding the "required" to my input element generating a popup warning on the text box.  

![converter](https://siphry.github.io/HW4/images/converter.PNG)

![converter_result](https://siphry.github.io/HW4/images/converter_result.PNG)

Nothing more satisfying than working through something with your classmates and getting it to work exactly how it's supposed to!

After reviewing the grading rubric, I realized I needed to add something into the converter in case the user wrote something manually into the url query string. I added some simple checking that will prints a pleasant message to the converter page if the user changes the value of 'miles' or 'units' in the url query string.

![miles_warning](https://siphry.github.io/HW4/images/miles_warning.PNG)  

![units_warning](https://siphry.github.io/HW4/images/units_warning.PNG)  


### Step 4 [Content/Coding]

```html
@{
    ViewBag.Title = "Create";
}
<!--razor Helper html method-->
@using (Html.BeginForm())
{
    <h2>Create a New Color</h2>
    <p>Please enter colors in HTML hexadecimal format: #AABBCC</p>
    <br />
    <!--Razor helper input for HTML-->
    <p><h5><b>First Color</b></h5></p>
    @Html.TextBox("firstColor", null, htmlAttributes: new { @class = "form-control", placeholder = "#ffffff", pattern = "#[0-9-Fa-f]{6}", required = "required" })
    <p><h5><b>Second Color</b></h5></p>
    @Html.TextBox("secondColor", null, htmlAttributes: new { @class = "form-control", placeholder = "#ffffff", pattern = "#[0-9-Fa-f]{6}", required = "required" })
    <br />
    <button class="btn btn-primary" type="submit">Mix Colors</button>
    <br /><br />
    <!--Adds the color squares to the page-->
    <div class="container">
        <div id="row">
            <div class="col-sm-2" style="@ViewBag.firstC"></div>
            <div class="col-sm-2" style="text-align: center; line-height: 80px; font-weight:300; font-size:28px; width: 80px; height: 80px;">@ViewBag.plus </div>
            <div class="col-sm-2" style="@ViewBag.secondC"></div>
            <div class="col-sm-2" style="text-align: center; line-height: 80px; font-weight:300; font-size:28px; width: 80px; height: 80px;">@ViewBag.equal </div>
            <div class="col-sm-2" style="@ViewBag.newC"></div>
        </div>
    </div>
}
```

Re-creating this page in HTML was simple enough, but figuring out exactly how to use the Razor HTML helpers took me a moment to figure out, so I went to Alex for help with the pattern and required parts especially. I also got help from Stuart for how to use the ViewBag with the HTML -- originally I was thinking about making the color squals with HTML and CSS, and figuring out how the ViewBag might alter the CSS background-color etc...I didn't realize you could do that right in the HTML.

```csharp
        /// <summary>
        /// GET method for user input of HEX color values
        /// </summary>
        /// <returns>The view for color page results</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST method to update the color create page to show color results
        /// </summary>
        /// <param name="firstColor">The HEX value for the first user input</param>
        /// <param name="secondColor">The HEX value for the second user input</param>
        /// <returns>The view for the color create page update</returns>
        [HttpPost]
        public ActionResult Create(string firstColor, string secondColor)
        {
            if(firstColor != null && secondColor != null)
            {
                //translates inputed HEX values to color object
                Color colorFirst = ColorTranslator.FromHtml(firstColor);
                Color colorSecond = ColorTranslator.FromHtml(secondColor);

                //add the argb values together to form new color
                int newA = colorFirst.A + colorSecond.A;
                if(newA > 1) { newA = 1; }
                int newR = colorFirst.R + colorSecond.R;
                if(newR > 255) { newR = 255; }
                int newG = colorFirst.G + colorSecond.G;
                if(newG > 255) { newG = 255; }
                int newB = colorFirst.B + colorSecond.B;
                if(newB > 255) { newB = 255; }

                //make new color obj from the above values
                Color newColor = Color.FromArgb(newA, newR, newG, newB);

                //convert color obj back to HTML hex values
                string firstHex = ColorTranslator.ToHtml(Color.FromArgb(colorFirst.A, colorFirst.R, colorFirst.G, colorFirst.B));
                string secondHex = ColorTranslator.ToHtml(Color.FromArgb(colorSecond.A, colorSecond.R, colorSecond.G, colorSecond.B));
                string newHex = ColorTranslator.ToHtml(Color.FromArgb(newA, newR, newG, newB));

                //add values to ViewBag to change the page and show the colors
                ViewBag.firstC = "width: 80px; height: 80px; border: 1px solid #000000; background: " + firstHex + "; ";
                ViewBag.secondC = "width: 80px; height: 80px; border: 1px solid #000000; background: " + secondHex + "; ";
                ViewBag.newC = "width: 80px; height: 80px; border: 1px solid #000000; background: " + newHex + "; ";
                ViewBag.plus = "+";
                ViewBag.equal = "=";
            }

            return View();
        }
```

For this section, I got help from Alex to get started but once I got rolling everything fell into place. The lecture on Thursday helped quite a bit as well with using the Color object and ColorTranslation, moving the hex input back and forth from hex to argb, etc. According [this](https://msdn.microsoft.com/en-us/library/mt712639.aspx) documentation on the Color class, there should have been a method to go straight back to hex via the color object, but that method was missing from Visual Studio, so we went with this somewhat longer workout to get the new color back to html. Now that we have the new color, we can add them all the the ViewBag with html strings, including the plus and equal signs (or else they'd always be on the create page).

![create](https://siphry.github.io/HW4/images/create.PNG)

![create_result](https://siphry.github.io/HW4/images/create_result.PNG)

It works! That was so satisfying to see the colors show up on the screen, and it's actually a bit fun to mix the colors here. 


#### Notes and Resources
[W3Schools](https://www.w3schools.com/asp/coll_querystring.asp)  
[Microsoft ASP.NET MVC 5](https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/getting-started)  