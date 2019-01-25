using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace jinalshah_coaching_class.Models
{
    public class question_visitor
    {
        public int pk_que_id { get; set; }
        public string que_title { get; set; }
        [AllowHtml]
        public string que_desc { get; set; }
        public string que_img { get; set; }
        public string fk_email_id { get; set; }
        public int fk_grp_id { get; set; }
        public DateTime que_date { get; set; }
        public string fname { get; set; } 
    }
}