using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jinalshah_coaching_class.Entity_Model;
using jinalshah_coaching_class.Areas.Admin.Models;
namespace jinalshah_coaching_class.Areas.Admin.Controllers
{
    public class Admin_our_workController : Controller
    {
        //
        // GET: /Admin/Admin_our_work/
        Database1Entities _entity = new Database1Entities();
        our_work_admin _o = new our_work_admin();
        and_our_work_tbl _otbl = new and_our_work_tbl();

        public ActionResult addload()
        {
            return View("add_our_work");
        }
        public ActionResult insert_work(FormCollection frm)
        {
            var pnm = frm["txtnm"];
            var desc = frm["txtdesc"];
            var size = frm["size"];
            var name = frm["txtname"];
            var day = frm["txtd"];
            var type = frm["txttype"];
            var platform = frm["txtp"];
           var link = frm["txtlink"];

            HttpPostedFileBase hp = Request.Files["fileuploadDemo"];
            if (hp != null && hp.FileName != "")
            {
                string img = hp.FileName;
                string path = "~/Our_work_img/" + img;
                hp.SaveAs(Server.MapPath(path));
                _otbl.our_work_image = path;
            }
            else
            {
                _otbl.our_work_image = "~/Blog_image/no_image.gif";
            }
           

            _otbl.project_name = pnm;
            _otbl.project_desc = desc;
            var s = Convert.ToInt16(size);
            _otbl.team_size = s;
            _otbl.members_name = name;
            
            _otbl.days_to_complete = day;
            _otbl.type = type;
            _otbl.platform = platform;
            _otbl.link = link;
                _entity.and_our_work_tbl.Add(_otbl);
                _entity.SaveChanges();
           

            return RedirectToAction("Index","Admin_our_work");
        }
        public ActionResult Index()
        {
            List<our_work_admin> objlist = new List<our_work_admin>();
            var q = (from r in _entity.and_our_work_tbl
                     
                     select r).ToList();

            if (q.Count > 0)
            {
                foreach (var item in q)
                {
                    objlist.Add(new our_work_admin() { pk_our_work_id = item.pk_our_work_id, our_work_image = item.our_work_image, 
                        project_name = item.project_name, project_desc = item.project_desc,type=item.type });

                }
            }
            else
            {
                ViewBag.message = "No Projects yet";
            }

            return View("view_work_admin", objlist);
        }


        public ActionResult load_detail(int id)
        {
            List<our_work_admin> objlist = new List<our_work_admin>();
            var q = (from r in _entity.and_our_work_tbl
                     where r.pk_our_work_id == id
                     select r).ToList();

            if (q.Count > 0)
            {
                foreach (var item in q)
                {
                    int t = Convert.ToInt16(item.team_size);
                  //  int d = Convert.ToInt16(item.days_to_complete);

                    objlist.Add(new our_work_admin()
                    {
                        pk_our_work_id = item.pk_our_work_id,
                        our_work_image = item.our_work_image,
                        project_name = item.project_name,
                        project_desc = item.project_desc,
                        team_size = t,
                        members_name = item.members_name,
                        days_to_complete =item.days_to_complete,
                        type = item.type,
                        platform = item.platform,
                        link = item.link
                    });

                }
            }
            else
            {
                ViewBag.message = "No Projects yet";
            }

            return View("detail_work_admin", objlist);
        }

        public ActionResult Delete(int id)
        {
            var data = (from r in _entity.and_our_work_tbl
                        where r.pk_our_work_id == id
                        select r).FirstOrDefault();

            if (data.our_work_image != null || data.our_work_image != "")
            {
                if (data.our_work_image != "~/Blog_image/no_image.gif")
                {
                    System.IO.File.Delete(Server.MapPath(data.our_work_image));
                }
            }

            _entity.and_our_work_tbl.Remove(data);
            _entity.SaveChanges();

            return RedirectToAction("Index");
        }
      

    }
}
