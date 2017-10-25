using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWithSQL.Models;
using System.Net;
using System.Threading;

namespace DemoWithSQL.Controllers
{
    public class SQLServerController : Controller
    {
        DemoSQLEntities db = new DemoSQLEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SQL_SearchDemo_Index(string search,string searchGWE)
        {
            search = search == null ? "" : search;
            IQueryable<SearchTest> searchtests = db.SearchTests;
            ViewBag.search = null;
            ViewBag.searchGWE = null;
            if (!string.IsNullOrEmpty(search))
            {
               string searchc = "<test5>" + search + "</test5>";
                searchtests = searchtests.Where(s => s.RequestMsg.Contains(searchc));
                ViewBag.search = search;
            }
            if (!string.IsNullOrEmpty(searchGWE))
            {
                string searchGWEc = "<GWE>" + searchGWE + "</GWE>";
                searchtests = searchtests.Where(s => s.RequestMsg.Contains(searchGWEc));
                ViewBag.searchGWE = searchGWE;
            }
            int count = searchtests.Count();
            searchtests = searchtests.OrderByDescending(s => s.ID).Take(15);
            ViewBag.count = count;
            return View(searchtests);
        }

        public ActionResult SQL_SearchDemo_Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SQL_SearchDemo_Create_JS()
        {
            for (int i = 0; i < 300000; i++)
            {
                SearchTest searchtest = new SearchTest();
                searchtest.DefaultKey = i;
                searchtest.Name = "aName" + (i * 11).ToString();
                searchtest.Description = "A description " + (i * 32).ToString();
                List<string> nodes = new List<string> { "tjrsst1", "teshgrct2", "teawfst3", "test4", "test5", "test6gs", "testse7", "te7dtrst8", "tessst9", "tezg8st10","324","fgrrRsdfgRSf","SF","GWE","FWd36Ds" };
                string request = "<data dfgs fdgs fdgfg sehb dhgf>";
                string response = "<data jpforejpgf gmio wpetojnv>";
                Random rd = new Random();
                foreach (string str in nodes)
                {
                    request += "<" + str + ">" + rd.Next(1, 19).ToString() + "</" + str + ">";
                    response += "<" + str + ">" + rd.Next(1, 19).ToString() + "</" + str + ">";
                }
                request += "</data>";
                response += "</data>";
                searchtest.RequestMsg = request;
                searchtest.ResponseMsg = response;
                searchtest.CreateDate = DateTime.Now;
                db.SearchTests.Add(searchtest);
                db.SaveChanges();
                if (i % 100 == 0)
                {
                    Thread.Sleep(2000);
                }
            }
            return Content("success");
        }

        public ActionResult SQL_SearchDemo_Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SearchTest searchTest = db.SearchTests.Find(id);
            if (searchTest == null)
            {
                return HttpNotFound();
            }
            return View(searchTest);
        }
    }
}