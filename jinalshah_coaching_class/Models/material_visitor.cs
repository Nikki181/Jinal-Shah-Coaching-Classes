using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jinalshah_coaching_class.Models
{
    public class material_visitor
    {
        public int pk_material_id { get; set; }
        public int fk_grp_id { get; set; }
        public string fk_email_id { get; set; }
        public string material_name { get; set; }
        public string material_path { get; set; }
        public DateTime material_date { get; set; }
        public string type_of_material { get; set; }
        public string fname { get; set; }
        public int status { get; set; }
    }
}