using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace jinalshah_coaching_class.Areas.Admin.Models
{
    public class events_admin
    {
        public int pk_event_id
        {
            get;
            set;
        }
         [Required(ErrorMessage = "Please Give Event Name")]
        public string event_name
        {
            get;
            set;
        }

        public string event_desc
        {
            get;
            set;
        }
    [Required(ErrorMessage = "Please Give Event Place")]
        public string event_place
        {
            get;
            set;
        }


        public string speaker
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Please Give Event Topic")]
        public string topic
        {
            get;
            set;
        }

        public string event_img
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Please Give Event Date")]
        public string event_date
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Please Give Event Time")]
        public string event_time
        {
            get;
            set;
        }
    }
}