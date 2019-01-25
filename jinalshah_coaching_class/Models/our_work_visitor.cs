using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jinalshah_coaching_class.Models
{
    public class our_work_visitor
    {
        public int pk_our_work_id { get; set; }
        public string our_work_image { get; set; }
        public string project_name { get; set; }
        public string project_desc { get; set; }
        public int team_size { get; set; }
        public string members_name { get; set; }
        public string days_to_complete { get; set; }
        public string type { get; set; }
        public string platform { get; set; }
        public string link { get; set; }
    }
}