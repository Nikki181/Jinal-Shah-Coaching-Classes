using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jinalshah_coaching_class.Entity_Model;
using jinalshah_coaching_class.Models;
using PagedList;
using PagedList.Mvc;


namespace jinalshah_coaching_class.Controllers
{
    public class Visitor_homeController : Controller
    {
        //
        // GET: /Visitor/Visitor_home/

    
        Database1Entities db = new Database1Entities();
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult home_visitor()
        {
            
       
                var q = from r in db.and_news_tbl
                        select r;
                List<news_visitor> lst = new List<news_visitor>();
                foreach (var data in q)
                {
                    lst.Add(new news_visitor() { pk_news_id = data.pk_news_id, news_title = data.news_title, news_image = data.news_image, news_desc = data.news_desc });
                }
                ViewBag.newslist = lst;


                var q1 = from r1 in db.and_event_tbl
                         orderby r1.event_date_ 
                         select r1;

                List<event_visitor> lst1 = new List<event_visitor>();
                and_event_tbl[] _et = new and_event_tbl[q1.Count()];
                _et = q1.ToArray();
                for (int i = 0; i < 4; i++)
                {
                    lst1.Add(new event_visitor() { event_date = _et[i].event_date_.ToString(), event_name = _et[i].event_name, event_time = _et[i].event_time.ToString(), event_img = _et[i].event_img, event_desc = _et[i].event_desc.Substring(0, 10), event_place = _et[i].event_place });
                }
                ViewBag.eventlist = lst1;

                List<testimonial_visitor> lst2 = new List<testimonial_visitor>();

                var id_name = (from r in db.and_testimonials_tbl
                               join u in db.and_user_tbl on r.fk_email_id equals u.pk_email_id
                               where r.status=="Approved"
                               select new testimonial_visitor() { photo = u.photo, fname = u.fname, date = r.date, desc = r.desc, Id = r.Id });

                foreach (var data3 in id_name)
                {
                    lst2.Add(new testimonial_visitor() { Id = data3.Id, fname = data3.fname, photo = data3.photo, desc = data3.desc,date=data3.date });
                }
                ViewBag.testimoniallist = lst2;


                List<material_visitor> objlist = new List<material_visitor>();

                var q3 = (from r in db.and_material_tbl
                          where r.status == 1
                          select r);
                and_material_tbl[] _mt = new and_material_tbl[q3.Count()];
                _mt = q3.ToArray();

                for (int i = 0; i < 3; i++)
                {
                    DateTime dt = Convert.ToDateTime(_mt[i].material_date);
                    objlist.Add(new material_visitor() { pk_material_id = _mt[i].pk_material_id, material_name = _mt[i].material_name, material_path = _mt[i].material_path, material_date = dt, type_of_material = _mt[i].type_of_material });

                }


                ViewBag.material = objlist;

                List<our_work_visitor> obj = new List<our_work_visitor>();
                var q5 = (from r in db.and_our_work_tbl

                          select r).ToList();

                if (q5.Count > 0)
                {
                    foreach (var item in q5)
                    {
                        obj.Add(new our_work_visitor()
                        {
                            pk_our_work_id = item.pk_our_work_id,
                            our_work_image = item.our_work_image,
                            project_name = item.project_name,
                            project_desc = item.project_desc
                        });

                    }
                }
                else
                {
                    ViewBag.message = "No Projects yet";
                }
                ViewBag.our = obj;
            
            return View();
        }
        public ActionResult faq_view_visitor(int? Page_No)
        {
            var q = from r in db.and_faq_tbl
                    select r;
            List<faq_visitor> lst = new List<faq_visitor>();
            foreach (var data in q)
            {
                lst.Add(new faq_visitor() { pk_faq_id = data.pk_faq_id, que = data.que, ans = data.ans });
            }
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            return View(lst.ToPagedList(No_Of_Page, Size_Of_Page));
       }

        static int newsid;
        public ActionResult Details_news(int id)
        {
            newsid = id;
            var e = from r in db.and_news_tbl
                    where r.pk_news_id == newsid
                    select r;


            and_news_tbl _nt = new and_news_tbl();
            _nt = e.FirstOrDefault();
            news_visitor _n = new news_visitor();
            _n.pk_news_id = newsid;
            _n.news_title = _nt.news_title;
            _n.news_desc = _nt.news_desc;
            _n.news_image = _nt.news_image;
            _n.news_date = _nt.news_date.ToString();

            return View("news_view_visitor", _n);
        }

        public ActionResult news_list_visitor(int? Page_No)
        {
            var q = from r in db.and_news_tbl
                    select r;
            List<news_visitor> lst = new List<news_visitor>();
            foreach (var data in q)
            {
                lst.Add(new news_visitor() { pk_news_id = data.pk_news_id, news_title = data.news_title, news_image = data.news_image, news_desc = data.news_desc.Substring(0, 20) });
            }

            List<news_visitor> lst1 = new List<news_visitor>();
            for (int i = 0; i < 4; i++)
            {
                lst1.Add(new news_visitor() { pk_news_id = lst[i].pk_news_id, news_title = lst[i].news_title, news_image = lst[i].news_image, news_desc = lst[i].news_desc.Substring(0, 20) });
            }
            ViewBag.fournews = lst1;
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            return View(lst.ToPagedList(No_Of_Page, Size_Of_Page));
         
         
        }

        public ActionResult event_list_visitor(int? Page_No)
        {
            var q1 = from r1 in db.and_event_tbl
                     select r1;

            List<event_visitor> lst1 = new List<event_visitor>();

            foreach (var item in q1)
            {
                lst1.Add(new event_visitor() { event_date = item.event_date_.ToString(), event_name = item.event_name, event_time = item.event_time.ToString(), event_img = item.event_img, event_desc = item.event_desc, event_place = item.event_place });
            }

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            return View(lst1.ToPagedList(No_Of_Page, Size_Of_Page));
           
        }

    }
}
