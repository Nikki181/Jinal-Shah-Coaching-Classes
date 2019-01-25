using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jinalshah_coaching_class.Areas.Admin.Models;
using jinalshah_coaching_class.Entity_Model;

namespace jinalshah_coaching_class.Areas.Admin.Controllers
{
    public class Admin_faqController : Controller
    {
        //
        // GET: /Admin/Admin_faq/
        Database1Entities db = new Database1Entities();
        faq_admin _model = new faq_admin();
        and_faq_tbl _ft = new and_faq_tbl();
        public ActionResult Index()
        {
            return RedirectToAction("faq_all_admin");
        }

        public ActionResult add_faq_tmp()
        {
            return View("add_faq_admin");
        }
        public ActionResult add_faq_tmp1()
        {
            return View("add_faq_admin1");
        }

        public ActionResult add_faq1(FormCollection frm)
        {
            //return View("add_news_admin");
            var que = frm["txtque"];

            var desc = frm["txt_ans"];

            _ft.que = que;

            _ft.ans = desc;

            db.and_faq_tbl.Add(_ft);
            db.SaveChanges();

            return RedirectToAction("faq_all_admin", "Admin_faq");
        }

        public ActionResult faq_all_admin()
        {

            var q = (from r in db.and_faq_tbl
                     select r).ToList();
            List<faq_admin> lst = new List<faq_admin>();
            foreach (var item in q)
            {
                lst.Add(new faq_admin() { pk_faq_id = item.pk_faq_id, que=item.que,ans=item.ans});
            }
            Session["count"] = lst.Count;

            return View("faq_view_admin", lst);
        }

        public ActionResult faq_delete(int id)
        {
            var data = (from r in db.and_faq_tbl
                        where r.pk_faq_id == id
                        select r).FirstOrDefault();

            db.and_faq_tbl.Remove(data);
            db.SaveChanges();

            return RedirectToAction("faq_all_admin");
        }
        public ActionResult DeleteAll(FormCollection frm)
        {
            var chkvalue = frm.GetValues("assignChkBx");
            foreach (var id in chkvalue)
            {
                faq_admin _fm = new faq_admin();
                int id1 = Convert.ToInt32(id);
                var data = (from c in db.and_faq_tbl where c.pk_faq_id == id1 select c).First();
                db.and_faq_tbl.Remove(data);
                db.SaveChanges();
            }
            return RedirectToAction("faq_all_admin", "Admin_faq");
        }
    }
}
