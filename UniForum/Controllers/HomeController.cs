using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace UniForum.Controllers
{

    //[Authorize(Roles = "admin,user,customer")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

   
    }
}