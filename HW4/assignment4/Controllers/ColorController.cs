using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;

namespace assignment4.Controllers
{
    public class ColorController : Controller
    {
        // GET: Color
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string firstColor, string secondColor)
        {
            //debug to check that input values works
            System.Diagnostics.Debug.WriteLine("First Color: " + firstColor);
            System.Diagnostics.Debug.WriteLine("Second Color: " + secondColor);

            if(firstColor != null && secondColor != null)
            {
                //translates inputed HEX values to color object
                Color colorFirst = ColorTranslator.FromHtml(firstColor);
                Color colorSecond = ColorTranslator.FromHtml(secondColor);

                //debug to check that translator works
                System.Diagnostics.Debug.WriteLine("First Color: " + Convert.ToString(colorFirst));
                System.Diagnostics.Debug.WriteLine("Second Color: " + Convert.ToString(colorSecond));

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

                //debug check to see if these values are correct rgb vals
                System.Diagnostics.Debug.WriteLine("New color: " + Convert.ToString(newColor));

                //convert color obj back to HTML hex values
                string firstHex = ColorTranslator.ToHtml(Color.FromArgb(colorFirst.A, colorFirst.R, colorFirst.G, colorFirst.B));
                string secondHex = ColorTranslator.ToHtml(Color.FromArgb(colorSecond.A, colorSecond.R, colorSecond.G, colorSecond.B));
                string newHex = ColorTranslator.ToHtml(Color.FromArgb(newA, newR, newG, newB));

                //debug check to see if these values correctly converted
                System.Diagnostics.Debug.WriteLine("First hex: " + firstHex);
                System.Diagnostics.Debug.WriteLine("Second hex: " + secondHex);
                System.Diagnostics.Debug.WriteLine("New hex: " + newHex);

                ViewBag.firstC = "width: 80px; height: 80px; border: 1px solid #000000; background: " + firstHex + "; ";
                ViewBag.secondC = "width: 80px; height: 80px; border: 1px solid #000000; background: " + secondHex + "; ";
                ViewBag.newC = "width: 80px; height: 80px; border: 1px solid #000000; background: " + newHex + "; ";
            }

            return View();
        }
    }

}