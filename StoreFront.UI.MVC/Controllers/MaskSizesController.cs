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
    public class MaskSizesController : Controller
    {
        private StoreFrontEntities1 db = new StoreFrontEntities1();

        #region Ajax Create

        public JsonResult AjaxCreate(MaskSize maskSize)
        {
            db.MaskSizes.Add(maskSize);
            db.SaveChanges();
            return Json(maskSize);
        }

        #endregion

        #region Ajax Delete

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AjaxDelete(int id)
        {
            MaskSize maskSize = db.MaskSizes.Find(id);
            db.MaskSizes.Remove(maskSize);
            db.SaveChanges();
            var message = $"Deleted {maskSize.Size} from the database";

            return Json(new
            {
                id = id,
                message = message
            });
        }

        #endregion

        #region Edit [Get]

        public PartialViewResult MaskSizeEdit(int id)
        {
            MaskSize maskSize = db.MaskSizes.Find(id);
            return PartialView(maskSize);
        }

        #endregion

        #region Edit [Post]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AjaxEdit(MaskSize maskSize)
        {
            db.Entry(maskSize).State = EntityState.Modified;
            db.SaveChanges();
            return Json(maskSize);
        }

        #endregion

        // GET: MaskSizes
        public ActionResult Index()
        {
            return View(db.MaskSizes.ToList());
        }

        // GET: MaskSizes/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MaskSize maskSize = db.MaskSizes.Find(id);
        //    if (maskSize == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(maskSize);
        //}

        //// GET: MaskSizes/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: MaskSizes/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MaskSizeID,Size")] MaskSize maskSize)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.MaskSizes.Add(maskSize);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(maskSize);
        //}

        //// GET: MaskSizes/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MaskSize maskSize = db.MaskSizes.Find(id);
        //    if (maskSize == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(maskSize);
        //}

        //// POST: MaskSizes/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "MaskSizeID,Size")] MaskSize maskSize)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(maskSize).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(maskSize);
        //}

        //// GET: MaskSizes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MaskSize maskSize = db.MaskSizes.Find(id);
        //    if (maskSize == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(maskSize);
        //}

        //// POST: MaskSizes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    MaskSize maskSize = db.MaskSizes.Find(id);
        //    db.MaskSizes.Remove(maskSize);
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
