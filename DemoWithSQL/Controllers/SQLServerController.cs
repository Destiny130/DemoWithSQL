using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWithSQL.Models;
using System.Net;
using System.Threading;
using System.Text;

namespace DemoWithSQL.Controllers
{
    public class SQLServerController : Controller
    {
        DemoSQLEntities db = new DemoSQLEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SQL_Fruits_Index()
        {
            IQueryable<Fruits> fruits = db.Fruits;

            fruits = fruits.OrderByDescending(f => f.FruitKey).Skip(0).Take(10);
            return View(fruits);
        }

        public ActionResult SQL_Fruits_Search(SearchList searchList)
        {
            IQueryable<Fruits> fruits = db.Fruits.Where(f => f.Name == searchList.Name).OrderByDescending(f => f.CreateDate);
            return Json(fruits);
        }

        public ActionResult SQL_Fruits_Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SQL_Fruits_Create([Bind(Include = "FruitKey,Name,Price,Description,Message")] Fruits fruits)
        {
            if (ModelState.IsValid)
            {
                fruits.CreateDate = DateTime.Now;
                db.Fruits.Add(fruits);
                db.SaveChanges();
                return RedirectToAction("SQL_Fruits_Index");
            }

            return View(fruits);
        }

        public ActionResult SQL_Fruits_CreateMany()
        {
            Random rd = new Random();
            int count = rd.Next(10, 20);
            for (int i = 0; i < count; i++)
            {
                Fruits fruit = new Fruits();
                List<string> nameList = new List<string> { "Apple", "Orange", "Pear", "Banana", "Grape", "Plum", "Honeymelon", "Papaya" };
                int nameCount = nameList.Count();
                fruit.Name = nameList[rd.Next(1, nameCount - 1)];
                fruit.Price = Math.Round(rd.NextDouble() * 100 + 100, 2);
                fruit.Description = "This is " + fruit.Name;
                StringBuilder message = new StringBuilder();
                message.Append("<FruitData>");
                message.Append("<TotalNumber>" + rd.Next(10, 300) + "</TotalNumber>");
                message.Append("<SendCountryCode>" + rd.Next(1, 130) + "</SendCountryCode>");
                message.Append("<TakeCountryCode>" + rd.Next(1, 130) + "</TakeCountryCode>");
                message.Append("<ShipNumber>" + rd.Next(30, 90) + "</ShipNumber>");
                message.Append("</FruitData>");
                fruit.Message = message.ToString();
                fruit.CreateDate = DateTime.Now;
                db.Fruits.Add(fruit);
                db.SaveChanges();
                Thread.Sleep(rd.Next(156000, 402000));  //26 min to 67 min.
            }
            return Content("Create Success!");
        }

        public ActionResult SQL_Fruits_Details(int? id)
        {
            Fruits fruit = new Fruits();
            if (id != null)
            {
                fruit = db.Fruits.Where(f => f.FruitKey == id).FirstOrDefault();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (fruit == null)
            {
                return HttpNotFound();
            }
            return View(fruit);
        }

        public ActionResult SQL_Fruits_Delete(int? id)
        {
            Fruits fruit = new Fruits();
            if (id != null)
            {
                fruit = db.Fruits.Where(f => f.FruitKey == id).FirstOrDefault();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (fruit == null)
            {
                return HttpNotFound();
            }
            TempData["DeleteStatus"] = "Delete Success!";
            return View("SQL_Fruits_Index");
        }
    }

    public class SearchList
    {
        public string FruitKey { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string TotalNumber { get; set; }
        //public string CreateDate { get; set; }
        public string PageSize { get; set; }
        public string TotalPage { get; set; }
        public string CurrentPage { get; set; }
    }
}
