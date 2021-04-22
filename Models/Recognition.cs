﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace mis4200_Project.Models
{
    public class Recognition
    {
        public int RecognitionID { get; set; }

        [Display(Name = "Core Value")]
        [Required(ErrorMessage = "Add Core Value")]
        public CoreValue value { get; set; }
        public enum CoreValue
        {
            Excellence = 1,
            Openness = 2,
            Stewardship = 3,
            Culture = 4,
            Passion = 5,
            Innovate = 6,
            Balanced = 7

        }

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

       /* [Display(Name = "Your Name")]
        [Required(ErrorMessage = "Add your name")]
        public Guid userID { get; set; }
        public virtual Profile User { get; set; } */



    }
    }