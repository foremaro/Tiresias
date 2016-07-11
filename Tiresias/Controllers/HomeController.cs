using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tiresias.DAL;

namespace Tiresias.Controllers
{
    public class HomeController : Controller
    {

        public tiresiasDBcontextDataContext dbContext = new tiresiasDBcontextDataContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About Us";

            return View();
        }


        public ActionResult Admin()
        {
            ViewBag.Message = "Admin Page";

            return View();
        }


    }
}