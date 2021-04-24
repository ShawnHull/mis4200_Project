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
        [Required(ErrorMessage = "Add First Name")]
        public string employeeFirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Add Last Name")]
        public string employeeLastName { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Add Email")]
        public string email { get; set; }
        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Add Phone")]
        public string phone { get; set; }
        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Add Start Date")]
        [DisplayFormat(DataFormatString = "{0:d}")] 
        public DateTime employeeSince { get; set; }

        [Display(Name = "Depaterment")]
        [Required(ErrorMessage = "Add Depaterment")]
        public string Department { get; set; }

        [Display(Name = "Position")]
        [Required(ErrorMessage = "Add Postion")]
        public string postion { get; set; }

        [Display(Name = "Social Media Links")]
        public string socialMediaLinks { get; set; }

        [Display(Name = "Avatar")]
        public string avatar { get; set; }

        [Display(Name = "Full Name")]
        public string employeeFullName
        {
            get
            {
                return employeeLastName + ", " + employeeFirstName;
            }

        }
        public ICollection<Recognition> Recognition { get; set; }
    }
}