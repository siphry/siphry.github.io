## Homework 7
The goal of homework 7 was to become comfortable with dynamic web design, custom routing, AJAX, JSON, hidden keys, and sending/receiving requests from other servers.

### Links
[Home](https://siphry.github.io)  
[Assignment Details](http://www.wou.edu/~morses/classes/cs46x/assignments/HW7_1819.html)  
[Code Repository](https://github.com/siphry/siphry.github.io/tree/master/HW7)

### Step 1 [Setup]
Again, standard procedures for set up except this time we did not use any templates for the project or the .cs files -- I created empty projects with only the MVC folders set up upon creation and the only scaffolding I did was for generating the database model after creating the table with SQL.

### Step 2 [Content & Coding]
For this, I basically just used the same html from the previous homework assignment and set it up for posting the message undernearth the search bar, but that was about it. Also added the `scriptSection` first thing before writing too much of the javascript.

```html
@{
    ViewBag.Title = "CS 460 Homework 7";
}

<div class="row">
    <div class="col-sm-2"></div>
    <div class="col-sm-8">
        <div align="center">
            <h2>Internet Language Translator</h2>
            <br />
            <div class="searchbar">
                <input type="text" class="form-control" id="search" placeholder="Start typing message here..." />
            </div>
            <br />
            <div class="message">

            </div>
        </div>
    </div>
    <div class="col-sm-2"></div>
</div>

@section scriptSection{
    <script src="~/Scripts/jquery-3.3.1.min.js"></script>
    <script src="~/Scripts/search.js"></script>
}
```

### Step 3 [Setup]
Signing up for Giphy was easy. Got my API key and immediately made my AppSettingsSecrets.config file and placed it into my Web.config following the instructions from the linked page on the assignment. 

```html
<appSettings file="..\..\..\AppSettingsSecrets.config">
...
</appSettings>
```

### Step 4 [Content & Coding]
The next steps I tackled was all the javascript coding for the input bar and sending the last word either to the controller via AJAX or posting it straight to the page via JQuery. 

```javascript
//listens for keydowns, if space bar is pressed take the value, split it into an array, and check the last input
$("#search").keydown(function (event) {
    var last = "";
    if (event.keyCode == 32 || event.keyCode == 0) {
        var userString = $("#search").val().toString();
        var inputArray = userString.split(" ");
        var num = inputArray.length;
        last = inputArray[num - 1];

        if (adjectives.includes(last.toLowerCase()) || nouns.includes(last.toLowerCase()) || verbs.includes(last.toLowerCase())) {
            //send interesting word to controller via jquery.ajax
            var source = "/API/Sticker/" + last
            $.ajax({
                method: "GET",
                dataType: "json",
                url: source,
                success: successSticker,
                error: errorAjax
            })
        } else {
            //add regular word \to index via Jquery
            $(".message").append(last + " ");
        }
    }
});
```

Tiffany helped me with the AJAX code based on the examples from the lectures. We could not get it to send to the controller until I asked Scot for advice on the slack page, to which we figured out that we had forgotten to do the custom routing and designate the path for this AJAX code to send.

After we got the word to actually send to the controller, I tackled sending it off to Giphy via our url/API key. We constructed a string url to actually send via a HttpWebRequest. I found an example on stack overflow for both the web request and for parsing the Json object that is returned.

```csharp
using System;
using System.Web.Mvc;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;
using assignment7.Models;
using assignment7.DAL;

namespace assignment7.Controllers
{
    public class APIController : Controller
    {
        private RecordsContext db = new RecordsContext();

        public JsonResult Sticker(string input)
        {
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["id"];

            string website = "https://api.giphy.com/v1/stickers/translate?api_key=" + apiKey + "&s=" + input;

            var request = HttpWebRequest.Create(website);
            request.ContentType = "application/json; charset=utf-8";
            var response = (HttpWebResponse)request.GetResponse();

            string text, url, data;
            //reads the JSON object and places it into a string
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
                sr.Close();
            }
            //parses the string into the data key values
            data = JObject.Parse(text)["data"].ToString();
            //parses the string to the embed_url value 
            url = JObject.Parse(data)["embed_url"].ToString();
            
            //creats a json type object to send back to the view/javascript
            var sticker = new
            {
                embed_url = url
            };

            //database information
            ...

            return Json(sticker, JsonRequestBehavior.AllowGet);
        }
    }
}
```

I installed the Newtonsoft JSON linq plugin for Visual Studio so I could easily parse through the return JSON object/string without doing some ridculous string splitting or searching with a regex. I used the embed_url since it is a direct link to a postable gif.

Once I knew I got back the url from parsing (while writing the code I had included multiple console.log and Debug.WriteLine in order to test that my code was working properly), I added the append JQuery logic to my javascript file so the words and gifs would post properly, then changed up the styling to match the example a little bit more.

```javascript
function successSticker(sticker) {
    //add stick url to index
    $(".message").append("<iframe src='" + sticker.embed_url + "' height='150' width='150' frameBorder='0' align='middle'>");
}

//if the pass didn't work
function errorAjax() {
    console.log("error");
}
```

Finally, I tackled the database aspects. I had the most trouble with getting this to work, mostly due to me not looking carefully at the error I was getting/not clicking around in the inspect window to get more details. I made the database first, created a table in the database, auto-generated the model based off that, then wrote the code in the controller to add the needed information to the database. Unforunately, I had my connection string labeled incorrectly and it took me far too long to realize what I had missed. Once I labeled it correctly, everything worked as desired.

```csharp
//database information
            var ip = Request.UserHostAddress;
            var agent = Request.Browser.Type;
            var newRecord = new Record
            {
                Date = DateTime.Now,
                Input = input,
                GiphyURL = sticker.embed_url,
                IP = ip,
                Browser_Agent = agent
        };
            db.Record.Add(newRecord);
            db.SaveChanges();
```

### Video Demo


