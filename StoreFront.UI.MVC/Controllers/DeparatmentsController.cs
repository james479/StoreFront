using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront.DATA.EF;

namespace StoreFront.UI.MVC.Controllers
{
    public class DeparatmentsController : Controller
    {
        private StoreFrontEntities1 db = new StoreFrontEntities1();

        // GET: Deparatments
        public ActionResult Index()
        {
            return View(db.Deparatments.ToList());
        }

        // GET: Deparatments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deparatment deparatment = db.Deparatments.Find(id);
            if (deparatment == null)
            {
                return HttpNotFound();
            }
            return View(deparatment);
        }

        // GET: Deparatments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Deparatments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentID,DepartmentName")] Deparatment deparatment)
        {
            if (ModelState.IsValid)
            {
                db.Deparatments.Add(deparatment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deparatment);
        }

        // GET: Deparatments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deparatment deparatment = db.Deparatments.Find(id);
            if (deparatment == null)
            {
                return HttpNotFound();
            }
            return View(deparatment);
        }

        // POST: Deparatments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentID,DepartmentName")] Deparatment deparatment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deparatment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deparatment);
        }

        // GET: Deparatments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deparatment deparatment = db.Deparatments.Find(id);
            if (deparatment == null)
            {
                return HttpNotFound();
            }
            return View(deparatment);
        }

        // POST: Deparatments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deparatment deparatment = db.Deparatments.Find(id);
            db.Deparatments.Remove(deparatment);
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
