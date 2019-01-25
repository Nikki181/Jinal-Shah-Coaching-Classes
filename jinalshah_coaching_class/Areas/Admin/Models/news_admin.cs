using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace jinalshah_coaching_class.Areas.Admin.Models
{
    public class news_admin
    {
        public int pk_news_id
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Please Give News Title")]
        public string news_title
        {
            get;
            set;
        }

      [Required(ErrorMessage = "Please Give News Description")]
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

        [Required(ErrorMessage = "Please Give News Date")]
        public string news_date
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please Give News Isactive")]
        public string news_isactive
        {
            get;
            set;
        }
    }
}