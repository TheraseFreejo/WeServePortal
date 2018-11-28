using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.ComponentModel.DataAnnotations;

namespace WeServe
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
         //   RegisterRoutes(RouteTable.Routes);
           // ModelBinders.Binders.DefaultBinder = new System.ComponentModel.DataAnnotations.Validator();
        
    }
    }
}
