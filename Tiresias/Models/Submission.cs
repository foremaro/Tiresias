using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tiresias.Models
{
    public class Submission
    {

        public int submission_id { get; set; }

        public DateTime submission_date { get; set; }

        public string submission_email { get; set; }

        public int work_id { get; set; }

        public string submission_content { get; set; }

        public int editor_id { get; set; }

        public bool approved { get; set; }



    }
}