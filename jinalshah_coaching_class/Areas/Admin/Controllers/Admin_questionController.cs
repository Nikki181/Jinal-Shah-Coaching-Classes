using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jinalshah_coaching_class.Areas.Admin.Models;
using jinalshah_coaching_class.Entity_Model;

namespace jinalshah_coaching_class.Areas.Admin.Controllers
{
    public class Admin_questionController : Controller
    {
        //
        // GET: /Admin/Admin_question/

        question_admin _q = new question_admin();
        and_question_tbl _qtbl = new and_question_tbl();
        and_answer_tbl ans_tbl = new and_answer_tbl();
        Database1Entities db = new Database1Entities();

        public ActionResult Index()
        {
            var q2 = (from r in db.and_answer_tbl
                      join p in db.and_question_tbl on r.fk_que_id equals p.pk_que_id
                      join u in db.and_user_tbl on r.fk_email_id equals u.pk_email_id
                      select new {p.que_desc, p.pk_que_id, u.fname, r.pk_ans_id, p.que_title, r.answer, r.ans_img, r.fk_email_id, r.a_date }).ToList();
            List<question_admin> lst = new List<question_admin>();
            foreach (var item in q2)
            {
                lst.Add(new question_admin() { que_desc=item.que_desc,pk_que_id = item.pk_que_id, fname = item.fname, ans_img = item.ans_img, pk_ans_id = item.pk_ans_id, que_title = item.que_title, answer = item.answer, fk_email_id = item.fk_email_id, a_date = Convert.ToDateTime(item.a_date) });

            }
            Session["count"] = lst.Count;
            return View("question_view", lst);
        }
        public ActionResult que_view()
        {

            return RedirectToAction("view_all", "Admin_question");
        }
        public ActionResult addque_load()
        {
            return View("add_que_admin");
        }


        public ActionResult add_que(FormCollection frm)
        {
            var sub = frm["txtsub"];

            var desc = frm["que_desc"];

            HttpPostedFileBase hp = Request.Files["fileuploadDemo"];
            if (hp != null && hp.FileName != "")
            {
                string img = hp.FileName;
                string path = "~/que_image/" + img;
                hp.SaveAs(Server.MapPath(path));
                _qtbl.que_img = path;
            }
            else
            {
                _qtbl.que_img = "~/Blog_image/no_image.gif";
            }
           

            _qtbl.que_title = sub;

            _qtbl.que_desc = desc;

            var eid = "";
            eid = Convert.ToString(Session["email-id"]);
            _qtbl.fk_email_id = eid;

            var dt = System.DateTime.Now;
            _qtbl.que_date = dt;
            _qtbl.fk_grp_id = 1;
            if (eid != "")
            {
                db.and_question_tbl.Add(_qtbl);
                db.SaveChanges();

            }
            else
            {
                ViewBag.message = "Please Login to Add Question";
                return RedirectToAction("Index", "User");

            }


            return RedirectToAction("view_all", "Admin_question");
        }

