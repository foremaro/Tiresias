using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiresias.Models
{
    [Table("metadata")]
    public class Metadata
    {
        [Key]
        public int metadata_id { get; set; }

        public string media_type { get; set; }

        public string doi { get; set; }

        public string isbn { get; set; }



    }
}