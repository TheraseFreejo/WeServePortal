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
        public ActionResult Login(LoginModel model)
        {
            TbUser u = new TbUser();

            #region "Declaration"
          //  BookingModel m = new BookingModel();
            #endregion

            #region "Select Data from DB and copy to Model"
            using (var db = new weserveEntities())
            {
                var objLogin = (from x in db.TbUsers
                                where x.UserName == model.Username && x.Password == model.Password
                                select x).FirstOrDefault();

                if (objLogin == null)
                {
                    ModelState.AddModelError("UserName", "Unauthorized to view contents");
                    return View(model);
                }
                else
                model.usertype = objLogin.Role;


            }
            #endregion

            FormsAuthentication.SetAuthCookie(model.Username, false);
            var myCookie = new HttpCookie("myCookie");//instantiate an new cookie and give it a name
            myCookie.Values.Add("Username", model.Username.ToString());//populate it with key, value pairs
            Response.Cookies.Add(myCookie);//add it to the client
                                           //     string status = "OK";
            
            if(model.usertype == "admin")
            return RedirectToAction("Index","Admin", new { area = "" });
            else
                return RedirectToAction("HomePage", "Home", new { area = "" });
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
        public ActionResult CreateUser(UserModel model)
        {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                #region "Declaration"
                // BookingModel m = new BookingModel();
                #endregion

                #region "Create a new user and save it to database"
                #endregion
                using (var db = new weserveEntities())
                {
                    TbUser u = new TbUser();
                    u.UserName = model.UserName;
                    u.FirstName = model.FirstName;
                    u.LastName = model.LastName;
                    u.DOB = model.DOB;
                    u.Password = model.Password;
                    u.Email = model.Email;
                    u.Phone = model.Phone;
                    u.Address = model.Address;
                    u.Role = "member";

                    db.TbUsers.Add(u);
                    db.SaveChanges();
                
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            
        }
    }  
        
    
}