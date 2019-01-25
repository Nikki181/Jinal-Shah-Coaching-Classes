using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jinalshah_coaching_class.Models;
using jinalshah_coaching_class.Entity_Model;
using PagedList;
using PagedList.Mvc;


namespace jinalshah_coaching_class.Controllers
{
    public class Visitor_blogController : Controller
    {
        //
        // GET: /Visitor/Visitor_blog/

        
        Blog_visitor b = new Blog_visitor();
        and_blog_tbl _btbl = new and_blog_tbl();
        Database1Entities db = new Database1Entities();
        and_comment_tbl ctbl = new and_comment_tbl();
        comment_model_visitor c = new comment_model_visitor();
     
        public ActionResult Index()
        {
            return View("blog_View_visitor");
        }

        public ActionResult tp1()
        {
            return View("faq_view_admin");
        }

        public ActionResult tp2()
        {
            return View("events_view_admin");
        }
        public ActionResult tp3()
        {
            return View("add_material_admin");
        }
        public ActionResult tp4()
        {
            return View("view_material_admin");
        }
        public ActionResult index1()
        {
            return View("add_blog_admin");
        }
        public ActionResult loadview()
        {
            return RedirectToAction("blog_view_visitor", "Visitor_blog");
        }

        
        public ActionResult blog_view_visitor(int? Page_No)
        {

         
            List<Blog_visitor> objlist = new List<Blog_visitor>();
            var q = (from r in db.and_blog_tbl
                     join c in db.and_user_tbl on r.fk_email_id equals c.pk_email_id
                     orderby r.blog_date_ descending
                     select new { r.subject, r.pk_blog_id, r.blog_desc, r.blog_image, r.blog_date_, c.fname });

                foreach (var item in q)
                {
                    objlist.Add(new Blog_visitor() { pk_blog_id = item.pk_blog_id, subject = item.subject, blog_image = item.blog_image, blog_date_ = item.blog_date_, blog_desc = item.blog_desc, fname = item.fname });

                }

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            return View(objlist.ToPagedList(No_Of_Page, Size_Of_Page));
         
        }

        public ActionResult addview_load()
        {
            return View("add_blog_visitor");
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
            List<comment_model_visitor> objlist = new List<comment_model_visitor>();
            var co = (from c in db.and_comment_tbl
                      join b2 in db.and_blog_tbl on c.fk_blog_id equals b2.pk_blog_id
                      join u in db.and_user_tbl on c.fk_email_id equals u.pk_email_id
                      where b2.pk_blog_id == id
                      select new { c.pk_comment_id,c.comment, c.cmt_date, u.fname, u.photo }).ToList();

            ViewBag.cmt = co.Count;
            if (co.Count > 0)
            {
                foreach (var item in co)
                {
                    objlist.Add(new comment_model_visitor() { pk_comment_id=item.pk_comment_id ,comment = item.comment, cmt_date = item.cmt_date, fname = item.fname, photo = item.photo });
                }
            }
            else
            {
                ViewBag.comment = "No comments yet";
            }
            ViewBag.comment = objlist;

            return View("View1_blog_visitor", b);
        }

      

    }
}
