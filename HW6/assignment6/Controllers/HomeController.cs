using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using assignment6.Models;
using assignment6.Models.ViewModels;

namespace assignment6.Controllers
{
    public class HomeController : Controller
    {
        private WorldWideImportersContext db = new WorldWideImportersContext();

        [HttpGet]
        public ActionResult Index()
        {
            string searchName = Request.QueryString["searchname"];

            List<PersonVM> result = db.People.Where(p => p.FullName.Contains(searchName)).Where(p => p.PersonID != 1).Select(p => new PersonVM { FullName = p.FullName, PreferredName = p.PreferredName, PhoneNumber = p.PhoneNumber, FaxNumber = p.FaxNumber, EmailAddress = p.EmailAddress, ValidFrom = p.ValidFrom}).ToList();

            return View(result);
        }
       [HttpGet]
       public ActionResult Details(string personName)
        {
            List<PersonVM> person = db.People.Where(p => p.FullName == personName).Select(p => new PersonVM { FullName = p.FullName, PreferredName = p.PreferredName, PhoneNumber = p.PhoneNumber, FaxNumber = p.FaxNumber, EmailAddress = p.EmailAddress, ValidFrom = p.ValidFrom }).ToList();

            return View(person);
        }

    }
}