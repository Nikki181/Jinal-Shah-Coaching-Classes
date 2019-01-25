using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jinalshah_coaching_class.Entity_Model;
using jinalshah_coaching_class.Areas.Admin.Models;

namespace jinalshah_coaching_class.Areas.Admin.Controllers
{
    public class Admin_galleryController : Controller
    {
        //
        // GET: /Admin/Admin_gallery/
        Database1Entities _entity = new Database1Entities();

        gallery_admin _g  = new gallery_admin();
        and_gallery_tbl _gtbl = new and_gallery_tbl();

        album_admin _a = new album_admin();
        and_album_tbl _atbl = new and_album_tbl();

        public ActionResult add_gallery_admin()
        {
            getalbumname();
            return View();
           
        }
        private void getalbumname()
        {
            var data = from c in _entity.and_album_tbl
                       select c.album_name;
            ViewBag.allalbum = data;
        }

        public static int aid=0;
        public ActionResult insert_img(FormCollection frm)
        {
            var anm = frm["album_name"];
            var cap = frm["caption"];
            var loc = frm["loc"];
            var q = (from c in _entity.and_album_tbl
                     where c.album_name == anm
                     select c).FirstOrDefault();
            _gtbl.fk_album_id = q.pk_album_id;

            aid = q.pk_album_id;

            HttpPostedFileBase hp = Request.Files["fileuploadDemo"];
            if (hp != null && hp.FileName != "")
            {
                string img = hp.FileName;
                string path = "~/Gallery_img/" + img;
                hp.SaveAs(Server.MapPath(path));
                _gtbl.gallery_image = path;
            }

          
            _gtbl.gallery_caption = cap;
            _gtbl.location = loc;
            
            _entity.and_gallery_tbl.Add(_gtbl);
            _entity.SaveChanges();
            
            return RedirectToAction("view_gallary1", "Admin_gallery");
        }

        public ActionResult view_gallary1()
        {
         
            List<gallery_admin> objlist = new List<gallery_admin>();
            var q = (from r in _entity.and_gallery_tbl
                     where r.fk_album_id == aid
                     select r).ToList();

            if (q.Count > 0)
            {
                foreach (var item in q)
                {
                    int id1 = Convert.ToInt16(item.fk_album_id);
                    objlist.Add(new gallery_admin()
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

            return View("view_gallery_admin", objlist);
        }

        public ActionResult view_gallary(int id)
        {
            List<gallery_admin> objlist = new List<gallery_admin>();
            var q = (from r in _entity.and_gallery_tbl
                     where r.fk_album_id==id
                     select r).ToList();

            if (q.Count > 0)
            {
                foreach (var item in q)
                {
                    int id1 = Convert.ToInt16(item.fk_album_id);
                    objlist.Add(new gallery_admin()
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

            return View("view_gallery_admin", objlist);
        }
        public ActionResult Delete(int id)
        {
            var data = (from r in _entity.and_gallery_tbl
                        where r.pk_gallery_id == id
                        select r).FirstOrDefault();

            if (data.gallery_image != null || data.gallery_image != "")
            {
                if (data.gallery_image != "~/Blog_image/no_image.gif")
                {
                    System.IO.File.Delete(Server.MapPath(data.gallery_image));
                }  
            }
            _entity.and_gallery_tbl.Remove(data);
            _entity.SaveChanges();

            return RedirectToAction("view_gallary1");
        }

    }
}
