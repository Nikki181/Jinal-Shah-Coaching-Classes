using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace jinalshah_coaching_class.Areas.Admin.Models
{
    public class comment_admin
    {
        public int pk_comment_id { get; set; }
        public int fk_blog_id { get; set; }
        public string fk_email_id { get; set; }
        [AllowHtml]
        public string comment { get; set; }
        public DateTime cmt_date { get; set; }
        public string subject { get; set; }
        public int pk_blog_id { get; set; }
        [AllowHtml]
        public string blog_desc { get; set; }
        public DateTime blog_date_ { get; set; }
        public string blog_image { get; set; }
        public string fname { get; set; }
        public string photo { get; set; }
    }
}