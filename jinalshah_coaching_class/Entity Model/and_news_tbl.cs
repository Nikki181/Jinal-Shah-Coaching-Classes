//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace jinalshah_coaching_class.Entity_Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class and_news_tbl
    {
        public int pk_news_id { get; set; }
        public string news_title { get; set; }
        public string news_desc { get; set; }
        public string news_image { get; set; }
        public Nullable<System.DateTime> news_date { get; set; }
        public string news_isactive { get; set; }
    }
}