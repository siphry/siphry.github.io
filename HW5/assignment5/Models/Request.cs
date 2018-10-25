using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace assignment5.Models
{
    public class Request
    {
        //sets form/database values 
        //id key for database
        [Key][Required]
        public int ID { get; set; }
        //first name input for form
        [Required][Display(Name = "First Name")]
        public string FirstName { get; set; }
        //last name input
        [Required][Display(Name = "Last Name")]
        public string LastName { get; set; }
        //phone number -- phone number structure required
        [Required]
        [Phone][Display(Name = "Phone Number")]
        public string PhoneNum { get; set; }
        //apartment name input string
        [Required][Display(Name = "Apartment Name")]
        public string AptName { get; set; }
        //unit number input
        [Required][Display(Name = "Unit Name")]
        public int UnitNum { get; set; }
        //string for comments/messages on request
        [Required][Display(Name = "Comments")]
        public string Comments { get; set; }
        //permission checkbox/bool for entering apt
        [Display(Name = "Permission")]
        public bool Permission { get; set; }
        //gets current time from system for database
        private DateTime date = DateTime.Now;
        [Required][Display(Name = "Submission Time")]
        public DateTime SubmissionTime {
           get { return date; }
           set { date = value; }
        }

        //add a ToString method for testing

    }
}