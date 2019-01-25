using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jinalshah_coaching_class.Entity_Model;
using jinalshah_coaching_class.Areas.Admin.Models;

namespace jinalshah_coaching_class.Areas.Admin.Controllers
{
    public class Admin_albumController : Controller
    {
        //
        // GET: /Admin/Admin_album/

        Database1Entities _entity = new Database1Entities();

        album_admin _a = new album_admin();
        and_album_tbl _atbl = new and_album_tbl();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult add_album()
        {
            return View("add_album_admin");
        }
        public ActionResult insert_album(FormCollection frm)
        {
            var name=frm["name"];
            _atbl.album_name = name;
            _entity.and_album_tbl.Add(_atbl);
            _entity.SaveChanges();

            return RedirectToAction("view_album", "Admin_album");


        }
         public ActionResult view_album()
        {
       
         List<album_admin> objlist = new List<album_admin>();
            var q = (from r in _entity.and_album_tbl
                    
                     select r).ToList();

            if (q.Count > 0)
            {
                foreach (var item in q)
                {
                    objlist.Add(new album_admin(){pk_album_id=item.pk_album_id,album_name=item.album_name});
                }
            }
             return View("view_album_admin",objlist);
         }

         public ActionResult Delete(int id)
         {

             try
             {
                 var data = (from r in _entity.and_gallery_tbl
                             where r.fk_album_id == id
                             select r);

                 if (data.ToList().Count > 0)
                 {
                     foreach (var item in data)
                     {
                         _entity.and_gallery_tbl.Remove(item);
                     }


                 }

                 var q = (from r in _entity.and_album_tbl
                          where r.pk_album_id == id
                          select r).FirstOrDefault();

                 _entity.and_album_tbl.Remove(q);
                 _entity.SaveChanges();
                 return RedirectToAction("view_album", "Admin_album");

             }
             catch
             {
                 return View("Error");

             }
         }
    }
}
