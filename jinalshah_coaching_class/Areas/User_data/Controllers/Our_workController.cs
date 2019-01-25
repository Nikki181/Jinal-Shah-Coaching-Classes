using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jinalshah_coaching_class.Areas.User_data.Models;
using jinalshah_coaching_class.Entity_Model;

namespace jinalshah_coaching_class.Areas.User_data.Controllers
{
    public class Our_workController : Controller
    {
        //
        // GET: /Our_work/

        Database1Entities _entity = new Database1Entities();
        our_work _o = new our_work();
       and_our_work_tbl _otbl = new and_our_work_tbl();


        public ActionResult Index()
        {
            List<our_work> objlist = new List<our_work>();
            var q = (from r in _entity.and_our_work_tbl
                     
                     select r).ToList();

            if (q.Count > 0)
            {
                foreach (var item in q)
                {
                    objlist.Add(new our_work() { pk_our_work_id = item.pk_our_work_id, our_work_image = item.our_work_image, 
                        project_name=item.project_name, project_desc=item.project_desc,type=item.type});

                }
            }
            else
            {
                ViewBag.message = "No Projects yet";
            }

            return View("view_our_work", objlist);
        }
        public ActionResult load_view_detail(int id)
        {
            List<our_work> objlist = new List<our_work>();
            var q = (from r in _entity.and_our_work_tbl
                     where r.pk_our_work_id==id
                     select r).ToList();

            if (q.Count > 0)
            {
                foreach (var item in q)
                {
                    int t = Convert.ToInt16(item.team_size);
                   // int d = Convert.ToInt16(item.days_to_complete);

                    objlist.Add(new our_work()
                    {
                        pk_our_work_id = item.pk_our_work_id,
                        our_work_image = item.our_work_image,
                        project_name = item.project_name,
                        project_desc = item.project_desc,
                        team_size = t,
                        members_name = item.members_name,
                        days_to_complete = item.days_to_complete,
                        type = item.type,
                        platform = item.platform,
                        link=item.link
                    });

                }
            }
            else
            {
                ViewBag.message = "No Projects yet";
            }

            return View("detail_view_our_work", objlist);
        }

    }
}
