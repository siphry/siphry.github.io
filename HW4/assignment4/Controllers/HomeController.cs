using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace assignment4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Controller Action Method
        [HttpGet]
        public ActionResult Converter()
        {
            //Get query strings
            string str_miles = Request.QueryString["miles"];
            string metric = Request.QueryString["units"];

            //math goes here
            if(str_miles != null)
            {
                double result = 0;
                double miles = Convert.ToDouble(str_miles);
                if (metric == "millimeters")
                {
                    result = miles * 1609344;
                }
                if (metric == "centimeters")
                {
                    result = miles * 160934.4;
                }
                if (metric == "meters")
                {
                    result = miles * 1609.344;
                }
                if (metric == "kilometers")
                {
                    result = miles * 1.609344;
                }

                //message goes here
                string message = miles + " miles is equal to " + Convert.ToString(result) + " " + metric;

                //model/viewbag
                ViewBag.message = message;
            }
            
            
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Message = "Combines colors from HEX";

            return View();
        }
    }
}