using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Geo.Grid.Tainan.OpenGov.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Index()
        {
            return View("Error");
            //test
        }

        public ViewResult NotFound()
        {
            //Response.StatusCode = 404;  //you may want to set this to 200
            //return View("NotFound");
            return View();
        }
    }
}