using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DemoWithSQL.Models;

namespace DemoWithSQL.Controllers
{
    public class FruitsController : Controller
    {
        private DemoSQLEntities db = new DemoSQLEntities();

        // GET: Fruits
        public ActionResult Index()
        {
            return View(db.Fruits.ToList());
        }

        // GET: Fruits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fruits fruits = db.Fruits.Find(id);
            if (fruits == null)
            {
                return HttpNotFound();
            }
            return View(fruits);
        }

        // GET: Fruits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fruits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FruitKey,Name,Price,Description,Message,CreateDate")] Fruits fruits)
        {
            if (ModelState.IsValid)
            {
                db.Fruits.Add(fruits);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fruits);
        }

        // GET: Fruits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fruits fruits = db.Fruits.Find(id);
            if (fruits == null)
            {
                return HttpNotFound();
            }
            return View(fruits);
        }

        // POST: Fruits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FruitKey,Name,Price,Description,Message,CreateDate")] Fruits fruits)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fruits).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fruits);
        }

        // GET: Fruits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fruits fruits = db.Fruits.Find(id);
            if (fruits == null)
            {
                return HttpNotFound();
            }
            return View(fruits);
        }

        // POST: Fruits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fruits fruits = db.Fruits.Find(id);
            db.Fruits.Remove(fruits);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
