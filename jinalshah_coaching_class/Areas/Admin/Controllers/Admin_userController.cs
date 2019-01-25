using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jinalshah_coaching_class.Areas.Admin.Models;
using jinalshah_coaching_class.Entity_Model;


namespace jinalshah_coaching_class.Areas.Admin.Controllers
{
    public class Admin_userController : Controller
    {
        //
        // GET: /Admin/Admin_user/
        user_admin _model = new user_admin();
        testimonial_admin _model1 = new testimonial_admin();
        and_testimonials_tbl _tbl = new and_testimonials_tbl();
        Database1Entities db = new Database1Entities();
        and_user_tbl _utbl = new and_user_tbl();

        public ActionResult Index()
        {
            return RedirectToAction("user_all_admin");
        }
        public FileResult Downloadapk()
        {

            var filepath = "http://jinalshah1.brainoorja.com/BooksBart.apk";

            string contentType = "application/apk";
            return File(filepath, contentType,"Download.apk");

        }
        public ActionResult Reach_to_us_admin()
        {
            return View();
        }
        public ActionResult log_out()
        {
            Session.Remove("email-id");
            return RedirectToAction("Index", "Visitor_user", new { area = "" });
        }

        public ActionResult user_all_admin()
        {

            var q = (from r in db.and_user_tbl
                     where r.status == "Approved" || r.status == "Rejected" || r.status=="Pending"
                     select r).ToList();
            List<user_admin> lst = new List<user_admin>();
            foreach (var item in q)
            {
                lst.Add(new user_admin() { pk_email_id=item.pk_email_id,fname=item.fname,lname=item.lname,address=item.address,gender=item.gender,phone_no=item.phone_no,status=item.status,semester=item.semester,photo=item.photo});
            }
            Session["count"] = lst.Count;

            return View("user_view_admin", lst);
        }

      
        public ActionResult Approve_user(string name)
        {
            var data = (from r in db.and_user_tbl
                        where r.pk_email_id == name
                        select r).FirstOrDefault();
            data.status = "Approved";
            user_admin _ua = new user_admin();
            _ua.status = data.status;
            db.SaveChanges();
            return RedirectToAction("user_all_admin");
        }

        public ActionResult Reject_user(string name)
        {
            var data = (from r in db.and_user_tbl
                        where r.pk_email_id == name
                        select r).FirstOrDefault();
            data.status = "Rejected";
            user_admin _ua = new user_admin();
            _ua.status = data.status;
            db.SaveChanges();
            return RedirectToAction("user_all_admin");
        }


        public ActionResult user_delete(string name)
        {
            var data = (from r in db.and_user_tbl
                        where r.pk_email_id == name
                        select r).FirstOrDefault();
            data.status = "Deleted";
            user_admin _ua = new user_admin();
            _ua.status = data.status;
            if (data.photo != null || data.photo != "")
            {
                System.IO.File.Delete(Server.MapPath(data.photo));
            }

            db.and_user_tbl.Remove(data);
            db.SaveChanges();

            return RedirectToAction("user_all_admin");
        }
        public ActionResult DeleteAll(FormCollection frm)
        {
            var chkvalue = frm.GetValues("assignChkBx");
            foreach (var id in chkvalue)
            {
                user_admin n_m = new user_admin();
                string id1 =id;
                var data = (from c in db.and_user_tbl where c.pk_email_id == id1 select c).First();

                if (data.photo != null || data.photo != "")
                {
                    if (data.photo != "~/Blog_image/no_image.gif")
                    {
                        System.IO.File.Delete(Server.MapPath(data.photo));
                    }
                }

                db.and_user_tbl.Remove(data);
                db.SaveChanges();
            }
            return RedirectToAction("user_all_admin", "Admin_user");
        }
        public ActionResult contact_us()
        {
            return View("contact_us_admin");
        }
        public ActionResult about_us()
        {
            return View("about_us_admin");
        }
        public ActionResult why_us()
        {
            return View("why_us_admin");
        }
        public ActionResult our_services()
        {
            return View("our_services_admin");
        }

        public ActionResult follow_us_admin()
        {
            return View();
        }
        public ActionResult Pending_user()
        {

            var q = (from r in db.and_user_tbl
                     where  r.status == "Pending"
                     select r).ToList();
            List<user_admin> lst = new List<user_admin>();
            foreach (var item in q)
            {
                lst.Add(new user_admin() { pk_email_id = item.pk_email_id, fname = item.fname, lname = item.lname, address = item.address, gender = item.gender, phone_no = item.phone_no, status = item.status, semester = item.semester, photo = item.photo });
            }
            Session["count"] = lst.Count;

            return View("user_pending_view_admin", lst);
        }

