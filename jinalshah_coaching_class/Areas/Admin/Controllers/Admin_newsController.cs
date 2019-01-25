using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jinalshah_coaching_class.Areas.Admin.Models;
using jinalshah_coaching_class.Entity_Model;


namespace jinalshah_coaching_class.Areas.Admin.Controllers
{
    public class Admin_newsController : Controller
    {
        //
        // GET: /Admin/Admin_news/

        and_news_tbl _nt = new and_news_tbl();
        news_admin n_model = new news_admin();
        Database1Entities db = new Database1Entities();
        public ActionResult Index()
        {
            return RedirectToAction("news_all_admin");
        }

        public ActionResult add_news_tmp()
        {
            return View("add_news_admin");
        }

        public ActionResult add_news1(FormCollection frm)
        {
            //return View("add_news_admin");
            var title = frm["news_title"];

            var desc = frm["news_desc"];

            HttpPostedFileBase hp = Request.Files["fileuploadDemo"];
            if (hp != null && hp.FileName != "")
            {
                string img = hp.FileName;
                string path = "~/Images/" + img;
                hp.SaveAs(Server.MapPath(path));
                _nt.news_image = path;
            }

            else
            {
                _nt.news_image = "~/Blog_image/no_image.gif";
            }
           
            _nt.news_title = title;

            _nt.news_desc = desc;

           
            var dt = System.DateTime.Now;
            _nt.news_date = dt;
            _nt.news_isactive = "0";
            db.and_news_tbl.Add(_nt);
            db.SaveChanges();
                
            return RedirectToAction("news_all_admin", "Admin_news");
        }
        public ActionResult news_all_admin()
        {

            var q = (from r in db.and_news_tbl
                     select r).ToList();
            List<news_admin> lst = new List<news_admin>();
            foreach (var item in q)
            {
                lst.Add(new news_admin() { pk_news_id = item.pk_news_id, news_title = item.news_title, news_desc = item.news_desc, news_image = item.news_image, news_date = item.news_date.ToString(), news_isactive = item.news_isactive });
            }
            Session["count"] = lst.Count;
            
            return View("news_view_admin",lst);
        }

       

        public ActionResult news_edit()
        {

            //var data = (from r in db.and_news_tbl
            //            where r.pk_news_id == id
            //            select r).FirstOrDefault();
            //news_admin _nt = new news_admin();
         
            //_nt.news_title = data.news_title;

            //_nt.news_image = data.news_image;
            //_nt.news_desc = data.news_desc;
            //_nt.news_date = data.news_date.ToString();
            //_nt.news_isactive = data.news_isactive;

            return View("news_edit_admin");
        }

        [HttpPost]
        public ActionResult news_edit(int id, FormCollection collection)
        {

            var data = (from r in db.and_news_tbl
                        where r.pk_news_id == id
                        select r).FirstOrDefault();

           
            data.news_title = collection["news_title"];
            HttpPostedFileBase hp = Request.Files["fileuploadDemo"];
            if (hp != null && hp.FileName != "")
            {
                string path = "~/Images/" + hp.FileName;
                if (data.news_image != path)
                {
                    if (data.news_image == "~/Blog_image/no_image.gif")
                    {

                        hp.SaveAs(Server.MapPath(path));
                    }
                    else
                    {
                        System.IO.File.Delete(Server.MapPath(data.news_image));
                        hp.SaveAs(Server.MapPath(path));
                    }
                }
                data.news_image = path;
            }
            data.news_desc = collection["news_desc"];
            data.news_date = Convert.ToDateTime(collection["news_date"]);
            data.news_isactive = collection["news_isactive"];
            news_admin _nt = new news_admin();
            
            _nt.news_title = data.news_title;
            _nt.news_image = data.news_image;
            _nt.news_desc = data.news_desc;
            _nt.news_date = data.news_date.ToString();
            _nt.news_isactive = data.news_isactive;

            db.SaveChanges();
            return RedirectToAction("news_all_admin");
        }

        public ActionResult news_delete(int id)
        {
            var data = (from r in db.and_news_tbl
                        where r.pk_news_id == id
                        select r).FirstOrDefault();

            if (data.news_image != null || data.news_image != "")
            {
                if (data.news_image != "~/Blog_image/no_image.gif")
                {
                    System.IO.File.Delete(Server.MapPath(data.news_image));
                }
            }
                
            db.and_news_tbl.Remove(data);
            db.SaveChanges();

            return RedirectToAction("news_all_admin");
        }
        public ActionResult DeleteAll(FormCollection frm)
        {
            var chkvalue = frm.GetValues("assignChkBx");
            foreach (var id in chkvalue)
            {
                news_admin n_m = new news_admin();
                int id1 = Convert.ToInt32(id);
                var data = (from c in db.and_news_tbl where c.pk_news_id == id1 select c).First();
                if (data.news_image != null || data.news_image != "")
                {
                    if (data.news_image != "~/Blog_image/no_image.gif")
                    {
                        System.IO.File.Delete(Server.MapPath(data.news_image));
                    }
                }
           
                db.and_news_tbl.Remove(data);
                db.SaveChanges();
            }
            return RedirectToAction("news_all_admin", "Admin_news");
        }

    }
}
