using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Tiresias.Models
{
    public class Post
    {
        [HiddenInput(DisplayValue = false)]
        public int post_id { get; set; }

        public string post_type { get; set; }

        [Display(Name = "Title")]
        public string post_title { get; set; }

        [DisplayFormat(DataFormatString ="{0:MMM dd, yyyy}")]
        [Display(Name ="Date Posted")]
        public DateTime post_date { get; set; }

        public string post_content { get; set; }
        


    }
}