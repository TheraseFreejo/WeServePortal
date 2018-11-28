using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeServe.Models
{
    public class BookingModel
    {
        public int bookid { get; set; }
        public int userid { get; set; }
        public string username { get; set; }
        public int serviceid { get; set; }
        public string service { get; set; }

        [Required(ErrorMessage = "A Name is required")]
        public string description { get; set; }
        public DateTime bookdate { get; set; }
        public int confirmstatus { get; set; }
    }
}