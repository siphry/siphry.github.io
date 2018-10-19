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
    }
}