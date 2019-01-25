using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace jinalshah_coaching_class.Areas.User_data.Models

{
    
    public class Blog
    {
        public int pk_blog_id { get; set; }
        [AllowHtml]
        public string blog_desc { get; set; }
        public string subject { get; set; }
        public string fk_email_id { get; set; }
        public string blog_image { get; set; }
        public DateTime blog_date_ { get; set; }
        public string fname { get; set; }

    }
}