using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jinalshah_coaching_class.Models;
using jinalshah_coaching_class.Entity_Model;

namespace jinalshah_coaching_class.Controllers
{
    public class Visitor_galleryController : Controller
    {
        //
        // GET: /Visitor_gallery/

        Database1Entities _entity = new Database1Entities();

        album_visitor _a = new album_visitor();
        gallery_visitor _g = new gallery_visitor();
        and_gallery_tbl _gtbl = new and_gallery_tbl();

        public ActionResult Index()
        {
            List<album_visitor> objlist = new List<album_visitor>();
            var q = (from r in _entity.and_album_tbl

                     select r).ToList();

            if (q.Count > 0)
            {
                foreach (var item in q)
                {
                    objlist.Add(new album_visitor() { pk_album_id = item.pk_album_id, album_name = item.album_name });
                }
            }

            return View("all_gallery_visitor", objlist);
        }


        public ActionResult view_img(int id)
        {

            List<gallery_visitor> objlist = new List<gallery_visitor>();
            var q = (from r in _entity.and_gallery_tbl
                     where r.fk_album_id == id
                     select r).ToList();

            if (q.Count > 0)
            {
                foreach (var item in q)
                {
                    int id1 = Convert.ToInt16(item.fk_album_id);
                    objlist.Add(new gallery_visitor()
                    {
                        pk_gallery_id = item.pk_gallery_id,
                        gallery_image = item.gallery_image,
                        fk_album_id = id1,
                        gallery_caption = item.gallery_caption,
                        location = item.location
                    });

                }
            }
            else
            {
                ViewBag.message = "No Images yet";
            }
            
            return View("gallery_visitor", objlist);
        }


    }
}
