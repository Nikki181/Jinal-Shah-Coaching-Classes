using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jinalshah_coaching_class.Models
{
    public class news_visitor
    {
        public int pk_news_id
        {
            get;
            set;
        }

        public string news_title
        {
            get;
            set;
        }

        public string news_desc
        {
            get;
            set;
        }

        public string news_image
        {
            get;
            set;
        }

        public string news_date
        {
            get;
            set;
        }

        public string news_isactive
        {
            get;
            set;
        }
    }
}