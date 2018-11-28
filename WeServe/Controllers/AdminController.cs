using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WeServe.Models;

namespace WeServe.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewBookings()
        {

            #region "Declaration"
            List<BookingModel> lstBookings = new List<BookingModel>();
            //  ICollection<ServiceDDLViewmodel> lstServices;
            #endregion
            using (var db = new weserveEntities())
            {
                var objBookings = db.Fetch_Bookings().ToList();
                //u.Username = model.Username;
                //u.AccountNo = objLogin.AccountNo;
                lstBookings = objBookings.AsEnumerable()
                          .Select(x => new WeServe.Models.BookingModel
                          {
                              bookid = Convert.ToInt32(x.Id),
                              userid = Convert.ToInt32(x.UserId),
                              username = x.UserName,
                              serviceid = Convert.ToInt32(x.Id),
                              service = x.title,
                              description = x.description,
                              bookdate = Convert.ToDateTime(x.bookdate)

                          }
                          ).ToList();

                //  db.SaveChanges();
                return View(lstBookings);
            }
        }
        [HttpGet]
        public ActionResult Confirm(int id)
        {
            ////  System.Net.Mail fro = new System.Net.Mail();
            //  string from = "therasefreejo@gmail.com";
            //  SmtpClient client = new SmtpClient();
            //  //If you need to authenticate
            //  client.Credentials = new NetworkCredential("c", "jonathan2013");
            //  MailMessage mailMessage = new MailMessage();
            //  mailMessage.From = new MailAddress(from.ToString());
            //  mailMessage.To.Add("therasefreejo@gmail.com");
            //  mailMessage.Subject = "Hello There";
            //  mailMessage.Body = "Hello my friend!";
            //  client.Send(mailMessage);


            //MailMessage msgMail = new MailMessage();
            //msgMail.To.Add(new MailAddress("therasefreejo@gmail.com"));
            //msgMail.Subject = "Message from web";
            //msgMail.IsBodyHtml = true;
            //msgMail.Body = "Test message";
            //SmtpClient Client = new SmtpClient();  /* uses settings form web.config */
            //Client.ServicePoint.MaxIdleTime = 1; /* without this the connection is idle too long and not terminated, times out at the server and gives sequencing errors */
            //Client.Send(msgMail);D:\Therase\MVC\WeServe\WeServe\css\newstyle.css
            //msgMail.Dispose(); 


            #region "Declaration"
            List<BookingModel> lstBookings = new List<BookingModel>();
            BookingModel b = new BookingModel();
            #endregion

            #region "Select record from db"
            using (weserveEntities db = new weserveEntities())
            {
                var objBooking = (from x in db.tbbookings where x.Id==id
                                 select x).FirstOrDefault();
                b.confirmstatus = 1;
                b.bookid = id;
                b.userid = Convert.ToInt32(objBooking.userid);
                             // b.username = objBooking.u,
                              b.serviceid = Convert.ToInt32(objBooking.Id);
                              b.service = objBooking.description;
                            //  b.description = x.description,
                              b.bookdate = Convert.ToDateTime(objBooking.bookdate);


              //  b.confirmstatus = 1;
              //  db.Edit_Booking(m.bookid);
              // db.SaveChanges();
            }
            #endregion
            return View(b);
        }

        [HttpPost]
        public ActionResult Confirm(BookingModel model)
        {
            using (var db = new weserveEntities())
            {
               // tbbooking obj = new tbbooking();
                db.Edit_Booking(model.bookid);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Serving");
        }

       


    }
}