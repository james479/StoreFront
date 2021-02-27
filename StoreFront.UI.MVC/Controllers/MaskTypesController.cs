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
    public class MaskTypesController : Controller
    {
        private StoreFrontEntities1 db = new StoreFrontEntities1();

        #region Ajax Create

        public JsonResult AjaxCreate(MaskType maskType)
        {
            db.MaskTypes.Add(maskType);
            db.SaveChanges();
            return Json(maskType);
        }

        #endregion

        #region Edit - GET

        public PartialViewResult MaskTypeEdit(int id)
        {
            MaskType maskType = db.MaskTypes.Find(id);
            return PartialView(maskType);
        }

        #endregion

        #region Edit - POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AjaxEdit(MaskType maskType)
        {
            db.Entry(maskType).State = EntityState.Modified;
            db.SaveChanges();
            return Json(maskType);
        }

        #endregion

        #region Ajax Delete

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AjaxDelete(int id)
        {
            MaskType maskType = db.MaskTypes.Find(id);
            db.MaskTypes.Remove(maskType);
            db.SaveChanges();
            var message = $"Deleted {maskType.MaskTypeName} from the database";

            return Json(new
            {
                id = id,
                message = message
            });
        }

        #endregion

        // GET: MaskTypes
        public ActionResult Index()
        {
            return View(db.MaskTypes.ToList());
        }

        // GET: MaskTypes/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MaskType maskType = db.MaskTypes.Find(id);
        //    if (maskType == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(maskType);
        //}

        //// GET: MaskTypes/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: MaskTypes/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MaskTypeID,MaskTypeName")] MaskType maskType)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.MaskTypes.Add(maskType);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(maskType);
        //}

        //// GET: MaskTypes/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MaskType maskType = db.MaskTypes.Find(id);
        //    if (maskType == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(maskType);
        //}

        //// POST: MaskTypes/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "MaskTypeID,MaskTypeName")] MaskType maskType)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(maskType).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(maskType);
        //}

        //// GET: MaskTypes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MaskType maskType = db.MaskTypes.Find(id);
        //    if (maskType == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(maskType);
        //}

        //// POST: MaskTypes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    MaskType maskType = db.MaskTypes.Find(id);
        //    db.MaskTypes.Remove(maskType);
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
