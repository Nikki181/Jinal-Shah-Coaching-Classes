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
    public class QuestionController : Controller
    {
        //
        // GET: /Question/
        question _q = new question();
        and_question_tbl _qtbl = new and_question_tbl();
        answer_model _a = new answer_model();
        and_answer_tbl _atbl = new and_answer_tbl();
        Database1Entities db = new Database1Entities();

        public ActionResult Index()
        {
            return RedirectToAction("que_view", "Question");
            //return View("que_add");
        }
        public ActionResult addque_load()
        {
            return View("que_add");
        }

        [ValidateInput(false)]
        public ActionResult insert_que(FormCollection frm)
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
            if (desc.ToString() == null || desc.ToString() == "" || sub.ToString() == null || sub.ToString() == "")
            {
                ViewBag.msg = "Please add Question";
                ViewBag.subject = "Please add Subject";
                return View("que_add");
            }
            else
            {
                _qtbl.que_title = sub;

                _qtbl.que_desc = desc;

                var eid = "";
                eid = Convert.ToString(Session["email-id"]);
                _qtbl.fk_email_id = eid;

                var dt = System.DateTime.Now;
                _qtbl.que_date = dt;
                _qtbl.fk_grp_id = 1;
                db.and_question_tbl.Add(_qtbl);
                db.SaveChanges();
                return RedirectToAction("que_view", "Question");
            }
           
        }

        public ActionResult que_view(int? Page_No)
        {

            List<question> objlist = new List<question>();
            var q = (from r in db.and_question_tbl
                     join c in db.and_user_tbl on r.fk_email_id equals c.pk_email_id
                     orderby r.que_date descending
                     select new {r.fk_email_id,r.que_title, r.pk_que_id, r.que_desc, r.que_img, r.que_date, c.fname });

            if (q.Count() > 0)
            {
                foreach (var item in q)
                {
                    DateTime dt = Convert.ToDateTime(item.que_date);
                    objlist.Add(new question() {fk_email_id=item.fk_email_id, pk_que_id = item.pk_que_id, que_title = item.que_title, que_img = item.que_img, que_date = dt, que_desc = item.que_desc, fname = item.fname });

                }
            }
            else
            {
                ViewBag.message = "No Questions yet";
            }
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            return View(objlist.ToPagedList(No_Of_Page, Size_Of_Page));


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
                      select new { u.fname ,b1.fk_email_id}).FirstOrDefault();

            ViewBag.fname = q2.fname;
            _a.que_title = q.que_title;
            _a.que_img = q.que_img;
            _a.que_desc = q.que_desc;
            _a.fk_email_id = q.fk_email_id;
            _a.que_date = Convert.ToDateTime(q.que_date);
            qid = id;


            List<answer_model> objlist = new List<answer_model>();
            var co = (from a in db.and_answer_tbl
                      join q3 in db.and_question_tbl on a.fk_que_id equals q3.pk_que_id
                      where a.fk_que_id == id
                      select new { a.pk_ans_id, a.ans_img, a.answer, a.a_date, a.fk_email_id }).ToList();
            if (co.Count > 0)
            {
                var user_data = (from b1 in db.and_answer_tbl
                                 join u in db.and_user_tbl on b1.fk_email_id equals u.pk_email_id
                                 where b1.fk_que_id == id
                                 select new { u.fname, u.photo,b1.fk_email_id }).FirstOrDefault();
                ViewBag.fname = user_data.fname;
                ViewBag.photo = user_data.photo;


                foreach (var item in co)
                {
                    objlist.Add(new answer_model() {pk_ans_id=item.pk_ans_id, fk_emai_id=item.fk_email_id,ans_img = item.ans_img, answer = item.answer, a_date = Convert.ToDateTime(item.a_date) });

                }

            }
            else
            {
                ViewBag.answer = "No answer yet";
            }
            ViewBag.answer = objlist;
            ViewBag.count = objlist.Count();
            return View("ans_View", _a);
        }

        [ValidateInput(false)]
        public ActionResult add_answer(FormCollection frm)
        {

            var desc = frm["answer"];
            HttpPostedFileBase hp = Request.Files["fileuploadDemo"];
            if (hp != null && hp.FileName != "")
            {
                string img = hp.FileName;
                string path = "~/ans_image/" + img;
                hp.SaveAs(Server.MapPath(path));
                _atbl.ans_img = path;
            }
            else
            {
                _atbl.ans_img = "~/Blog_image/no_image.gif";
            }
            if (desc.ToString() == null || desc.ToString() == "" )
            {
                ViewBag.msg = "Please add Answer";
                var q = (from b1 in db.and_question_tbl
                         where b1.pk_que_id == qid
                         select new { b1.que_title, b1.que_desc, b1.que_img, b1.fk_email_id, b1.que_date }).FirstOrDefault();

                var q2 = (from b1 in db.and_question_tbl
                          join u in db.and_user_tbl on b1.fk_email_id equals u.pk_email_id
                          where b1.pk_que_id == qid
                          select new { u.fname, b1.fk_email_id }).FirstOrDefault();

                ViewBag.fname = q2.fname;
                _a.que_title = q.que_title;
                _a.que_img = q.que_img;
                _a.que_desc = q.que_desc;
                _a.fk_email_id = q.fk_email_id;
                _a.que_date = Convert.ToDateTime(q.que_date);
                

                List<answer_model> objlist = new List<answer_model>();
                var co = (from a in db.and_answer_tbl
                          join q3 in db.and_question_tbl on a.fk_que_id equals q3.pk_que_id
                          where a.fk_que_id == qid
                          select new { a.pk_ans_id, a.ans_img, a.answer, a.a_date, a.fk_email_id }).ToList();
                if (co.Count > 0)
                {
                    var user_data = (from b1 in db.and_answer_tbl
                                     join u in db.and_user_tbl on b1.fk_email_id equals u.pk_email_id
                                     where b1.fk_que_id == qid
                                     select new { u.fname, u.photo, b1.fk_email_id }).FirstOrDefault();
                    ViewBag.fname = user_data.fname;
                    ViewBag.photo = user_data.photo;


                    foreach (var item in co)
                    {
                        objlist.Add(new answer_model() { pk_ans_id = item.pk_ans_id, fk_emai_id = item.fk_email_id, ans_img = item.ans_img, answer = item.answer, a_date = Convert.ToDateTime(item.a_date) });

                    }

                }
                else
                {
                    ViewBag.answer = "No answer yet";
                }
                ViewBag.answer = objlist;
                ViewBag.count = objlist.Count();
                return View("ans_View", _a);
               
            }
            else
            {

                var eid = Session["email-id"];
                _atbl.fk_que_id = qid;
                _atbl.answer = desc;

                _atbl.fk_email_id = Convert.ToString(eid);
                _atbl.a_date = Convert.ToDateTime(System.DateTime.Now);
                db.and_answer_tbl.Add(_atbl);
                db.SaveChanges();
                return RedirectToAction("ans_detail", new { id = qid });
            }
           
        }

        public ActionResult Delete_ans(int id)
        {

            var data = (from r in db.and_answer_tbl
                        where r.pk_ans_id == id
                        select r);

            if (data.ToList().Count > 0)
            {
                foreach (var item in data)
                {
                    if (item.ans_img != "~/Blog_image/no_image.gif")
                    {
                        System.IO.File.Delete(Server.MapPath(item.ans_img));
                    }
                    db.and_answer_tbl.Remove(item);
                }
            }
           
            db.SaveChanges();
            return RedirectToAction("ans_detail", new { id = qid });

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
                        if (item.ans_img != "~/Blog_image/no_image.gif")
                        {
                            System.IO.File.Delete(Server.MapPath(item.ans_img));
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
                return RedirectToAction("que_view");

            }
            catch
            {
                return View("Error");

            }
        }

    }

}