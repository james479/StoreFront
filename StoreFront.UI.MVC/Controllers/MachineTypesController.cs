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
    public class MachineTypesController : Controller
    {
        private StoreFrontEntities1 db = new StoreFrontEntities1();

        //ajax methods to replace the regular action methods

        #region Ajax Create

        public JsonResult AjaxCreate(MachineType machineType)
        {
            db.MachineTypes.Add(machineType);
            db.SaveChanges();
            return Json(machineType);
        }

        #endregion

        #region Edit - GET
        
        public PartialViewResult MachineTypeEdit(int id)
        {
            MachineType machineType = db.MachineTypes.Find(id);
            return PartialView(machineType);
        }


        #endregion

        #region Edit - POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AjaxEdit(MachineType machineType)
        {
            db.Entry(machineType).State = EntityState.Modified;
            db.SaveChanges();
            return Json(machineType);
        }

        #endregion

        #region Ajax Delete

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AjaxDelete(int id)
        {
            MachineType machineType = db.MachineTypes.Find(id);
            db.MachineTypes.Remove(machineType);
            db.SaveChanges();
            var message = $"Delete {machineType.MachineTypeName} from the database";

            return Json(new
            {
                id = id,
                message = message
            });
        }

        #endregion


        // GET: MachineTypes
        public ActionResult Index()
        {
            return View(db.MachineTypes.ToList());
        }

        // GET: MachineTypes/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MachineType machineType = db.MachineTypes.Find(id);
        //    if (machineType == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(machineType);
        //}

        //// GET: MachineTypes/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: MachineTypes/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MachineTypeID,MachineTypeName")] MachineType machineType)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.MachineTypes.Add(machineType);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(machineType);
        //}

        //// GET: MachineTypes/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MachineType machineType = db.MachineTypes.Find(id);
        //    if (machineType == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(machineType);
        //}

        //// POST: MachineTypes/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "MachineTypeID,MachineTypeName")] MachineType machineType)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(machineType).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(machineType);
        //}

        //// GET: MachineTypes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MachineType machineType = db.MachineTypes.Find(id);
        //    if (machineType == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(machineType);
        //}

        //// POST: MachineTypes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    MachineType machineType = db.MachineTypes.Find(id);
        //    db.MachineTypes.Remove(machineType);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
