using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jinalshah_coaching_class.Areas.Admin.Controllers
{
    public class Admin_slideController : Controller
    {
        //
        // GET: /Admin/Admin_slide/

        public ActionResult Index()
        {
            return View("slide_chnage");
        }
        public ActionResult add()
        {
            HttpPostedFileBase hp = Request.Files["fileuploadDemo1"];
            if (hp != null && hp.FileName != "")
            {
                string img = hp.FileName;

                string original1 = "~/assets/images/slides/slide-1.jpg";
                string path1 = "~/assets/images/slides/" + img;


                original1 = path1.Replace(path1, "~/assets/images/slides/slide-1.jpg");

                hp.SaveAs(Server.MapPath(original1));

            }

            HttpPostedFileBase hp1 = Request.Files["fileuploadDemo2"];
            if (hp1 != null && hp1.FileName != "")
            {
                string img2 = hp1.FileName;


                string original2 = "~/assets/images/slides/slide-2.jpg";
                string path2 = "~/assets/images/slides/" + img2;


                original2 = path2.Replace(path2, "~/assets/images/slides/slide-2.jpg");

                hp1.SaveAs(Server.MapPath(original2));

            }

            HttpPostedFileBase hp2 = Request.Files["fileuploadDemo3"];
            if (hp2 != null && hp2.FileName != "")
            {
                string img3 = hp2.FileName;

                string original3 = "~/assets/images/slides/slide-3.jpg";
                string path3 = "~/assets/images/slides/" + img3;


                original3 = path3.Replace(path3, "~/assets/images/slides/slide-3.jpg");

                hp2.SaveAs(Server.MapPath(original3));

            }

            //HttpPostedFileBase hp4 = Request.Files["fileuploadDemo4"];
            
            //}

            ViewBag.msg = "Images successfully changed...!!!!";
            return View("slide_chnage");
            //return RedirectToAction("Index", "Visitor_user");

        }
        public ActionResult slide2()
        {
            return View();
        }
        public ActionResult add_chng()
        {

            HttpPostedFileBase hp4 = Request.Files["fileuploadDemo4"];
            if (hp4 != null && hp4.FileName != "")
            {
                string img4 = hp4.FileName;

                string original4 = "~/assets/images/slides/slide-4.jpg";
                string path4 = "~/assets/images/slides/" + img4;


                original4 = path4.Replace(path4, "~/assets/images/slides/slide-4.jpg");

                hp4.SaveAs(Server.MapPath(original4));
            }
                ViewBag.msg = "done";
                return View("slide2");
            
        } 
    }
}
