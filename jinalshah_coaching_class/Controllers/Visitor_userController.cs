using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jinalshah_coaching_class.Models;
using jinalshah_coaching_class.Entity_Model;
using PagedList;
using PagedList.Mvc;
using System.Net;
using System.IO;

namespace jinalshah_coaching_class.Controllers
{
    public class Visitor_userController : Controller
    {
        //
        // GET: /Visitor/Visitor_user/

        and_user_tbl utbl = new and_user_tbl();
        and_testimonials_tbl ttable = new and_testimonials_tbl();
        Database1Entities db = new Database1Entities();
        public ActionResult Index()
        {
            return View("login");
        }
        private void getallsemester()
        {
            var data = (from c in db.and_user_tbl
                        select c.semester).Distinct();
            ViewBag.sem = data;
        }
        public ActionResult testi_msg()
        {
            ViewBag.msg="Please login_insert";
            return View("testimonial_view_add_visitor");
        }
        public ActionResult sign_up()
        {
          
            
            Session.Remove("email-id");
            return View();
        }
        public ActionResult about_us()
        {
            return View("about_us_visitor");
        }
        public ActionResult contact_us()
        {
            return View("contact_us_visitor");
        }
        public ActionResult view_gallery()
        {
            return View("gallery_visitor");
        }
        public ActionResult view_services()
        {
            return View("our_services");
        }
        public ActionResult why_us()
        {
            return View("Why_us_visitor");
        }
      
        public ActionResult mail()
        {

            return View("contact_us_visitor");
        }
        public ActionResult Reach_to_us()
        {
            return View();
        }
        public ActionResult log_out()
        {
            Session.Remove("email-id"); 
            return View("login", new { area = "Areas_User" });
        }

        public ActionResult login_insert(FormCollection frm)
        {
            var name = frm["email"];
            var pwd = frm["pwd"];

            if (name == "jinalshah999@gmail.com" && pwd == "jinal")
            {
                Session["email-id"] = name;
                return RedirectToAction("index", "admin_user", new { area = "admin" });
            }
            else
            {
            var q = from r in db.and_user_tbl where r.pk_email_id == name && r.password == pwd && r.status == "Approved" select r;
            var q1 = from r in db.and_user_tbl where r.pk_email_id == name && r.password == pwd && r.status == "Pending" select r;
                if (q.ToList().Count == 1)
                {
                    Session["email-id"] = name;
                    return RedirectToAction("Index1", "Home", new { area = "User_data" });
                 }
                else if (q1.ToList().Count == 1)
                {
                    ViewBag.err1 = "Your request has been sent to admin and soon it will be approved !!";
                    return View("login");
                }
                else
                {
                    ViewBag.err = "Incorrect Email Id or Password.";
                    return View("login");
                }
              
            }
         
        }

        [HttpPost]
        public ActionResult insert_user(FormCollection frm)
        {
            int flag = 0;
            var data = from r in db.and_user_tbl select r;
            foreach (var item in data)
            {
                string email=frm["txteid"].ToString();
                if (item.pk_email_id == email )
                {
                    flag = 1;
                    ViewBag.msg = "Email Id already Exists";
                }

            }
            if (flag == 0)
            {
                utbl.pk_email_id = frm["txteid"];

                utbl.password = frm["txtpwd"];
                utbl.fname = frm["txtfnm"];
                utbl.lname = frm["txtlnm"];
                utbl.address = frm["txtadd"];
                utbl.gender = frm["txtgen"];

                utbl.phone_no = frm["txtph"];

                utbl.status = "Pending";
                utbl.semester = frm["sem"];
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

                return View("msg_dsply");
            }
            else
            {
                return RedirectToAction("sign_up", "Visitor_user");

            }
        }

        public ActionResult go_bck()
        {
            return RedirectToAction("home_visitor", "Visitor_home");
        }

       public ActionResult for_pwd()
        {
            return View("Forget_pwd");
        }
        public ActionResult password_forget(FormCollection frm)
        {
            var name = frm["email"];

            var ans = (from r in db.and_user_tbl
                       where r.pk_email_id == name
                       select r).FirstOrDefault();
            if (ans != null)
            {
                //pwd mail krvano;
                string p = "Your password is "+ans.password;
                string ans1 = sendMail1(name,"jinalshah999@gmail.com","Forgot password", p);
                ViewBag.msg = "Your mail has been successfully sent";
                return View("Forget_pwd");
               
            }
            else
            {
                ViewBag.message = "You are not a user...";
                return View("Forget_pwd");
              
            }
            
        }
       
        public static string sendMail1(string to, string from, string subject, string message)
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


        public ActionResult testimonial_view_add_visitor(int? Page_No)
        {
            List<testimonial_visitor> lst = new List<testimonial_visitor>();
            var t = (from r in db.and_testimonials_tbl
                     join u in db.and_user_tbl on r.fk_email_id equals u.pk_email_id
                     where r.status=="Approved"
                     select new { r.fk_email_id, r.desc, r.date, u.photo, u.fname });
            if (t.Count() > 0)
            {
                foreach (var item in t)
                {
                    lst.Add(new testimonial_visitor() { fk_email_id = item.fk_email_id, desc = item.desc, date = item.date, photo = item.photo, fname = item.fname });
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
            var name = Session["email-id"].ToString();
            ttable.fk_email_id = name;
            ttable.desc = frm["desc"];
            ttable.date = System.DateTime.Now;
            db.and_testimonials_tbl.Add(ttable);
            db.SaveChanges();
            return RedirectToAction("testimonial_view", "Visitor_User");
        }

        public ActionResult msg(FormCollection frm)
        {
            string sub=frm["subject"];
            string ms=frm["message"];
            string id=frm["name"];
            string ans=sendMail("jinalshah999@gmail.com", id, sub, ms);
            ViewBag.msg = "Your mail has been successfully sent";
            return View("contact_us_visitor");

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
        public ActionResult follow()
        {
            return View("follow_us");
        }
        public FileResult Downloadapk()
        {

            var filepath = "http://jinalshah1.brainoorja.com/BooksBart.apk";

            string contentType = "application/msi";
            return File(filepath, contentType, "Download.apk");

        }
    }
}
