using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiresias.Models
{
    [Table("authors")]
    public class Author
    {
        [Key]
        public int author_id { get; set; }

        public string last_name { get; set; }

        public string first_name { get; set; }



    }
}