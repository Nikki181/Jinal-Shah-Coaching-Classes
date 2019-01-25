using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jinalshah_coaching_class.Models
{
    public class poll_visitor
    {
        public int pk_poll_id { get; set; }
        public string poll_que { get; set; }
        public string yes { get; set; }
        public string no { get; set; }
        public string yes_count { get; set; }
        public string no_count { get; set; }
        public string poll_isactive { get; set; }
    }
}