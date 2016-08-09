using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Tiresias.Models
{
    [Table("organizations")]
    public class Organization
    {
        [Key]
        public int organization_id { get; set; }

        public string organization_name { get; set; }

        public string oganization_abrev { get; set; }



    }
}