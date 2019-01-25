using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jinalshah_coaching_class.Areas.User_data.Models
{
    public class answer_model
    {
        public int pk_ans_id { get; set; }
        public int fk_que_id { get; set; }

        [AllowHtml]
        public string answer { get; set; }

        public string ans_img { get; set; }
        public string fk_emai_id { get; set; }
        public DateTime a_date { get; set; }

        public int pk_que_id { get; set; }
        public string que_title { get; set; }
        public string que_desc { get; set; }
        public string que_img { get; set; }
        public string fk_email_id { get; set; }
        public int fk_grp_id { get; set; }
        public DateTime que_date { get; set; }

        public string fname { get; set; }
        public string photo { get; set; } 
    }
}