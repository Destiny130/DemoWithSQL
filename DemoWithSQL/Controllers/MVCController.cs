using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWithSQL.Models;
using System.Data.Objects;
using System.Text;
using System.IO;
//using System.Data.Entity.Core.Objects;
//using System.Management;

namespace DemoWithSQL.Controllers
{
    public class MVCController : Controller
    {
        DemoSQLEntities db = new DemoSQLEntities();

        public ActionResult Index()
        {
            var haha = db.SysAuths.Where(s => s.TypeKey > 0 && s.ControllerName.Contains("e")).OrderByDescending(r => r.TypeKey).Select(s => s.ID);
            int i = 0;
            foreach (var temp in haha)
            {
                i++;
            }
            ViewBag.test = i.ToString();
            return View();
        }

        public ActionResult MVC_SetAuth()
        {
            IQueryable<SysAuth> laterUse = db.SysAuths.Where(s => s.TypeKey == 1);
            //ObjectQuery<SysAuth> sysauth = db.SysAuths;
            //db.CommandTimeOut = 50000;
            //var temp = MyExtensions.ToTraceString<SysAuth>(laterUse);

            //db.Log = new DebugTextWriter();
            //var temp = ((System.Data.Entity.Core.Objects.ObjectQuery)laterUse).ToTraceString();
            //ViewBag.temp = temp;

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

        public ActionResult GetParams(string str, int id/*, string[] strs, int[] ids, Fruit fruit, Fruit[] fruits*/)
        {
            string ret = "show: ";
            try
            {
                ret += str + ",&nbsp;";
                ret += id.ToString() + "<br />";
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

        public ActionResult GetParams_jQuery(string str, int id, string[] strs, int[] ids, Fruit fruit, Fruit[] fruits)
        {
            string ret = "";
            try
            {
                ret += str + ",&nbsp;";
                ret += id.ToString() + "<br />";
                foreach (string s in strs)
                {
                    ret += s + ",&nbsp;";
                }
                ret += "<br />";
                foreach (int i in ids)
                {
                    ret += i.ToString() + ",&nbsp;";
                }
                ret += "<br />";
                ret += fruit.Name + "<br />";
                foreach (Fruit f in fruits)
                {
                    ret += f.Name + ",&nbsp;";
                }
                ret += "<br />";
            }
            catch (Exception ex)
            {
                ret += ex.Message;
            }
            return Content(ret);
        }
    }

    public static class MyExtensions
    {
        public static string ToTraceString<T>(this IQueryable<T> t)
        {
            string sql = "";
            ObjectQuery<T> oqt = t as ObjectQuery<T>;
            if (oqt != null)
                sql = oqt.ToTraceString();
            return sql;
        }
    }

    class DebugTextWriter : System.IO.TextWriter
    {
        public override void Write(char[] buffer, int index, int count)
        {
            System.Diagnostics.Debug.Write(new String(buffer, index, count));
        }

        public override void Write(string value)
        {
            System.Diagnostics.Debug.Write(value);
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.Default; }
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