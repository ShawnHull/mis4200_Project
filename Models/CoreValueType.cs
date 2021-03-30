using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mis4200_Project.Models
{
    public class CoreValueType
    {
        public int CoreValueTypeID { get; set; }

        [Display(Name = "Core Value Name")]
        public string CoreValueName { get; set; }

        public ICollection<Recognition> recognition { get; set; }
    }
}