        public ActionResult DeleteAll_que_ans(FormCollection frm)
        {
            var chkvalue = frm.GetValues("assignChkBx");
            foreach (var id in chkvalue)
            {
                question_admin _fm = new question_admin();
                int id1 = Convert.ToInt32(id);
                var data = (from c in db.and_question_tbl where c.pk_que_id == id1 select c).FirstOrDefault();
                if (data.que_img != null || data.que_img != "")
                {
                    if (data.que_img != "~/Blog_image/no_image.gif")
                    {
                        System.IO.File.Delete(Server.MapPath(data.que_img));
                    }
                }
          
                db.and_question_tbl.Remove(data);              
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Admin_question");
        }

        public ActionResult view_all()
        {

            List<question_admin> objlist = new List<question_admin>();
            var q = (from r in db.and_question_tbl
                     join c in db.and_user_tbl on r.fk_email_id equals c.pk_email_id
                     orderby r.que_date descending
                     select new { r.que_title, r.pk_que_id, r.que_desc, r.que_img, r.que_date,c.fname}).ToList();

            if (q.Count() > 0)
            {
                foreach (var item in q)
                {
                    DateTime dt = Convert.ToDateTime(item.que_date);
                    objlist.Add(new question_admin() { pk_que_id = item.pk_que_id, que_title = item.que_title, que_img = item.que_img, que_date = dt, que_desc = item.que_desc,fname=item.fname });

                }
            }
            else
            {
                ViewBag.message = "No Questions yet";
            }
           // ViewBag.que = objlist;
            return View("view_que_admin", objlist);
        }

        public ActionResult Delete(int id)
        {

            try
            {
                var data = (from r in db.and_answer_tbl
                            where r.fk_que_id == id
                            select r);

                if (data.ToList().Count > 0)
                {
                    foreach (var item in data)
                    {
                        if (item.ans_img != null || item.ans_img != "")
                        {
                            if (item.ans_img != "~/Blog_image/no_image.gif")
                            {
                                System.IO.File.Delete(Server.MapPath(item.ans_img));
                            }
                        }
          
                        db.and_answer_tbl.Remove(item);
                    }
                }
           
                var q = (from r in db.and_question_tbl
                         where r.pk_que_id == id
                         select r).FirstOrDefault();
                
                if (q.que_img != null || q.que_img != "")
                {
                    if (q.que_img != "~/Blog_image/no_image.gif")
                    {
                        System.IO.File.Delete(Server.MapPath(q.que_img));
                    }
                }
       
                db.and_question_tbl.Remove(q);
                db.SaveChanges();
                return RedirectToAction("view_all", "Admin_question");

            }
            catch
            {
                return View("Error");

            }
        }

      
        //public ActionResult Answer(int id)
        //{
        //    return view("");

        //}

        public ActionResult ans_Delete(int id)
        {
            try
            {
                var q = (from r in db.and_answer_tbl
                         where r.pk_ans_id == id
                         select r).FirstOrDefault();

                if (q.ans_img != null || q.ans_img != "")
                {
                    if (q.ans_img != "~/Blog_image/no_image.gif")
                    {
                        System.IO.File.Delete(Server.MapPath(q.ans_img));
                    }
                }

                db.and_answer_tbl.Remove(q);

                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View("Error");

            }
        }

        public static int qid;
        public ActionResult ans_Detail(int id)
        {


            var q = (from b1 in db.and_question_tbl
                     where b1.pk_que_id == id
                     select new { b1.que_title, b1.que_desc, b1.que_img, b1.fk_email_id, b1.que_date }).FirstOrDefault();

            var q2 = (from b1 in db.and_question_tbl
                      join u in db.and_user_tbl on b1.fk_email_id equals u.pk_email_id
                      where b1.pk_que_id == id
                      select new { u.fname }).FirstOrDefault();

            ViewBag.fname = q2.fname;
            _q.que_title = q.que_title;
            _q.que_img = q.que_img;
            _q.que_desc = q.que_desc;
            _q.fk_email_id = q.fk_email_id;
            _q.que_date = Convert.ToDateTime(q.que_date);
            qid = id;




            List<question_admin> objlist = new List<question_admin>();
            var co = (from a in db.and_answer_tbl
                      join q3 in db.and_question_tbl on a.fk_que_id equals q3.pk_que_id
                      where a.fk_que_id == id
                      select new { a.ans_img, a.answer, a.a_date, a.fk_email_id }).ToList();
            if (co.Count > 0)
            {
                var user_data = (from b1 in db.and_answer_tbl
                                 join u in db.and_user_tbl on b1.fk_email_id equals u.pk_email_id
                                 where b1.fk_que_id == id
                                 select new { u.fname, u.photo }).FirstOrDefault();
                ViewBag.fname = user_data.fname;
                ViewBag.photo = user_data.photo;


                foreach (var item in co)
                {
                    objlist.Add(new question_admin() { ans_img = item.ans_img, answer = item.answer, a_date = Convert.ToDateTime(item.a_date) });

                }

            }
            else
            {
                ViewBag.answer = "No answer yet";
            }
            ViewBag.answer = objlist;
            ViewBag.count = objlist.Count();
            return View("admin_ans_view", _q);
        }
        public ActionResult add_answer(FormCollection frm)
        {

            var desc = frm["answer"];
            HttpPostedFileBase hp = Request.Files["fileuploadDemo"];
            if (hp != null && hp.FileName != "")
            {
                string img = hp.FileName;
                string path = "~/ans_image/" + img;
                hp.SaveAs(Server.MapPath(path));
                ans_tbl.ans_img = path;
            }
            else
            {
                ans_tbl.ans_img = "~/Blog_image/no_image.gif";
            }
            var eid = Session["email-id"];
            ans_tbl.fk_que_id = qid;
            ans_tbl.answer = desc;

            ans_tbl.fk_email_id = Convert.ToString(eid);
            ans_tbl.a_date = Convert.ToDateTime(System.DateTime.Now);
            db.and_answer_tbl.Add(ans_tbl);
            db.SaveChanges();
            return RedirectToAction("ans_detail", "Admin_question", new { id = qid });
        }


    }
}
