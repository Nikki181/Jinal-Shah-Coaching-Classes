using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jinalshah_coaching_class.Areas.User_data.Models
{
    public class events
    {
        public int pk_event_id
        {
            get;
            set;
        }

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

        public string event_date
        {
            get;
            set;
        }

        public string event_time
        {
            get;
            set;
        }
    }
}