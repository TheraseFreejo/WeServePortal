using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WeServe.Models;

namespace WeServe.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HomePage()
        {
            return View();
        }


        public ActionResult AboutUs()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            LoginModel obj = new LoginModel();
            return View(obj);
        }
        [HttpPost]
        public JsonResult Login(string txtuname,string txtpass)
        {
            TbUser u = new TbUser();

            #region "Declaration"
            //  BookingModel m = new BookingModel();
            LoginModel m = new LoginModel();
            #endregion

            #region "Select Data from DB and copy to Model"
            using (var db = new weserveEntities())
            {
                m.Username = txtuname;
                m.Password = txtpass;
                var objLogin = (from x in db.TbUsers
                                where x.UserName == txtuname && x.Password == txtpass
                                select x).FirstOrDefault();




                if (objLogin == null)
                {
                    ModelState.AddModelError("UserName", "Unauthorized to view contents");
                  //  return View(m);
                    return Json(new { redirectTo = Url.Action("Login", "Home"),m }, JsonRequestBehavior.AllowGet);
                }
                else
                m.usertype = objLogin.Role;


            }
            #endregion

            FormsAuthentication.SetAuthCookie(m.Username, false);
            var myCookie = new HttpCookie("myCookie");//instantiate an new cookie and give it a name
            myCookie.Values.Add("Username", m.Username.ToString());//populate it with key, value pairs
            Response.Cookies.Add(myCookie);//add it to the client
                                           //     string status = "OK";

            if (m.usertype == "admin")
               //  return RedirectToAction("Index","Admin", new { area = "" });
                //return View("~/Views/Admin/Index.cshtml");
                return Json(new { redirectTo = Url.Action("Index", "Admin"), }, JsonRequestBehavior.AllowGet);

            else
                return Json(new { redirectTo = Url.Action("HomePage", "Home"), }, JsonRequestBehavior.AllowGet);
            // return View("~/Views/Home/HomePage.cshtml");
            // return RedirectToAction("HomePage", "Home", new { area = "" });
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            UserModel model = new UserModel();
            return View(model);
        }
        [HttpPost]
        public JsonResult CreateUser(string txtuname,string txtpass,string txtfname,string txtlname,string txtdob,string txtphone,string txtemail,string txtaddress)
        {
                
                #region "Declaration"
                 UserModel m = new UserModel();
            #endregion
            m.UserName = txtuname;
            m.Password = txtpass;
            m.FirstName = txtfname;
            m.LastName = txtlname;
            m.DOB = Convert.ToDateTime(txtdob);
            m.Phone = txtphone;
            m.Email = txtemail;
            m.Address = txtaddress;
            if (!ModelState.IsValid)
            {
                //  return Json(new { redirectTo = Url.Action("CreateUser", "Home"), }, JsonRequestBehavior.AllowGet);
                //  return View(mod);
                return Json(m);
            }
            #region "Create a new user and save it to database"
            #endregion
            using (var db = new weserveEntities())
            {
                TbUser u = new TbUser();
                u.UserName = txtuname;
                u.FirstName = txtfname;
                u.LastName = txtlname;
                u.DOB = Convert.ToDateTime(txtdob);
                u.Password = txtpass;
                u.Email = txtemail;
                u.Phone = txtphone;
                u.Address = txtaddress;
                u.Role = "member";
                db.TbUsers.Add(u);
                db.SaveChanges();
                return Json(m);
                // return Json(new { redirectTo = Url.Action("Login", "Home"), }, JsonRequestBehavior.AllowGet);


            }
            
        }
    }  
        
    
}