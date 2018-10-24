using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace assignment5.Models
{
    public class Request
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Phone]
        public string PhoneNum { get; set; }
        [Required]
        public string AptName { get; set; }
        [Required]
        public int UnitNum { get; set; }
        [Required]
        public string Comments { get; set; }
        [Required]
        public bool Permission { get; set; }

        //add a ToString method for testing

    }
}