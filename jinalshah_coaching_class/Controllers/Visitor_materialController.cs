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
    public class Visitor_materialController : Controller
    {
        //
        // GET: /Visitor/Visitor_material/

        material_visitor _m = new material_visitor();
        and_material_tbl _mtbl = new and_material_tbl();
        Database1Entities db = new Database1Entities();

        public ActionResult Index()
        {
            return RedirectToAction("view_material_visitor", "Visitor_material");
        }

        public ActionResult all_subjects_visitor()
        {
            return View();
        }
        
        public ActionResult add_mat()
        {
            return View("lab_work_upload_visitor");
        }
        

        public ActionResult view_material_visitor(int? Page_No, string name)   //all material
        {
          
            List<material_visitor> objlist = new List<material_visitor>();
            var q = (from r in db.and_material_tbl
                     where r.status == 1 && r.type_of_material == name
                     orderby r.material_date
                     select new { r.pk_material_id, r.material_path, r.material_date, r.material_name, r.type_of_material });



            if (q.Count() > 0)
            {
                foreach (var item in q)
                {
                    DateTime dt = Convert.ToDateTime(item.material_date);
                    objlist.Add(new material_visitor() { pk_material_id = item.pk_material_id, material_name = item.material_name, material_path = item.material_path, material_date = dt, type_of_material = item.type_of_material });

                }
            }
            else
            {
                ViewBag.message = "No Materials yet";
            }

            int Size_Of_Page = 20;
            int No_Of_Page = (Page_No ?? 1);
            return View(objlist.ToPagedList(No_Of_Page, Size_Of_Page));
         
        }

    }
}
