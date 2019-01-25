using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jinalshah_coaching_class.Areas.Admin.Models;
using jinalshah_coaching_class.Entity_Model;

namespace jinalshah_coaching_class.Areas.Admin.Controllers
{
    public class Admin_materialController : Controller
    {
        //
        // GET: /Admin/Admin_material/
        material_admin _m = new material_admin();
        and_material_tbl _mtbl = new and_material_tbl();
        Database1Entities db = new Database1Entities();


        public ActionResult Index()
        {
            return RedirectToAction("view", "Admin_material");
        }

        public ActionResult add_view()
        {
            return View("add_material_admin");
        }

        public ActionResult add_material(FormCollection frm)
        {
            var sub = frm["txtsub"];
            var type = frm["type_mat"];
            string x = "";
            if (type == "CG")
            {
                x = "~/material/cg/";
            }
            else if (type == "SS")
            {
                x = "~/material/ss/";
            }
            else if (type == "DFS")
            {
                x = "~/material/dfs/";
            }

            else if (type == "DC")
            {
                x = "~/material/dc/";
            }

            else if (type == "JAVA")
            {
                x = "~/material/java/";
            }
   
            HttpPostedFileBase hp = Request.Files["fileuploadDemo"];
            if (hp != null && hp.FileName != "")
            {
                string img = hp.FileName;
                string path = x + img;
                hp.SaveAs(Server.MapPath(path));
                _mtbl.material_path = path;
            }

            
            var eid = "";
            eid = Convert.ToString(Session["email-id"]);
            
            _mtbl.fk_email_id = eid;
            _mtbl.status = 1;
            _mtbl.material_name = sub;
            var dt = System.DateTime.Now;
            _mtbl.material_date = dt;
            _mtbl.type_of_material = type;

                db.and_material_tbl.Add(_mtbl);
                db.SaveChanges();

          
            return RedirectToAction("view", "Admin_material");
        }

        public ActionResult view()   //all material
        {
            List<material_admin> objlist = new List<material_admin>();
            var q = (from r in db.and_material_tbl
                     where r.status == 1
                     orderby r.material_date descending
                     select new { r.pk_material_id, r.material_path, r.material_date, r.material_name, r.type_of_material }).ToList();


            Session["Count"] = q.Count;
            if (q.Count > 0)
            {
                foreach (var item in q)
                {
                    DateTime dt = Convert.ToDateTime(item.material_date);
                    objlist.Add(new material_admin() { pk_material_id = item.pk_material_id, material_name = item.material_name, material_path = item.material_path, material_date = dt, type_of_material = item.type_of_material });

                }
            }
            else
            {
                ViewBag.message = "No Materials yet";
            }

            return View("view_material_admin", objlist);
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

        public ActionResult Edit(int id)
        {

            var data = (from r in db.and_material_tbl
                        where r.pk_material_id == id
                        select r).FirstOrDefault();
            material_admin mt = new material_admin();
            mt.pk_material_id = data.pk_material_id;
           
            mt.material_name = data.material_name;
            mt.material_path = data.material_path;
            mt.type_of_material = data.type_of_material;
           
           
            return View("edit_material_admin", mt);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {

            var data = (from r in db.and_material_tbl
                        where r.pk_material_id == id
                        select r).FirstOrDefault();
      
      
            data.material_name = collection["material_name"];
            HttpPostedFileBase hp = Request.Files["fileuploadDemo"];
            if (hp != null && hp.FileName != "")
            {
                string path = "~/material/" + hp.FileName;
                if (data.material_path != path)
                {
                    System.IO.File.Delete(Server.MapPath(data.material_path));
                    hp.SaveAs(Server.MapPath(path));

                }
                data.material_path = path;
            }

            data.type_of_material = collection["type_mat"];
      

            material_admin mt = new material_admin();

            mt.fk_email_id = data.fk_email_id;
            mt.material_name = data.material_name;
            mt.material_path = data.material_path;
            mt.type_of_material = data.type_of_material;
            var dt = System.DateTime.Now;
            mt.material_date = dt;
            mt.status = data.status;

            db.SaveChanges();
            return RedirectToAction("view");
        }



        public ActionResult Delete(int id)
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
                return RedirectToAction("view", "Admin_material");

            }
            catch
            {
                return View("Error");

            }
        }

        public ActionResult DeleteAll(FormCollection frm)
        {
            var chkvalue = frm.GetValues("assignChkBx");
            foreach (var id in chkvalue)
            {
                material_admin _model = new material_admin();
                int id1 = Convert.ToInt32(id);
                var data = (from c in db.and_material_tbl where c.pk_material_id == id1 select c).First();

                if (data.material_path != null || data.material_path != "")
                {
                    System.IO.File.Delete(Server.MapPath(data.material_path));
                }
                
                db.and_material_tbl.Remove(data);
                db.SaveChanges();
            }
            return RedirectToAction("view", "Admin_material");
        }

    }
}