        public ActionResult Pending_testi()
        {
            var q1 = (from r in db.and_testimonials_tbl
                      where r.status == "Pending"
                      select r).ToList();
            List<testimonial_admin> lst2 = new List<testimonial_admin>();
            foreach (var item in q1)
            {
                lst2.Add(new testimonial_admin() { fk_email_id = item.fk_email_id, desc = item.desc, date = item.date, photo = item.photo, status = item.status });
            }
            Session["count"] = lst2.Count;
            return View("testi_pending_view_admin",lst2);
        }

        public ActionResult Approve_testi(string name1)
        {
            var data = (from r in db.and_testimonials_tbl
                        where r.fk_email_id == name1
                        select r).FirstOrDefault();
            data.status = "Approved";
            testimonial_admin _ta = new testimonial_admin();
            _ta.status = data.status;
            db.SaveChanges();
            return RedirectToAction("testi_all_admin");
        }

        public ActionResult Reject_testi(string name1)
        {
            var data = (from r in db.and_testimonials_tbl
                        where r.fk_email_id == name1
                        select r).FirstOrDefault();
            data.status = "Rejected";
            testimonial_admin _ta = new testimonial_admin();
            _ta.status = data.status;
            db.SaveChanges();
            return RedirectToAction("testi_all_admin");
        }


        public ActionResult testi_delete(string name1)
        {
            var data = (from r in db.and_testimonials_tbl
                        where r.fk_email_id == name1
                        select r).FirstOrDefault();


            if (data.photo != null || data.photo != "")
            {
                System.IO.File.Delete(Server.MapPath(data.photo));
            }

            db.and_testimonials_tbl.Remove(data);
            db.SaveChanges();

            return RedirectToAction("testi_all_admin");
        }

        //public ActionResult testi_pending_admin()
        //{

        //    var q = (from r in db.and_testimonials_tbl
        //             where r.status == "Approved" || r.status == "Pending"
        //             select r).ToList();
        //    List<testimonial_admin> lst1 = new List<testimonial_admin>();
        //    foreach (var item in q)
        //    {
        //        lst1.Add(new testimonial_admin() { fk_email_id = item.fk_email_id, desc = item.desc.Substring(0, 40), date = item.date, photo = item.photo, status = item.status });
        //    }
        //    Session["count"] = lst1.Count;

        //    return View("testi_view_admin", lst1);
        //}

        public ActionResult testi_all_admin()
        {

            var q = (from r in db.and_testimonials_tbl
                     where r.status == "Approved" || r.status == "Pending" || r.status=="Rejected"
                     select r).ToList();
            List<testimonial_admin> lst1 = new List<testimonial_admin>();
            foreach (var item in q)
            {
                lst1.Add(new testimonial_admin() {fk_email_id=item.fk_email_id,desc=item.desc,date=item.date,photo=item.photo,status=item.status });
            }
            Session["count"] = lst1.Count;

            return View("testi_view_admin", lst1);
        }

        //public ActionResult testi_delete(string name)
        //{
        //    var data = (from r in db.and_testimonials_tbl
        //                where r.fk_email_id == name
        //                select r).FirstOrDefault();
        //    data.status = "Deleted";
        //    testimonial_admin _ta = new testimonial_admin();
        //    _ta.status = data.status;
        //    db.and_testimonials_tbl.Remove(data);
        //    db.SaveChanges();

        //    return RedirectToAction("testi_all_admin");
        //}
        public ActionResult DeleteAll_testi(FormCollection frm)
        {
            var chkvalue = frm.GetValues("assignChkBx");
            foreach (var id in chkvalue)
            {
                user_admin n_m = new user_admin();
                string id1 = id;
                var data = (from c in db.and_testimonials_tbl where c.fk_email_id == id1 select c).First();

                if (data.photo != null || data.photo != "")
                {
                    if (data.photo != "~/Blog_image/no_image.gif")
                    {
                        System.IO.File.Delete(Server.MapPath(data.photo));
                    }
                }

                db.and_testimonials_tbl.Remove(data);
                db.SaveChanges();
            }
            return RedirectToAction("testi_all_admin", "Admin_user");
        }
    }
}
