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
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name ="Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(12, MinimumLength =6)]
        [Display(Name ="Password: ")]
        public string Password { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Please confirm your password.")]
        public string ConfirmPassword { get; set; }

        
        public int OrganizationId { get; set; }

        [Display(Name = "Role")]
        public int RoleId { get; set; }
        
        public string RoleName { get; set; }

        [Display(Name ="Organization")]
        public string OrgName { get; set; }

        public bool Active { get; set; }

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