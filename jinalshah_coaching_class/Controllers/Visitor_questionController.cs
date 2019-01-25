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
    public class Visitor_questionController : Controller
    {
        //
        // GET: /Visitor/Visitor_question/

        question_visitor _q = new question_visitor();
        and_question_tbl _qtbl = new and_question_tbl();
        answer_model_visitor _a = new answer_model_visitor();
        and_answer_tbl _atbl = new and_answer_tbl();
        Database1Entities db = new Database1Entities();

        public ActionResult Index()
        {
            return RedirectToAction("que_view_visitor", "Visitor_question");
            
        }
       
        public ActionResult que_view_visitor(int? Page_No)
        {

            List<question_visitor> objlist = new List<question_visitor>();
            var q = (from r in db.and_question_tbl
                     join c in db.and_user_tbl on r.fk_email_id equals c.pk_email_id
                     orderby r.que_date descending
                     select new { r.que_title, r.pk_que_id, r.que_desc, r.que_img, r.que_date, c.fname });

            if (q.Count() > 0)
            {
                foreach (var item in q)
                {
                    DateTime dt = Convert.ToDateTime(item.que_date);
                    objlist.Add(new question_visitor() { pk_que_id = item.pk_que_id, que_title = item.que_title, que_img = item.que_img, que_date = dt, que_desc = item.que_desc, fname = item.fname });

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
                      select new { u.fname }).FirstOrDefault();

            ViewBag.fname = q2.fname;
            _a.que_title = q.que_title;
            _a.que_img = q.que_img;
            _a.que_desc = q.que_desc;
            _a.fk_email_id = q.fk_email_id;
            _a.que_date = Convert.ToDateTime(q.que_date);
            qid = id;




            List<answer_model_visitor> objlist = new List<answer_model_visitor>();
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
                    objlist.Add(new answer_model_visitor() { ans_img = item.ans_img, answer = item.answer, a_date = Convert.ToDateTime(item.a_date) });

                }

            }
            else
            {
                ViewBag.answer = "No answer yet";
            }
            ViewBag.answer = objlist;
            ViewBag.count = objlist.Count();
            return View("ans_view_visitor", _a);
        }
        

    }
}
