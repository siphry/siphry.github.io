using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ViewModel class to hold the details of a client from the database
/// </summary>

namespace assignment6.Models.ViewModels
{
    public class PersonVM
    {
        //Basic details
        public int PersonID { get; set; }
        public string FullName { get; set; }
        public string PreferredName { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime ValidFrom { get; set; }
        //list of clients who are also customers
        public IEnumerable<Customer> Customer { get; set; }

    }
}