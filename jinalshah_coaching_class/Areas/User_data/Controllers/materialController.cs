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
    public class materialController : Controller
    {
        //
        // GET: /material/

        material _m = new material();
        and_material_tbl _mtbl = new and_material_tbl();
        Database1Entities db = new Database1Entities();

        public ActionResult Index()
        {
            return RedirectToAction("view_material", "material");
        }

        public ActionResult add_mat()
        {
            return View("lab_work_upload");
        }
        public ActionResult all_subjects()
        {
            return View();
        }
      
        public static string email;
        public ActionResult insert_material(FormCollection frm)
        {
            var sub = frm["txtsub"];
         

            HttpPostedFileBase hp = Request.Files["fileuploadDemo"];
            if (hp != null && hp.FileName != "")
            {
                string img = hp.FileName;
                string path = "~/Material_img/" + img;
                hp.SaveAs(Server.MapPath(path));
                _mtbl.material_path = path;
            }

            if (sub.ToString() == null || sub.ToString() == "")
            {
                ViewBag.msg = "Please Add subject";
                return View("lab_work_upload");
            }
            else
            {
                
                var eid = Convert.ToString(Session["email-id"]);
                email = eid;
                _mtbl.fk_email_id = eid;
                _mtbl.status = 0;
                _mtbl.material_name = sub;
                var dt = System.DateTime.Now;
                _mtbl.material_date = dt;
                _mtbl.type_of_material = null;
                db.and_material_tbl.Add(_mtbl);
                db.SaveChanges();
                return RedirectToAction("lab_work_view", "material");
            }

             
        }

        public ActionResult view_material(int? Page_No,string name)   //all material
        {
            List<material> objlist = new List<material>();
            var q = (from r in db.and_material_tbl
                     where r.status==1 && r.type_of_material==name
                     orderby r.material_date
                     select new {r.pk_material_id, r.material_path, r.material_date, r.material_name, r.type_of_material });



            if (q.Count() > 0)
            {
                foreach (var item in q)
                {
                    DateTime dt = Convert.ToDateTime(item.material_date);
                    objlist.Add(new material() { pk_material_id = item.pk_material_id, material_name = item.material_name, material_path = item.material_path, material_date = dt, type_of_material = item.type_of_material});

                }
            }
            else
            {
                ViewBag.message = "No Materials yet";
            }
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            return View(objlist.ToPagedList(No_Of_Page, Size_Of_Page));
         
        }

        public ActionResult lab_work_view(int? Page_No)   //user
        {
            var eid = Convert.ToString(Session["email-id"]);
            List<material> objlist = new List<material>();
            var q = (from r in db.and_material_tbl
                     where r.fk_email_id==eid && r.status==0
                     orderby r.material_date
                     select new { r.pk_material_id, r.material_path, r.material_date, r.material_name, r.type_of_material });



            if (q.Count() > 0)
            {
                foreach (var item in q)
                {
                    DateTime dt = Convert.ToDateTime(item.material_date);
                    objlist.Add(new material() { pk_material_id = item.pk_material_id, material_name = item.material_name, material_path = item.material_path, material_date = dt, type_of_material = item.type_of_material });

                }
            }
            else
            {
                ViewBag.message = "No Materials yet";
            }

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            return View(objlist.ToPagedList(No_Of_Page, Size_Of_Page));


        }
  
        public FileResult DownloadData(int id)
        {
            int fid = Convert.ToInt32(id);

            var filename = (from f in db.and_material_tbl
                            where f.pk_material_id == fid
                            select f.material_path).FirstOrDefault();
            var filepath = filename;

            if (filepath.Contains(".jpg"))
            {
                string contentType = "application/jpg";
                return File(filepath, contentType, "Download.jpg");
            }
            else if (filepath.Contains(".pdf"))
            {
                string contentType = "application/pdf";
                return File(filepath, contentType, "Download.pdf");
            }

            else if (filepath.Contains(".png"))
            {
                string contentType = "application/pdf";
                return File(filepath, contentType, "Download.png");
            }

            else
            {
                string contentType = "application/plain";
                return File(filepath, contentType, "Download.txt");
            }
        }

        public ActionResult delete(int id)
        {

            try
            {
                
                var q = (from r in db.and_material_tbl
                         where r.pk_material_id == id
                         select r).FirstOrDefault();

                if (q.material_path != null || q.material_path != "")
                {
                    System.IO.File.Delete(Server.MapPath(q.material_path));
                }

                db.and_material_tbl.Remove(q);
                db.SaveChanges();
                return RedirectToAction("lab_work_view");

            }
            catch
            {
                return View("Error");

            }
        }



    }
}
