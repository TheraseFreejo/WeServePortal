using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeServe.Models;

namespace WeServe.Controllers
{
    public class ServingController : Controller
    {
        // GET: Serving

        [Authorize(Roles = "admin,member")]
        public ActionResult Index()
        {

            List<tbservice> lstServices = new List<tbservice>();
            using (weserveEntities db = new weserveEntities())
            {
                var objService = from x in db.tbservices
                                 select x;

                lstServices = objService.AsEnumerable()
                          .Select(x => new tbservice
                          {
                              Id = x.Id,
                              title = x.title,
                              charge = x.charge,
                              description = x.description
                          }
                          ).ToList();
            }

            return View(lstServices);
            // return View();
        }

        [HttpGet]
        public ActionResult Booking(int id)
        {
            #region "Declaration"
            BookingModel m = new BookingModel();
            #endregion

            #region "Select Data from DB and copy to Model"
            using (var db = new weserveEntities())
            {
                var objserviceModel = (from x in db.tbservices
                                       where x.Id == id
                                       select x).FirstOrDefault();
                m.userid = 1;//retrive from cookies using formauthentication
                m.serviceid = id;
                m.service = objserviceModel.title;
            }
            #endregion

            return View(m);



        }
        [HttpPost]
        public ActionResult Booking(BookingModel model)
        {
            if (ModelState.IsValid)
            {

                using (var db = new weserveEntities())
                {
                    tbbooking obj = new tbbooking();
                    obj.userid = model.userid;

                    obj.bookdate = model.bookdate;
                    obj.description = model.description;
                    obj.serviceid = model.serviceid;
                    obj.confirmstatus = 0;


                    db.tbbookings.Add(obj);
                    db.SaveChanges();
                }
              return  RedirectToAction("Index", "Serving");
            }
        

            else
                return View(model); 
            
        }

        public ActionResult viewDetails()
        {

            #region "Declaration"
            // List<tbservice> lstServices = new List<tbservice>();
            ICollection<ServiceDDLViewmodel> lstServices;
            #endregion
            using (weserveEntities db = new weserveEntities())
            {
                var objService = from x in db.tbservices
                                 select x;
                lstServices = objService.AsEnumerable()
                          .Select(x => new WeServe.Models.ServiceDDLViewmodel
                          {
                              serviceId = x.Id,
                              title = x.title
                          }
                          ).ToList();
            }
            ViewBag.data = lstServices;
            return View();
        }


        //public ActionResult getDetails()
        //{

        //    #region "Declaration"
        //    // List<tbservice> lstServices = new List<tbservice>();
        //    ICollection<ServiceDDLViewmodel> lstServices;
        //    #endregion
        //    using (weserveEntities db = new weserveEntities())
        //    {
        //        var objService = from x in db.tbservices
        //                         select x;
        //        lstServices = objService.AsEnumerable()
        //                  .Select(x => new WeServe.Models.ServiceDDLViewmodel
        //                  {
        //                      serviceId = x.Id,
        //                      title = x.title
        //                  }
        //                  ).ToList();
        //    }
        //    ViewBag.data = lstServices;
        //    return View();
        //}


        [HttpGet]
        public ActionResult GetService(int id)
        {

            #region "Declaration"
            tbservice serviceViewModel = new tbservice();
            // int companyId = Convert.ToInt16(ConfigurationManager.AppSettings["companyid"]);
            #endregion
            using (weserveEntities db = new weserveEntities())
            {
                var objService = (from x in db.tbservices
                                  where x.Id == id
                                  select x).FirstOrDefault();
                serviceViewModel.Id = id;
                serviceViewModel.title = objService.title;
                serviceViewModel.charge = objService.charge;
                serviceViewModel.description = objService.description;
                return PartialView("_GetServicePartial", serviceViewModel);

            }

        }

        public ActionResult ViewConfirmations()
        {

            #region "Declaration"
            List<BookingModel> lstConfirmations= new List<BookingModel>();
            //  ICollection<ServiceDDLViewmodel> lstServices;
            #endregion
            using (var db = new weserveEntities())
            {


                var cookie = Request.Cookies["myCookie"];
                var username = cookie.Values["Username"].ToString();
                ViewBag.AccountNo = username.ToString();

            
                    var objConfirmations = db.fetchConfirmedBookings(username).ToList();
                //u.Username = model.Username;
                //u.AccountNo = objLogin.AccountNo;
                lstConfirmations = objConfirmations.AsEnumerable()
                              .Select(x => new WeServe.Models.BookingModel
                              {
                                  bookid = Convert.ToInt32(x.Id),
                                //  userid = Convert.ToInt32(x.UserId),
                                  username = x.UserName,
                                  //serviceid = Convert.ToInt32(x.Id),
                                  service = x.title,
                                  description = x.description,
                                  bookdate = Convert.ToDateTime(x.bookdate)

                              }
                              ).ToList();

                    //  db.SaveChanges();

                
                return View(lstConfirmations);
            }
        }
    }
}