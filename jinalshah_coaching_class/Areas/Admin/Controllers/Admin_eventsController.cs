
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jinalshah_coaching_class.Areas.Admin.Models;
using jinalshah_coaching_class.Entity_Model;

namespace jinalshah_coaching_class.Areas.Admin.Controllers
{
    public class Admin_eventsController : Controller
    {
        //
        // GET: /Admin/Admin_events/
        Database1Entities db = new Database1Entities();
        and_event_tbl _et = new and_event_tbl();
        events_admin e_model = new events_admin();
        public ActionResult Index()
        {
            return View();
            //return RedirectToAction("events_all_admin");
        }

        public ActionResult add_events_tmp()
        {
            return View("add_events_admin");
        }
        public ActionResult add_events_tmp1()
        {
            return View("add_events_admin1");
        }

        public ActionResult add_event1(FormCollection frm)
        {
            var name = frm["event_name"];

            var desc = frm["event_desc"];
            var spk = frm["event_speaker"];
            var topic1 = frm["event_topic"];
            var place = frm["event_place"];
            DateTime dt = Convert.ToDateTime(frm["event_date"]);

            DateTime tm1 = Convert.ToDateTime(frm["event_time"]);
            TimeSpan ts = tm1.TimeOfDay;
            HttpPostedFileBase hp = Request.Files["fileuploadDemo"];
            if (hp != null && hp.FileName != "")
            {
                string img = hp.FileName;
                string path = "~/Images/" + img;
                hp.SaveAs(Server.MapPath(path));
                _et.event_img = path;
            }
            else
            {
                _et.event_img = "~/Blog_image/no_image.gif";
            }
           

            _et.event_name = name;

            _et.event_desc = desc;
            _et.speaker = spk;
            _et.topic = topic1;
            _et.event_place = place;

            _et.event_date_ = dt;
           _et.event_time = ts;

            db.and_event_tbl.Add(_et);
            db.SaveChanges();

            return RedirectToAction("events_all_admin", "Admin_events");
        }

        public ActionResult events_all_admin()
        {
            var q = (from r in db.and_event_tbl
                     orderby r.event_date_
                     select r).ToList();
            List<events_admin> lst = new List<events_admin>();
            foreach (var item in q)
            {
                lst.Add(new events_admin() { pk_event_id = item.pk_event_id, event_name = item.event_name, event_desc = item.event_desc, event_img = item.event_img, speaker = item.speaker, topic = item.topic, event_date=item.event_date_.ToShortDateString(), event_time = item.event_time.ToString(), event_place = item.event_place });
            }
            Session["count"] = lst.Count;

            return View("events_view_admin", lst);
        }

        public ActionResult event_edit(int id)
        {

            var data = (from r in db.and_event_tbl
                        where r.pk_event_id == id
                        select r).FirstOrDefault();
            events_admin e_model = new events_admin();
            e_model.event_name = data.event_name;

            e_model.event_desc = data.event_desc;
            e_model.speaker = data.speaker;
            e_model.topic = data.topic;
            e_model.event_img = data.event_img;
            e_model.event_place = data.event_place;

            e_model.event_date = (data.event_date_).ToString();
            e_model.event_time = (data.event_time).ToString();

            return View("event_edit_admin", e_model);
        }

        [HttpPost]
        public ActionResult event_edit(int id, FormCollection collection)
        {

            var data = (from r in db.and_event_tbl
                        where r.pk_event_id == id
                        select r).FirstOrDefault();


            data.event_name = collection["event_name"];
            HttpPostedFileBase hp = Request.Files["fileuploadDemo"];
            if (hp != null && hp.FileName != "")
            {
                string path = "~/Images/" + hp.FileName;
                if (data.event_img != path)
                {
                    if (data.event_img == "~/Blog_image/no_image.gif")
                    {

                        hp.SaveAs(Server.MapPath(path));
                    }
                    else
                    {
                        System.IO.File.Delete(Server.MapPath(data.event_img));
                        hp.SaveAs(Server.MapPath(path));
                    }
                }
                data.event_img = path;
            }
            data.event_desc = collection["event_desc"];
            data.speaker = collection["speaker"];
            data.topic = collection["topic"];
            data.event_date_ = Convert.ToDateTime(collection["event_date"]);
            DateTime dt = Convert.ToDateTime(collection["event_time"]);
            TimeSpan ts = dt.TimeOfDay;
            data.event_time = ts;
            data.event_place = collection["event_place"];
            events_admin _et = new events_admin();

            _et.event_name = data.event_name;

            _et.event_desc = data.event_desc;
            _et.speaker = data.speaker;
            _et.topic = data.topic;
            _et.event_img = data.event_img;
            _et.event_place = data.event_place;

            _et.event_date = data.event_date_.ToString();
            _et.event_time = data.event_time.ToString();

            db.SaveChanges();
            return RedirectToAction("events_all_admin");
        }

        public ActionResult event_delete(int id)
        {
            var data = (from r in db.and_event_tbl
                        where r.pk_event_id == id
                        select r).FirstOrDefault();

            if (data.event_img != null || data.event_img != "")
            {
                if (data.event_img != "~/Blog_image/no_image.gif")
                {
                    System.IO.File.Delete(Server.MapPath(data.event_img));
                }
            }
            
            db.and_event_tbl.Remove(data);
            db.SaveChanges();

            return RedirectToAction("events_all_admin");
        }
        public ActionResult DeleteAll(FormCollection frm)
        {
            var chkvalue = frm.GetValues("assignChkBx");
            foreach (var id in chkvalue)
            {
                news_admin n_m = new news_admin();
                int id1 = Convert.ToInt32(id);
                var data = (from c in db.and_event_tbl where c.pk_event_id == id1 select c).First();
                if (data.event_img != null || data.event_img != "")
                {
                    if (data.event_img != "~/Blog_image/no_image.gif")
                    {
                        System.IO.File.Delete(Server.MapPath(data.event_img));
                    }
                }
          
                db.and_event_tbl.Remove(data);
                db.SaveChanges();
            }
            return RedirectToAction("events_all_admin", "Admin_events");
        }




    }
}
