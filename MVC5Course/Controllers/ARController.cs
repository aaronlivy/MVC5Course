using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult View2()
        {
            return PartialView("Index");
        }

        public ActionResult View3()
        {
            return View();
        }

        public ActionResult File1()
        {
            return File(Server.MapPath("~/Content/Images/10752579.png"), "image/png");
        }

        public ActionResult File2()
        {
            return File(Server.MapPath("~/Content/Images/10752579.png"), "image/png", "白爛貓.jpg");
        }
    }
}