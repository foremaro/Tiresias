using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tiresias.Models
{
    public class Author
    {
        
        public int author_id { get; set; }

        public string last_name { get; set; }

        public string first_name { get; set; }



    }
}