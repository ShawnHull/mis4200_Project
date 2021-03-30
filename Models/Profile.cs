using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mis4200_Project.Models
{
    public class Profile
    {
        public Guid profileID { get; set; }
        
        [Display(Name = "First Name")]
        public string employeeFirstName { get; set; }
        [Display(Name = "Last Name")]
        public string employeeLastName { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Phone")]
        public string phone { get; set; }
        [Display(Name = "Start Date")]
        public DateTime employeeSince { get; set; }
        [Display(Name = "Depaterment")]
        public string Department { get; set; }
        [Display(Name = "Social Media Links")]
        public string socialMediaLinks { get; set; }
        [Display(Name = "Full Name")]
        public string employeeFullName
        {
            get
            {
                return employeeLastName + ", " + employeeFirstName;
            }

        }
        public ICollection<Recognition> recognition { get; set; }
    }
}