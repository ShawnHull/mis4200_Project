using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mis4200_Project.Models
{
    public class Profile
    {
        public int profileID { get; set; }
        public string employeeFirstName { get; set; }
        public string employeeLastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public DateTime employeeSince { get; set; }
        public string Department { get; set; }
        public string socialMediaLinks { get; set; }
        public string employeeFullName
        {
            get
            {
                return employeeLastName + ", " + employeeFirstName;
            }

        }
    }
}