using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jinalshah_coaching_class.Areas.Admin.Models;
using jinalshah_coaching_class.Entity_Model;
using PagedList;
using PagedList.Mvc;


namespace jinalshah_coaching_class.Areas.Admin.Controllers
{
    public class Admin_blogController : Controller
    {
        //
        // GET: /Admin/Admin_blog/
        blog_admin _b = new blog_admin();
        and_blog_tbl _btbl = new and_blog_tbl();
        Database1Entities db = new Database1Entities();
        and_comment_tbl ctbl = new and_comment_tbl();



        public ActionResult Index()
        {
            return RedirectToAction("view_blog_admin", "Admin_blog");
        }

        [ValidateInput(false)]
        public ActionResult insert_blog_admin(FormCollection frm)
        {
            var sub = frm["txtsub"];
            var desc = frm["blog_desc"];

            HttpPostedFileBase hp = Request.Files["fileuploadDemo"];
            if (hp != null && hp.FileName != "")
            {
                string img = hp.FileName;
                string path = "~/Blog_image/" + img;
                hp.SaveAs(Server.MapPath(path));
                _btbl.blog_image = path;
            }
            else
            {
                _btbl.blog_image = "~/Blog_image/no_image.gif";
            }
            if (desc.ToString() == null || desc.ToString() == "")
            {
                ViewBag.msg = "Please add description";
                return View("add_blog_admin");
            }
            else
            {

                _btbl.subject = sub;
                _btbl.blog_desc = desc;


                var eid = "";
                eid = Convert.ToString(Session["email-id"]);
                _btbl.fk_email_id = eid;
                var dt = System.DateTime.Now;
                _btbl.blog_date_ = dt;

                db.and_blog_tbl.Add(_btbl);
                db.SaveChanges();
                return RedirectToAction("view_blog_admin", "Admin_blog");
            }
    
        }

        public ActionResult view_blog_admin(int? Page_No)
        {
            List<blog_admin> objlist = new List<blog_admin>();
            var q = (from r in db.and_blog_tbl

                     orderby r.pk_blog_id descending
                     select new { r.subject, r.pk_blog_id, r.blog_desc, r.blog_image, r.blog_date_ });

            if (q.Count() > 0)
            {

                foreach (var item in q)
                {
                    objlist.Add(new blog_admin() { pk_blog_id = item.pk_blog_id, subject = item.subject, blog_image = item.blog_image, blog_date_ = item.blog_date_, blog_desc = item.blog_desc });

                }
            }
            else
            {
                ViewBag.message = "No Blogs yet";
            }

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            return View(objlist.ToPagedList(No_Of_Page, Size_Of_Page));


        }

        public ActionResult addview_load()
        {
            return View("add_blog_admin");
        }

        public static int bid;
        public ActionResult detail(int id)
        {
            var q = (from b1 in db.and_blog_tbl

                     where b1.pk_blog_id == id
                     select b1).FirstOrDefault();


            bid = id;
            var q2 = (from c in db.and_blog_tbl
                      where c.pk_blog_id == id
                      select c).FirstOrDefault();
            _b.blog_desc = q2.blog_desc;
            _b.blog_date_ = System.DateTime.Now;
            _b.blog_image = q2.blog_image;
            _b.fk_email_id = Convert.ToString(Session["email-id"]);

            _b.subject = q2.subject;
            List<comment_admin> objlist = new List<comment_admin>();
            var co = (from c in db.and_comment_tbl
                      join b2 in db.and_blog_tbl on c.fk_blog_id equals b2.pk_blog_id
                      join u in db.and_user_tbl on c.fk_email_id equals u.pk_email_id
                      where b2.pk_blog_id == id
                      select new { c.pk_comment_id, c.comment, c.cmt_date, u.fname, u.photo }).ToList();
            ViewBag.cmt = co.Count();
            if (co.Count > 0)
            {
                foreach (var item in co)
                {
                    objlist.Add(new comment_admin() { pk_comment_id = item.pk_comment_id, comment = item.comment, cmt_date = item.cmt_date, fname = item.fname, photo = item.photo });
                }
            }
            else
            {
                ViewBag.comment = "No comments yet";
            }
            ViewBag.comment = objlist;
            return View("View1_blog_admin", _b);
        }

