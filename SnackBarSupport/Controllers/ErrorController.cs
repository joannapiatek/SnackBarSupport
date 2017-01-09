using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.ViewModels;

namespace SnackBarSupport.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(string message)
        {
            var model = new SnackBarError()
            {
                Message = message
            };
            return View(model);
        }
    }
}