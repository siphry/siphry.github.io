using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// ViewModel class to hold the details of a client from the database who is a customer
/// </summary>
namespace assignment6.Models.ViewModels
{
    public class CustomerVM
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string WebsiteURL { get; set; }
        public DateTime AccountStarted { get; set; }
    }
}