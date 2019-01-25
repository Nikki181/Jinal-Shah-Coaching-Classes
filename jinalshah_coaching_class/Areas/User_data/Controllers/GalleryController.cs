using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jinalshah_coaching_class.Areas.User_data.Models;
using jinalshah_coaching_class.Entity_Model;

namespace jinalshah_coaching_class.Areas.User_data.Controllers
{
    public class GalleryController : Controller
    {
        //
        // GET: /Gallery/
        Database1Entities _entity = new Database1Entities();

        Album _a = new Album();
        Gallery _g=new Gallery();
        and_gallery_tbl _gtbl=new and_gallery_tbl();

        public ActionResult Index()
        {
            List<Album> objlist = new List<Album>();
            var q = (from r in _entity.and_album_tbl
                    
                     select r).ToList();

            if (q.Count > 0)
            {
                foreach (var item in q)
                {
                    objlist.Add(new Album(){pk_album_id=item.pk_album_id,album_name=item.album_name});
                }
            }

             return View("all_album",objlist);
         }

        
         public ActionResult view_img(int id)
        {
         
            List<Gallery> objlist = new List<Gallery>();
            var q = (from r in _entity.and_gallery_tbl
                     where r.fk_album_id==id
                     select r).ToList();

            if (q.Count > 0)
            {
                foreach (var item in q)
                {
                    int id1 = Convert.ToInt16(item.fk_album_id);
                    objlist.Add(new Gallery()
                    {
                       pk_gallery_id=item.pk_gallery_id,
                       gallery_image=item.gallery_image,
                       fk_album_id=id1,
                       gallery_caption=item.gallery_caption,
                       location=item.location
                    });

                }
            }
            else
            {
                ViewBag.message = "No Images yet";
            }
           // ViewBag.list = objlist;
            return View("gallery", objlist);
        }

      }
}
