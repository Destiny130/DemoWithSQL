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
            string[] strs = new string[] { "ASP", "ASP.NET", "ASP.NET MVC", "JAVA", "PYTHON", "C", "C#", "C++", "JAVASCRIPT", "SPRING MVC", "RUBY", "BILL", "LAW", "DELL", "SONY" };
            ViewBag.strs = strs;
            return View();
        }

        public ActionResult MVC_TransManyParam()
        {
            return View();
        }

        public ActionResult GetParams(string str/*, int id, string[] strs, int[] ids, Fruit fruit, Fruit[] fruits*/)
        {
            string ret = "show: ";
            try
            {
                ret += str + ",&nbsp;";
                //ret += id.ToString() + "<br />";
                //foreach (string s in strs)
                //{
                //    ret += s + ",&nbsp;";
                //}
                //ret += "<br />";
                //foreach (int i in ids)
                //{
                //    ret += i.ToString() + ",&nbsp;";
                //}
                //ret += "<br />";
                //ret += fruit.Name + "<br />";
                //foreach (Fruit f in fruits)
                //{
                //    ret += fruit.Name + ",&nbsp;";
                //}
                //ret += "<br />";
            }
            catch (Exception ex)
            {
                ret += ex.Message;
            }
            return Content(ret);
        }
    }

    public class Fruit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsOnSale { get; set; }
    }
}