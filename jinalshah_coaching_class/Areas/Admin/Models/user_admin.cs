using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace jinalshah_coaching_class.Areas.Admin.Models
{
    public class user_admin
    {
        public int Id { get; set; }
        public string pk_email_id { get; set; }
        public string password { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string address { get; set; }
        public string gender { get; set; }
        public string city { get; set; }
        public string phone_no { get; set; }
        public string country { get; set; }
        [Required(ErrorMessage = "Please Enter Status")]
        public string status { get; set; }
        public string semester { get; set; }
        public string photo { get; set; }
        
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

        

       
    }
}