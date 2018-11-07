using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ViewModel class to hold the details of a client's orders and total profit -- accesses prior VM lists
/// </summary>
namespace assignment6.Models.ViewModels
{
    public class FullDetailsVM
    {
        public IEnumerable<PersonVM> Person { get; set; }
        public IEnumerable<CustomerVM> Customer { get; set; }
        public IEnumerable<InvoiceVM> Invoice { get; set; }
        public int OrderCount { get; set; }
        public decimal GrossSales { get; set; }
        public decimal GrossProfit { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}