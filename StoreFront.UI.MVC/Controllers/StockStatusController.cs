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
    public class StockStatusController : Controller
    {
        private StoreFrontEntities1 db = new StoreFrontEntities1();

        #region Ajax Create

        public JsonResult AjaxCreate(StockStatu stockStatu)
        {
            db.StockStatus.Add(stockStatu);
            db.SaveChanges();
            return Json(stockStatu);
        }

        #endregion

        #region Edit [Get]

        public PartialViewResult StockStatusEdit(int id)
        {
            StockStatu stockStatu = db.StockStatus.Find(id);
            return PartialView(stockStatu);
        }

        #endregion

        #region Category Edit [Post]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AjaxEdit(StockStatu stockStatu)
        {
            db.Entry(stockStatu).State = EntityState.Modified;
            db.SaveChanges();
            return Json(stockStatu);
        }

        #endregion

        #region Ajax Delete

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AjaxDelete(int id)
        {
            StockStatu stockStatu = db.StockStatus.Find(id);
            db.StockStatus.Remove(stockStatu);
            db.SaveChanges();
            var message = $"Deleted {stockStatu.StockStatus} from the database";

            return Json(new
            {
                id = id,
                message = message
            });
        }

        #endregion

        // GET: StockStatus
        public ActionResult Index()
        {
            return View(db.StockStatus.ToList());
        }

        //// GET: StockStatus/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    StockStatu stockStatu = db.StockStatus.Find(id);
        //    if (stockStatu == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(stockStatu);
        //}

        //// GET: StockStatus/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: StockStatus/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "StockStatusID,StockStatus")] StockStatu stockStatu)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.StockStatus.Add(stockStatu);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(stockStatu);
        //}

        //// GET: StockStatus/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    StockStatu stockStatu = db.StockStatus.Find(id);
        //    if (stockStatu == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(stockStatu);
        //}

        //// POST: StockStatus/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "StockStatusID,StockStatus")] StockStatu stockStatu)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(stockStatu).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(stockStatu);
        //}

        //// GET: StockStatus/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    StockStatu stockStatu = db.StockStatus.Find(id);
        //    if (stockStatu == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(stockStatu);
        //}

        //// POST: StockStatus/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    StockStatu stockStatu = db.StockStatus.Find(id);
        //    db.StockStatus.Remove(stockStatu);
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
