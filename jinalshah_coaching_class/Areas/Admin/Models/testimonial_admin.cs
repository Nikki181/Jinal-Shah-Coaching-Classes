using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jinalshah_coaching_class.Areas.Admin.Models
{
    public class testimonial_admin
    {
        public int Id
        {
            get;
            set;
        }

        public string fk_email_id
        {
            get;
            set;
        }

        public string desc
        {
            get;
            set;
        }

        public DateTime date
        {
            get;
            set;
        }

        public string fname
        {
            get;
            set;
        }
        public string status { set; get; }

        public string photo
        {
            get;
            set;
        }
    }
}