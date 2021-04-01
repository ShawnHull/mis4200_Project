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

        [Display(Name = "Core Value")]
        [Required(ErrorMessage = "Add Core Value")]
        public int CoreValueTypeID { get; set; }
        public virtual CoreValueType CoreValueType { get; set; }

        [Display(Name = "Recognition Description")]
        [Required(ErrorMessage = "Please explain why you are making this recognition")]
        public string recognitionDescription { get; set; }

        [Display(Name = "Recognition Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "Please input a date")]
        public DateTime recognitionDate { get; set; }

        [Display(Name = "Employee Name")]
        [Required(ErrorMessage = "Add Employee")]
        public Guid profileID { get; set; }
        public virtual Profile Profile { get; set; }


    }
    }