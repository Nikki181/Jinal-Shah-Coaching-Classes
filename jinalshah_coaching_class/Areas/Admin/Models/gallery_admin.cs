using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace jinalshah_coaching_class.Areas.Admin.Models
{
    public class gallery_admin
    {
        public int pk_album_id { get; set; }
        public int pk_gallery_id { get; set; }
        public string gallery_image{ get; set; }

        [Required(ErrorMessage = "Please Select Album")]
        public int fk_album_id { get; set; }
        public string  gallery_caption { get; set; }
        public string location { get; set; }
        public string album_name { get; set; }
    }
}