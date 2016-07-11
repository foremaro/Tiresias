using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tiresias.Models
{
    public class Work
    {
        public int work_id { get; set; }

        public string title { get; set; }

        public string edition { get; set; }

        public int author_id { get; set; }

        public int language_id { get; set; }

        public int translator_id { get; set; }

        public DateTime submission_date { get; set; }

        public int user_entry_id { get; set; }

        public int metadata_id { get; set; }

        public int submission_id { get; set; }


    }
}