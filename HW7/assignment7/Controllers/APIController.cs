using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Newtonsoft.Json;
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
            System.Diagnostics.Debug.WriteLine("search url: " + website);

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

            return Json(sticker, JsonRequestBehavior.AllowGet);
        }
    }
}