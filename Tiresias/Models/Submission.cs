using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiresias.Models
{
    [Table("submissions")]
    public class Submission
    {

        [Key]  
        [HiddenInput]
        public int submission_id { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        [Display(Name = "Date Added")]
        public DateTime submission_date { get; set; }

        [Display(Name = "Email")]
        public string submission_email { get; set; }

    
        public string author_name { get; set; }
        

        public int work_id { get; set; }
        public string work_title { get; set; }

        [Display(Name = "Comments")]
        public string submission_content { get; set; }

        [Display(Name ="Approved by")]
        public int editor_id { get; set; }

        [Display(Name ="Editor email")]
        public string editor_email { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool approved { get; set; }



    }
}