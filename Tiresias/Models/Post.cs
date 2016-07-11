using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tiresias.Models
{
    public class Post
    {

        public int post_id { get; set; }

        public string post_type { get; set; }

        public string post_title { get; set; }

        public DateTime post_date { get; set; }

        public string post_content { get; set; }


    }
}