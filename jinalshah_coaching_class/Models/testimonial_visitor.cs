using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jinalshah_coaching_class.Models
{
    public class testimonial_visitor
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

        public string photo
        {
            get;
            set;
        }

    }
}