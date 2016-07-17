using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tiresias.Models
{
    public class User
    {
        [Required]
        public int user_id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name ="Email")]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(12, MinimumLength =6)]
        [Display(Name ="Password: ")]
        public string password { get; set; }

        public int organization_id { get; set; }

        public int role_id { get; set; }
    }
}