using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tiresias.Models
{
    [Table("users")]
    public class User
    {
        [Required]
        [Key]
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
        [Display(Name = "Role")]
        public int role_id { get; set; }
        
        public string RoleName { get; set; }
        [Display(Name ="Organization")]
        public string OrgName { get; set; }

        public bool active { get; set; }

        //public string Id
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public string UserName
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }

        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }
}