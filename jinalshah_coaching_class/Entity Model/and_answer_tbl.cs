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
    
    public partial class and_answer_tbl
    {
        public int pk_ans_id { get; set; }
        public int fk_que_id { get; set; }
        public string answer { get; set; }
        public string ans_img { get; set; }
        public string fk_email_id { get; set; }
        public Nullable<System.DateTime> a_date { get; set; }
    }
}
