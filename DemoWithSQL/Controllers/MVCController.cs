using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWithSQL.Models;

namespace DemoWithSQL.Controllers
{
    public class MVCController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        DemoSQLEntities db = new DemoSQLEntities();
        public ActionResult MVC_SetAuth()
        {
            var laterUse = db.SysAuths.Where(s => s.TypeKey == 1);
            var controllerNameList = laterUse.ToList().Select(s => s.ControllerName).Distinct();
            Dictionary<string, List<SelectListItem>> shows = new Dictionary<string, List<SelectListItem>>();
            foreach (string controllerName in controllerNameList)
            {
                var actionNameList = laterUse.Where(l => l.ControllerName == controllerName).Select(l => l.ActionName).ToList();
                List<SelectListItem> show = new List<SelectListItem>();
                foreach (string actionName in actionNameList)
                {
                    bool isSale = laterUse.Where(l => l.ControllerName == controllerName && l.ActionName == actionName).Select(l => l.IsSale).FirstOrDefault();
                    show.Add(new SelectListItem { Text = actionName, Value = controllerName + actionName, Selected = isSale });
                }
                shows.Add(controllerName, show);
            }
            return View(shows);
        }

        [HttpPost]
        public ActionResult Submit(FormCollection collection)
        {
            try
            {
                var laterUse = db.SysAuths.Where(s => s.TypeKey == 1);
                var controllerNameList = laterUse.ToList().Select(s => s.ControllerName).Distinct();
                foreach (string controllerName in controllerNameList)
                {
                    var actionNameList = laterUse.Where(l => l.ControllerName == controllerName).Select(l => l.ActionName).ToList();
                    foreach (string actionName in actionNameList)
                    {
                        string collKey = controllerName + actionName;
                        bool isSale = collection[collKey].ToString().ToLower().Contains("true");
                        var check = db.SysAuths.Where(s => s.TypeKey == 1 && s.ControllerName == controllerName && s.ActionName == actionName).FirstOrDefault();
                        check.IsSale = isSale;
                    }
                }
                db.SaveChanges();
                TempData["Sucess"] = "Submit Sucess.";
                return RedirectToAction("MVC_SetAuth", "MVC");
            }
            catch
            {
                TempData["Failed"] = "Submit Failed.";
                return RedirectToAction("MVC_SetAuth", "MVC");
            }
        }

        public ActionResult MVC_AutoComplete()
        {
            List<string> strs = new List<string> { "ASP", "ASP.NET", "ASP.NET MVC", "JAVA", "PYTHON", "C", "C#", "C++", "JAVASCRIPT", "SPRING MVC", "RUBY", "BILL", "LAW", "DELL", "SONY" };
            ViewBag.strs = strs;
            return View();
        }
    }
}