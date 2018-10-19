## Homework 4
This assignment was designed in order for us to get comfortable with ASP.NET MVC 5, HTTP GET, and HTTP POST by creating two simple webpages requiring user input. We did this in two different ways -- via query strings and passing information via GET/POST parameters.  

### Links
[Home](https://siphry.github.io)  
[Assignment Details](http://www.wou.edu/~morses/classes/cs46x/assignments/HW4_1819.html)  
[Code Repository](https://github.com/siphry/siphry.github.io/tree/master/HW4)  

### Step 1 & 2 [Setup & Planning]
First I created a new project in Visual Studio named assignment4, and used [this](https://gist.github.com/indyfromoz/4109296) example as the basis for my .gitignore for my HW4 folder. Then, I started working on the `index.cshtml` and `layout` to get familiar with the project and the syntax before starting a feature branch called `converter` for the Miles to Metric section of the webpage. I thought I finished the index before switching, but I guess I did not so I ended up finishing the "Home" landing page and navbar in the `converter` feature branch. 

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
}
```

I started with re-creating the Miles to Metric page in html, which was simple enough to reproduce. Except it took me a while to realize that "number" inputs included the increment/decrement arrows and tried to find a bootstrap spinner or something. At first I gave each radio button a different name value which broke the selection (all would be checked at the same time), and forgot about adding decimal capabilies to the number input until I started testing the math in the `HomeController` view. Unfortunately, I apparently forgot what the purpose of the assignment was and did the conversion part in Javascript and wasted some time with that before reviewing the requirements and scrapping all that code. Thankfully, Wednesday's lecture covered all my questions I had surrounding how to make this work so I took my notes from that lecture and applied them to this assignment.

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

            //math goes here
            if(strMiles != null)
            {
                double result = 0;
                double miles = Convert.ToDouble(strMiles);

                switch(metric)
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
                        break;
                }

                //Debug.WriteLine(result);

                //message goes here
                string conversion = miles + " miles is equal to " + Convert.ToString(result) + " " + metric;

                //model/viewbag
                ViewBag.conversion = conversion;
            }

            return View();
        }
```
Since I had already written all of this in Javascript the night before, re-doing this was simple once I figured out how to use query strings and the Request object. I used the same logic we discussed in class in my if-statement to control when the math and ViewBag.conversion would occur. Originally I had written if-statements for my conversion logic, but did not like how that looked so Nick, Alex, and I decied a switch case would be better. After that, I tested to make sure everything seemed to work: I kept getting 404 errors if the user did not input a value into the miles input after they had already made a conversion and fixed this by adding the "required" to my input element generating a popup warning on the text box.  

#### Notes and Resources