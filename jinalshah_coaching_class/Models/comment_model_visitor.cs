using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace jinalshah_coaching_class.Models
{
    public class comment_model_visitor
    {
        public int pk_comment_id { get; set; }
        public int fk_blog_id { get; set; }
        public string fk_email_id { get; set; }
       public string comment { get; set; }
        public DateTime cmt_date { get; set; }
        public string fname { get; set; }
        public string photo { get; set; }
    }
}