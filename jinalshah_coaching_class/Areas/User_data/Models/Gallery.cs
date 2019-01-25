using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jinalshah_coaching_class.Areas.User_data.Models
{
    public class Gallery
    {
        public int pk_album_id { get; set; }
        public int pk_gallery_id { get; set; }
        public string gallery_image { get; set; }
        public int fk_album_id { get; set; }
        public string gallery_caption { get; set; }
        public string location { get; set; }
        public string album_name { get; set; }
    }
}