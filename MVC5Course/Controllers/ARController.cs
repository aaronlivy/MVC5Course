using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    
    public class ARController : BaseController
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

        public ActionResult Json1()
        {
            return Json(new LoginVM() { Username = "Aaronlivy", Password="xxxxxx"},JsonRequestBehavior.AllowGet);
        }

        public ActionResult Redirect1()
        {
            return RedirectToAction("View3");
        }

        public ActionResult Rrdirect2()
        {
            return RedirectToActionPermanent("View3");
        }
        
    }
}