using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoWithSQL.Controllers
{
    public class JavaScriptController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JS_RangeOfAVariable()
        {
            return View();
        }

        public ActionResult JS_DynamicArray()
        {
            return View();
        }

        public ActionResult JS_DefineOwnObject()
        {
            return View();
        }

        public ActionResult JS_Validation()
        {
            return View();
        }

        public ActionResult JS_ValidateEmail(string email)
        {
            int apos = email.IndexOf("@");
            int dotpos = email.IndexOf(".");
            string message = "Not a valid E-mail.";
            if (apos > 0 && (dotpos - apos) > 1 && dotpos != (email.Length - 1))
            {
                message = "This is a valid E-mail.";
            }
            return Content(message);
        }

        public ActionResult JS_Confirm()
        {
            return View();
        }

        public ActionResult JS_DotToImg()
        {
            return View();
        }
    }
}