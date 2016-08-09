using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Tiresias.Models
{
    [Table("works")]
    public class Work
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int work_id { get; set; }

        [Display(Name ="Title")]
        public string title { get; set; }

        [Display(Name ="Edition")]
        public string edition { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int author_id { get; set; }

        [Display(Name ="Author")]
        public string author_name { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int language_id { get; set; }

        [Display(Name = "Language")]
        public string language { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int translator_id { get; set; }

        [Display(Name = "Translated By")]
        public string translator_name { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int organization_id { get; set; }
        public string organization_name { get; set; }

        

        [HiddenInput(DisplayValue = false)]
        public int user_entry_id { get; set; }

        [Display(Name ="Entered by User")]
        public string user_entry_email { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int metadata_id { get; set; }

        [Display(Name = "Media Type")]
        public string media_type { get; set; }

        [Display(Name = "DOI")]
        public string doi { get; set; }

        [Display(Name = "ISBN")]
        public string isbn { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int submission_id { get; set; }


    }
}