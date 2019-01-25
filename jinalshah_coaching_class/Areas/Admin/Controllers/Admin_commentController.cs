using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jinalshah_coaching_class.Areas.Admin.Models;
using jinalshah_coaching_class.Entity_Model;

namespace jinalshah_coaching_class.Areas.Admin.Controllers
{
    public class Admin_commentController : Controller
    {
        //
        // GET: /Admin/Admin_comment/

        comment_admin c_model = new comment_admin();
        and_comment_tbl ctbl = new and_comment_tbl();
        Database1Entities db = new Database1Entities();
        public ActionResult Index()
        {
            var q = (from r in db.and_comment_tbl
                     join b in db.and_blog_tbl on r.fk_blog_id equals b.pk_blog_id
                     join u in db.and_user_tbl on r.fk_email_id equals u.pk_email_id
                     select new {u.fname,b.pk_blog_id,b.subject,r.pk_comment_id ,r.fk_blog_id,r.fk_email_id,r.comment,r.cmt_date}).ToList();
            List<comment_admin> lst = new List<comment_admin>();
            foreach (var item in q)
            {
                lst.Add(new comment_admin() {fname=item.fname, pk_blog_id=item.pk_blog_id, subject=item.subject,pk_comment_id=item.pk_comment_id,fk_email_id=item.fk_email_id,comment=item.comment,cmt_date=item.cmt_date });
            }
            Session["count"] = lst.Count;
            return View("comment_view",lst);

        }
       //
        // GET: /cat/Details/5

        //public ActionResult DeleteAll(FormCollection frm)
        //{
        //    var chkvalue = frm.GetValues("assignChkBx");
        //    foreach (var id in chkvalue)
        //    {
        //        comment_admin c_m = new comment_admin();
        //        int id1 = Convert.ToInt32(id);
        //        var data = (from c in db.and_comment_tbl where c.pk_comment_id == id1 select c).First();
        //        db.and_comment_tbl.Remove(data);
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Index", "Admin_comment", new { Area = "Admin" });
        //}
        public static int bid;
        public ActionResult blog_detail(int id)
        {
            var q = (from b1 in db.and_blog_tbl
                     join u in db.and_user_tbl on b1.fk_email_id equals u.pk_email_id
                     where b1.pk_blog_id==id
                     select u).FirstOrDefault();
            ViewBag.fname= q.fname;

            bid = id;
            var q2 = (from c in db.and_blog_tbl
                     where c.pk_blog_id==id
                     select c ).FirstOrDefault();
            c_model.blog_desc = q2.blog_desc;
            c_model.blog_date_ = System.DateTime.Now;
            c_model.blog_image = q2.blog_image;
            c_model.fk_email_id = Convert.ToString(Session["email-id"]);
            
            c_model.subject = q2.subject;
            List<comment_admin> objlist = new List<comment_admin>();
            var co = (from c in db.and_comment_tbl
                      join b2 in db.and_blog_tbl on c.fk_blog_id equals b2.pk_blog_id
                      join u in db.and_user_tbl on c.fk_email_id equals u.pk_email_id
                      where b2.pk_blog_id == id
                      select new { c.comment, c.cmt_date, u.fname, u.photo }).ToList();

            if (co.Count > 0)
            {
                foreach (var item in co)
                {
                    objlist.Add(new comment_admin() { comment = item.comment, cmt_date = item.cmt_date, fname = item.fname, photo = item.photo });
                }
            }
            else
            {
                ViewBag.comment = "No comments yet";
            }
            ViewBag.comment = objlist;
            return View("all_comment", c_model);
        }
        [ValidateInput(false)]
        public ActionResult add_comment(FormCollection frm)
        {
            var sub = frm["name"];
            var desc = frm["comment"];


            ctbl.fk_blog_id = bid;

            ctbl.fk_email_id = Convert.ToString(Session["email-id"]);
            ctbl.comment = desc;
            ctbl.cmt_date = System.DateTime.Now;
            db.and_comment_tbl.Add(ctbl);
            db.SaveChanges();
            return RedirectToAction("blog_detail", new { id = bid });
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
                return RedirectToAction("Index");

            }
            catch
            {
                return View("Error");

            }
        }

    }
}
