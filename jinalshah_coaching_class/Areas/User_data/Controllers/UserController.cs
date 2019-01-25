
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jinalshah_coaching_class.Areas.User_data.Models;
using jinalshah_coaching_class.Entity_Model;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Net;



namespace jinalshah_coaching_class.Areas.User_data.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        user umodel = new user();
        and_user_tbl utbl = new and_user_tbl();
	and_testimonials_tbl ttable = new and_testimonials_tbl();
        Database1Entities db = new Database1Entities();
        public ActionResult Index()
        {
            return View("login");
        }
        public ActionResult sign_up()
        {
            return View();
        }
        public ActionResult about_us()
        {
            return View();
        }
        public ActionResult contact_us()
        {
            return View();
        }
        public ActionResult why_us_user()
        {
            return View();
        }

        public ActionResult our_services_user()
        {
            return View();
        }
        public ActionResult follow_us_user()
        {
            return View();
        }
       
        public ActionResult view_gallery()
        {
            return View("gallery");
        }
        public ActionResult mail()
        {

            return View("contact_us");
        }
        public ActionResult Reach_to_us_user()
        {
            return View();
        }
        public ActionResult log_out()
        {
            Session.Remove("email-id");
            return RedirectToAction("Index", "Visitor_user", new {area="" });
        }

        //public ActionResult login_insert(FormCollection frm)
        //{
        //    var name = frm["email"];
        //    var pwd = frm["pwd"];
        //    var q = (from r in db.and_user_tbl where r.pk_email_id == name && r.password == pwd select r).FirstOrDefault();
        //    if (q != null)
        //    {
        //        Session["email-id"] = name;
        //        if (q.status == "1")
        //        {
        //            return RedirectToAction("Index", "Admin_user",new {area="Admin"});
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index1", "Home" ,new  {area="User_data"});
                    
                    
        //        }
        //    }
        //    else
        //    {
        //        return View("login");
        //    }
        //}
        [HttpPost]
        public ActionResult insert_user(FormCollection frm)
        {
            utbl.pk_email_id = frm["txteid"];


            utbl.password = frm["txtpwd"];
            utbl.fname = frm["txtfnm"];
            utbl.lname = frm["txtlnm"];
            utbl.address = frm["txtadd"];
            utbl.gender = frm["txtgen"];
            
            utbl.phone_no = frm["txtph"];
            
            utbl.status = "0";
            utbl.semester = frm["txtsem"];
            HttpPostedFileBase hp = Request.Files["fileuploadDemo"];
            if (hp != null && hp.FileName != "")
            {
                string img = hp.FileName;
                string path = "~/User_img/" + img;
                hp.SaveAs(Server.MapPath(path));
                utbl.photo = path;
            }
            db.and_user_tbl.Add(utbl);
            db.SaveChanges();

            return RedirectToAction("Index1", "Home");
        }

        public ActionResult chng_pwd()
        {
            ViewBag.name=Session["email-id"];
            return View("change_password");
        }

        public ActionResult Password_change(FormCollection frm)
        {
            var opwd=frm["old_pwd"];
            var npwd = frm["new_pwd"];
            var cn=frm["cnfrm"];
            string name = Convert.ToString(Session["email-id"]);
            var ans1 = from r in db.and_user_tbl
                       where r.password == opwd && r.pk_email_id==name
                       select r;
            if (ans1.Count()>0)
            {
                var t = (from r in db.and_user_tbl
                         where r.pk_email_id == name
                         select r).FirstOrDefault();
                t.password = npwd;
                if (npwd != cn)
                {
                    ViewBag.msg = "New password and Confirm Password should be same";
                }
                else
                {
                    db.SaveChanges();
                    ViewBag.success = "Password change successfully";
                    ViewBag.name = Session["email-id"];
                    
                }
                return View("change_password");
            }
            else
            {

                ViewBag.pwd = "old pwd doesnot match";
                return View("change_password");
            }
        }
        public ActionResult vew_profile()
        {

            string name = Convert.ToString(Session["email-id"]);
            var all = (from r in db.and_user_tbl
                       where r.pk_email_id == name
                       select r).FirstOrDefault();
            umodel.fname = all.fname;
            umodel.lname = all.lname;
            umodel.address = all.address;
            umodel.gender = all.gender;
            umodel.pk_email_id = all.pk_email_id;
            umodel.semester = all.semester;
            umodel.photo = all.photo;
            umodel.phone_no = all.phone_no;
            return View("View_profile",umodel);
        }
        public ActionResult edit_profile()
        {
            string name = Convert.ToString(Session["email-id"]);

            var all = (from r in db.and_user_tbl
                       where r.pk_email_id == name
                       select r).FirstOrDefault();

            umodel.fname = all.fname;
            umodel.lname = all.lname;
            umodel.address = all.address;
            umodel.gender = all.gender;
            umodel.pk_email_id = all.pk_email_id;
            
            umodel.photo = all.photo;
            umodel.phone_no = all.phone_no;
            getallsemester();
            umodel.semester = all.semester;
            return View(umodel);
        }
        public ActionResult edit(FormCollection frm)
        {
           
            var ans = Convert.ToString(Session["email-id"]);
               var t = (from r in db.and_user_tbl
                     where r.pk_email_id==ans
                     select r).FirstOrDefault();
            t.fname=frm["fname"];
            t.lname = frm["lname"];
            t.address = frm["address"];
            t.gender = frm["gender"];
            t.semester = frm["semester"];
            t.phone_no = frm["ph_no"];
           
            db.SaveChanges();
            ViewBag.update = "Updated successfully";
            return RedirectToAction("vew_profile", "User");
        }
        private void getallsemester()
        {
            var data = (from c in db.and_user_tbl
                       select c.semester).Distinct();
            ViewBag.sem = data;
        }
	

        public ActionResult testi_view_add(int? Page_No)
        {
            List<testimonial> lst = new List<testimonial>();
            var t = (from r in db.and_testimonials_tbl
                     join u in db.and_user_tbl on r.fk_email_id equals u.pk_email_id
                     where r.status=="Approved"
                     select new { r.fk_email_id, r.desc, r.date,u.photo,u.fname });
            if (t.Count() > 0)
            {
                foreach (var item in t)
                {
                    lst.Add(new testimonial() { fk_email_id = item.fk_email_id, desc = item.desc, date = item.date, photo = item.photo,fname=item.fname });
                }
            }

            else
            {
                ViewBag.testy = "No Review yet";
            }
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            return View(lst.ToPagedList(No_Of_Page, Size_Of_Page));
        
        }
        public ActionResult add_tasty(FormCollection frm)
        {
            var name=Session["email-id"].ToString();
            ttable.fk_email_id = name;
            ttable.desc = frm["desc"];
            ttable.date = System.DateTime.Now;
            ttable.status = "Pending";
            db.and_testimonials_tbl.Add(ttable);
            db.SaveChanges();
            return RedirectToAction("testi_view_add", "User");
        }

        public ActionResult msg(FormCollection frm)
        {
            string sub = frm["subject"];
            string ms = frm["message"];
            string id = frm["name"];
            string ans = sendMail("jinalshah999@gmail.com", id, sub, ms);
            ViewBag.msg = "Your mail has been successfully sent";
            return View("contact_us");

        }

        public static string sendMail(string to, string from, string subject, string message)
        {
            string responseData = "";
            string data = string.Format("to={0}&from={1}&title={2}&message={3}&pass=RSRD@1234",
                to, from, subject, message);

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://jinalshah.brainoorja.com/jin/sendmail.php?" + data);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.designbazzar.com/sendmail.php");
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "POST";
            byte[] bytes = new byte[data.Length * sizeof(char)];
            System.Buffer.BlockCopy(data.ToCharArray(), 0, bytes, 0, bytes.Length);
            //request.ContentLength = bytes.Length;
            request.ContentType = "application/x-www-form-urlencoded";

            //Stream dataStream = request.GetRequestStream();
            //dataStream.Write(bytes, 0, bytes.Length);
            //dataStream.Close();

            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(data);
            }

            var response = request.GetResponse();


            using (StreamReader responseReader = new StreamReader(response.GetResponseStream()))
            {
                responseData += responseReader.ReadToEnd();
            }

            return responseData;
        }

        public FileResult Downloadapk()
        {

            var filepath = "http://jinalshah1.brainoorja.com/BooksBart.apk";
         
                string contentType = "application/apk";
                return File(filepath, contentType, "Download.apk");
          
        }

    }
}
