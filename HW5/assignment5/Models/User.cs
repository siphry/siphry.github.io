using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assignment5.Models
{
    public class User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DOB.Year;
                if(DOB > today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}: {FirstName} {LastName} DOB = {DOB}";
        }
    }
}