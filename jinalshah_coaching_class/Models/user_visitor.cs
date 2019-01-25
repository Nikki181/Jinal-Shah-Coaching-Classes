using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jinalshah_coaching_class.Models
{
    public class user_visitor
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
        public string status { get; set; }
        public string semester { get; set; }
        public string photo { get; set; }
    }
}