using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiresias.Models
{
    [Table("translators")]
    public class Translator
    {
        [Key]
        public int translator_id { get; set; }

        public string translator_name { get; set; }

        public int organization_id { get; set; }


    }
}