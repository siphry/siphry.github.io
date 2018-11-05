using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Class that contains specific details on the items purchased by clients of the database
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