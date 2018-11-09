using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace assignment7.Controllers
{
    public class APIController : Controller
    {
        public JsonResult Sticker(string input)
        {
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["id"];
            //System.Diagnostics.Debug.WriteLine("API key:" + apiKey);
            //System.Diagnostics.Debug.WriteLine("search word: " + input);


            string website = "https://api.giphy.com/v1/stickers/translate?api_key=" + apiKey + "&s=" + input;

            var request = WebRequest.Create(website);
            request.ContentType = "application/json; charset=utf-8";
            var response = (HttpWebResponse)request.GetResponse();
            string text, url, data;

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
                sr.Close();
            }
            data = JObject.Parse(text)["data"].ToString();
            url = JObject.Parse(data)["embed_url"].ToString();
            //System.Diagnostics.Debug.WriteLine(url);
            var sticker = new
            {
                embed_url = url
            };

            return Json(sticker, JsonRequestBehavior.AllowGet);


        }

    }
}