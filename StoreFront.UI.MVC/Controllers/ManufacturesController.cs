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
    public class ManufacturesController : Controller
    {
        private StoreFrontEntities1 db = new StoreFrontEntities1();

        

        #region Ajax Create

        public JsonResult AjaxCreate(Manufacture manufacture)
        {
            db.Manufactures.Add(manufacture);
            db.SaveChanges();
            return Json(manufacture);
        }

        #endregion

        #region Ajax Delete

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AjaxDelete(int id)
        {
            Manufacture manufacture = db.Manufactures.Find(id);
            db.Manufactures.Remove(manufacture);
            db.SaveChanges();
            var message = $"Deleted {manufacture.ManufacturerName} from the database";

            return Json(new
            {
                id = id,
                message = message
            });
        }

        #endregion

        #region Edit [Get]

        public PartialViewResult ManufactureEdit(int id)
        {
            Manufacture manufacture = db.Manufactures.Find(id);
            ViewBag.StateID = new SelectList(db.States, "StateID", "StateName", manufacture.StateID);
            
            
            return PartialView(manufacture);
        }

        #endregion

        #region Category Edit [Post]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AjaxEdit(Manufacture manufacture)
        {
            db.Entry(manufacture).State = EntityState.Modified;
            db.SaveChanges();
            return Json(manufacture);
        }

        #endregion

        // GET: Manufactures
        public ActionResult Index()
        {
            var manufactures = db.Manufactures.Include(m => m.State);
            return View(manufactures.ToList());
        }

        // GET: Manufactures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacture manufacture = db.Manufactures.Find(id);
            if (manufacture == null)
            {
                return HttpNotFound();
            }
            return View(manufacture);
        }

        //// GET: Manufactures/Create
        public ActionResult Create()
        {
            ViewBag.StateID = new SelectList(db.States, "StateID", "StateName");
            return View();
        }

        // POST: Manufactures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ManufacturerID,ManufacturerName,City,StateID,Country")] Manufacture manufacture)
        {
            if (ModelState.IsValid)
            {
                db.Manufactures.Add(manufacture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StateID = new SelectList(db.States, "StateID", "StateName", manufacture.StateID);
            return View(manufacture);
        }


        //// GET: Manufactures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacture manufacture = db.Manufactures.Find(id);
            if (manufacture == null)
            {
                return HttpNotFound();
            }
            ViewBag.StateID = new SelectList(db.States, "StateID", "StateName", manufacture.StateID);

            return View(manufacture);
        }

        //// POST: Manufactures/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ManufacturerID,ManufacturerName,City,StateID,Country")] Manufacture manufacture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manufacture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateID = new SelectList(db.States, "StateID", "StateName", manufacture.StateID);
            return View(manufacture);
        }

        //// GET: Manufactures/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Manufacture manufacture = db.Manufactures.Find(id);
        //    if (manufacture == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(manufacture);
        //}

        // POST: Manufactures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manufacture manufacture = db.Manufactures.Find(id);
            db.Manufactures.Remove(manufacture);
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
