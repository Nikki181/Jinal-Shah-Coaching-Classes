﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jinalshah_coaching_class.Areas.Admin.Models
{
    public class faq_admin
    {
        public int pk_faq_id
        {
            get;
            set;
        }

        public string que
        {
            get;
            set;
        }

        public string ans
        {
            get;
            set;
        }
    }
}