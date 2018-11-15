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
            //System.Diagnostics.Debug.WriteLine("API key:" + apiKey);
            //System.Diagnostics.Debug.WriteLine("search word: " + input);

            string website = "https://api.giphy.com/v1/stickers/translate?api_key=" + apiKey + "&s=" + input;
            System.Diagnostics.Debug.WriteLine("search url: " + website);

            var request = HttpWebRequest.Create(website);
            request.ContentType = "application/json; charset=utf-8";
            var response = (HttpWebResponse)request.GetResponse();

            string text, url, data;

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
                sr.Close();
            }
            //System.Diagnostics.Debug.WriteLine(text);
            data = JObject.Parse(text)["data"].ToString();
            url = JObject.Parse(data)["embed_url"].ToString();
            //System.Diagnostics.Debug.WriteLine(url);
            var sticker = new
            {
                embed_url = url
            };

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