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
    
    public partial class and_gallery_tbl
    {
        public int pk_gallery_id { get; set; }
        public string gallery_image { get; set; }
        public int fk_album_id { get; set; }
        public string gallery_caption { get; set; }
        public string location { get; set; }
    }
}
