using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public ActionResult Create()
        {
            ViewBag.Message = "Combines colors from HEX";

            return View();
        }
    }
}