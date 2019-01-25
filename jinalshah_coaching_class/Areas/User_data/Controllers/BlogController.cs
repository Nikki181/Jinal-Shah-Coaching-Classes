using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jinalshah_coaching_class.Areas.User_data.Models;
using jinalshah_coaching_class.Entity_Model;
using PagedList;
using PagedList.Mvc;


namespace jinalshah_coaching_class.Areas.User_data.Controllers
{
    public class BlogController : Controller
    {
        //
        // GET: /Blog/

        Blog b = new Blog();
        and_blog_tbl _btbl = new and_blog_tbl();
        Database1Entities db = new Database1Entities();
        and_comment_tbl ctbl = new and_comment_tbl();
        comment_model c = new comment_model();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult loadview()
        {
            return RedirectToAction("blog_view", "Blog");
        }

        //[ValidateInput(false)]
        //public ActionResult insert(FormCollection frm)
        //{
        //    var sub = frm["txtsub"];
        //    var desc = frm["blog_desc"];

        //    HttpPostedFileBase hp = Request.Files["fileuploadDemo"];
        //    if (hp != null && hp.FileName != "")
        //    {
        //        string img = hp.FileName;
        //        string path = "~/Blog_image/" + img;
        //        hp.SaveAs(Server.MapPath(path));
        //        _btbl.blog_image = path;
        //    }
        //    else
        //    {
        //        _btbl.blog_image = "~/Blog_image/no_image.gif";
        //    }


        //    _btbl.subject = sub;
        //    _btbl.blog_desc = desc;


        //    var eid = "";
        //    eid = Convert.ToString(Session["email-id"]);
        //    _btbl.fk_email_id = eid;
        //    var dt = System.DateTime.Now;
        //    _btbl.blog_date_ = dt;
        //    db.and_blog_tbl.Add(_btbl);
        //    db.SaveChanges();
        //    return RedirectToAction("blog_view", "Blog");

        //}

        public ActionResult blog_view(int? Page_No)
        {
            List<Blog> objlist = new List<Blog>();
            var q = (from r in db.and_blog_tbl
                     join c in db.and_user_tbl on r.fk_email_id equals c.pk_email_id
                     orderby r.blog_date_ descending
                     select new { r.subject, r.pk_blog_id, r.blog_desc, r.blog_image, r.blog_date_, c.fname });

            foreach (var item in q)
            {
                objlist.Add(new Blog() { pk_blog_id = item.pk_blog_id, subject = item.subject, blog_image = item.blog_image, blog_date_ = item.blog_date_, blog_desc = item.blog_desc, fname = item.fname });

            }
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            return View(objlist.ToPagedList(No_Of_Page, Size_Of_Page));


        }

        public ActionResult addview_load()
        {
            return View("add_blog");
        }

        public static int bid;
        public ActionResult blog_detail(int id)
        {
            var q = (from b1 in db.and_blog_tbl
                     join u in db.and_user_tbl on b1.fk_email_id equals u.pk_email_id
                     where b1.pk_blog_id == id
                     select u).FirstOrDefault();
            ViewBag.fname = q.fname;

            bid = id;
            var q2 = (from c in db.and_blog_tbl
                      where c.pk_blog_id == id
                      select c).FirstOrDefault();
            b.blog_desc = q2.blog_desc;
            b.blog_date_ = System.DateTime.Now;
            b.blog_image = q2.blog_image;
            b.fk_email_id = q2.fk_email_id;
            b.blog_date_ = q2.blog_date_;

            b.subject = q2.subject;
            List<comment_model> objlist = new List<comment_model>();
            var co = (from c in db.and_comment_tbl
                      join b2 in db.and_blog_tbl on c.fk_blog_id equals b2.pk_blog_id
                      join u in db.and_user_tbl on c.fk_email_id equals u.pk_email_id
                      where b2.pk_blog_id == id
                      select new { c.fk_email_id, c.pk_comment_id, c.comment, c.cmt_date, u.fname, u.photo }).ToList();

            ViewBag.cmt = co.Count;
            if (co.Count > 0)
            {
                foreach (var item in co)
                {
                    objlist.Add(new comment_model() { fk_email_id = item.fk_email_id, pk_comment_id = item.pk_comment_id, comment = item.comment, cmt_date = item.cmt_date, fname = item.fname, photo = item.photo });
                }
            }
            else
            {
                ViewBag.comment = "No comments yet";
            }
            ViewBag.comment = objlist;
            return View("View1", b);
        }

        [ValidateInput(false)]
        public ActionResult add_comment(FormCollection frm)
        {
            var sub = frm["name"];
            var desc = frm["comment"];
            if (desc.ToString() == null || desc.ToString() == "")
            {
                 ViewBag.msg = "Please add comment";
            }
            else{

                ctbl.fk_blog_id = bid;

                ctbl.fk_email_id = Convert.ToString(Session["email-id"]);
                ctbl.comment = desc;
                ctbl.cmt_date = System.DateTime.Now;
                db.and_comment_tbl.Add(ctbl);
                db.SaveChanges();
            }

            var q = (from b1 in db.and_blog_tbl
                     join u in db.and_user_tbl on b1.fk_email_id equals u.pk_email_id
                     where b1.pk_blog_id == bid
                     select u).FirstOrDefault();
            ViewBag.fname = q.fname;

            
            var q2 = (from c in db.and_blog_tbl
                      where c.pk_blog_id == bid
                      select c).FirstOrDefault();
            b.blog_desc = q2.blog_desc;
            b.blog_date_ = System.DateTime.Now;
            b.blog_image = q2.blog_image;
            b.fk_email_id = q2.fk_email_id;
            b.blog_date_ = q2.blog_date_;

            b.subject = q2.subject;
            List<comment_model> objlist = new List<comment_model>();
            var co = (from c in db.and_comment_tbl
                      join b2 in db.and_blog_tbl on c.fk_blog_id equals b2.pk_blog_id
                      join u in db.and_user_tbl on c.fk_email_id equals u.pk_email_id
                      where b2.pk_blog_id == bid
                      select new { c.fk_email_id, c.pk_comment_id, c.comment, c.cmt_date, u.fname, u.photo }).ToList();

            ViewBag.cmt = co.Count;
            if (co.Count > 0)
            {
                foreach (var item in co)
                {
                    objlist.Add(new comment_model() { fk_email_id = item.fk_email_id, pk_comment_id = item.pk_comment_id, comment = item.comment, cmt_date = item.cmt_date, fname = item.fname, photo = item.photo });
                }
            }
            else
            {
                ViewBag.comment = "No comments yet";
            }
            ViewBag.comment = objlist;
            return View("View1", b);
           
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var q = (from r in db.and_comment_tbl
                         where r.pk_comment_id == id
                         select r).FirstOrDefault();

                db.and_comment_tbl.Remove(q);
                db.SaveChanges();
                return RedirectToAction("blog_detail", new { id = bid });

            }
            catch
            {
                return View("Error");

            }
        }
    }
}
