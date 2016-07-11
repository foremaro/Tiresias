using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tiresias.Models
{
    public class User
    {
        public int user_id { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public int organization_id { get; set; }

        public int role_id { get; set; }
    }
}