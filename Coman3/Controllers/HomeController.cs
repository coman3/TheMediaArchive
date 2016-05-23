using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Coman3.Models.Database;

namespace Coman3.Controllers
{
    public class HomeController : Controller
    {
        //private ApplicationDbContext DbContext = ApplicationDbContext.Create();
        public ActionResult Index()
        {
            return View();
        }
    }
}