        [ValidateInput(false)]
        public ActionResult add_cmt(FormCollection frm)
        {
            var sub = frm["name"];
            var desc = frm["comment"];
            if (desc.ToString() == null || desc.ToString() == "")
            {
                ViewBag.msg = "Please add comment";
            }
            else
            {
                ctbl.fk_blog_id = bid;
                ctbl.fk_email_id = Convert.ToString(Session["email-id"]);
                ctbl.comment = desc;
                ctbl.cmt_date = System.DateTime.Now;
                db.and_comment_tbl.Add(ctbl);
                db.SaveChanges();
            }
            var q = (from b1 in db.and_blog_tbl

                     where b1.pk_blog_id == bid
                     select b1).FirstOrDefault();


            var q2 = (from c in db.and_blog_tbl
                      where c.pk_blog_id == bid
                      select c).FirstOrDefault();
            _b.blog_desc = q2.blog_desc;
            _b.blog_date_ = System.DateTime.Now;
            _b.blog_image = q2.blog_image;
            _b.fk_email_id = Convert.ToString(Session["email-id"]);

            _b.subject = q2.subject;
            List<comment_admin> objlist = new List<comment_admin>();
            var co = (from c in db.and_comment_tbl
                      join b2 in db.and_blog_tbl on c.fk_blog_id equals b2.pk_blog_id
                      join u in db.and_user_tbl on c.fk_email_id equals u.pk_email_id
                      where b2.pk_blog_id == bid
                      select new { c.pk_comment_id, c.comment, c.cmt_date, u.fname, u.photo }).ToList();
            ViewBag.cmt = co.Count();
            if (co.Count > 0)
            {
                foreach (var item in co)
                {
                    objlist.Add(new comment_admin() { pk_comment_id = item.pk_comment_id, comment = item.comment, cmt_date = item.cmt_date, fname = item.fname, photo = item.photo });
                }
            }
            else
            {
                ViewBag.comment = "No comments yet";
            }
            ViewBag.comment = objlist;
            return View("View1_blog_admin", _b);


        }

        public ActionResult blog_edit(int id)
        {

            var data = (from r in db.and_blog_tbl

                        where r.pk_blog_id == id
                        select new { r.blog_desc, r.blog_image, r.subject,r.blog_date_}).FirstOrDefault();
            blog_admin _ba = new blog_admin();
            _ba.blog_desc = data.blog_desc;
            _ba.subject = data.subject;
            _ba.blog_image = data.blog_image;
            _ba.blog_date_ = System.DateTime.Now;
            
            return View("edit_blog_admin", _ba);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult blog_edit(int id, FormCollection collection)
        {

            var data = (from r in db.and_blog_tbl
                        where r.pk_blog_id == id
                        select r).FirstOrDefault();


            data.blog_desc = collection["blog_desc"];
            HttpPostedFileBase hp = Request.Files["fileuploadDemo"];
            if (hp != null && hp.FileName != "")
            {
                string path = "~/que_image/" + hp.FileName;
                if (data.blog_image != path)
                {
                    if (data.blog_image == "~/Blog_image/no_image.gif")
                    {

                        hp.SaveAs(Server.MapPath(path));
                    }
                    else
                    {
                        System.IO.File.Delete(Server.MapPath(data.blog_image));
                        hp.SaveAs(Server.MapPath(path));
                    }
                }
                data.blog_image = path;
            }
            data.blog_date_ = Convert.ToDateTime(collection["blog_date_"]);
            data.subject = collection["subject"];


            blog_admin _ba = new blog_admin();
            _ba.blog_desc = data.blog_desc;
            _ba.subject = data.subject;
            _ba.blog_image = data.blog_image;
            _ba.blog_date_ = System.DateTime.Now;


            db.SaveChanges();
            return RedirectToAction("view_blog_admin", _ba);
        }


        public ActionResult blog_delete(int id)
        {
            var data = (from r in db.and_comment_tbl
                        where r.fk_blog_id == id
                        select r);

            if (data.ToList().Count > 0)
            {
                foreach (var item in data)
                {
                    db.and_comment_tbl.Remove(item);
                }


            }
            var q = (from r in db.and_blog_tbl
                     where r.pk_blog_id == id
                     select r).FirstOrDefault();

            if (q.blog_image != null || q.blog_image != "")
            {
                if (q.blog_image != "~/Blog_image/no_image.gif")
                {
                    System.IO.File.Delete(Server.MapPath(q.blog_image));
                }
            }

            db.and_blog_tbl.Remove(q);
            db.SaveChanges();

            return RedirectToAction("view_blog_admin", "Admin_blog");

        }
    }
}
