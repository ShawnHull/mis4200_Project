using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mis4200_Project.Models
{
    public class Recognition
    {
        public int RecognitionID { get; set; }

        [Display(Name = "First Name")]
        public string employeeFirstName { get; set; }
        [Display(Name = "Last Name")]
        public string employeeLastName { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Phone")]
        public string phone { get; set; }
        [Display(Name = "Date of Recognition")]
        public DateTime recognitionDate { get; set; }
        [Display(Name = "Centric Unit")]
        public string Unit { get; set; }
        [Display(Name = "Full Name")]
        public string employeeFullName
        {
            get
            {
                return employeeLastName + ", " + employeeFirstName;
            }
        }

       }
    }