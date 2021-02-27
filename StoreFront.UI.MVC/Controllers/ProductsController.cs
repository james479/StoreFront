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
    public class ProductsController : Controller
    {
        private StoreFrontEntities1 db = new StoreFrontEntities1();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.MaskSize).Include(p => p.StockStatu).Include(p => p.MachineType).Include(p => p.MaskType).Include(p => p.Item);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.MaskSizeID = new SelectList(db.MaskSizes, "MaskSizeID", "Size");
            ViewBag.StockStatusID = new SelectList(db.StockStatus, "StockStatusID", "StockStatus");
            ViewBag.MachineTypeID = new SelectList(db.MachineTypes, "MachineTypeID", "MachineTypeName");
            ViewBag.MaskTypeID = new SelectList(db.MaskTypes, "MaskTypeID", "MaskTypeName");
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Price,MaskSizeID,StockStatusID,UnitsAvailable,MaskTypeID,MachineTypeID,ItemID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaskSizeID = new SelectList(db.MaskSizes, "MaskSizeID", "Size", product.MaskSizeID);
            ViewBag.StockStatusID = new SelectList(db.StockStatus, "StockStatusID", "StockStatus", product.StockStatusID);
            ViewBag.MachineTypeID = new SelectList(db.MachineTypes, "MachineTypeID", "MachineTypeName", product.MachineTypeID);
            ViewBag.MaskTypeID = new SelectList(db.MaskTypes, "MaskTypeID", "MaskTypeName", product.MaskTypeID);
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemName", product.ItemID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaskSizeID = new SelectList(db.MaskSizes, "MaskSizeID", "Size", product.MaskSizeID);
            ViewBag.StockStatusID = new SelectList(db.StockStatus, "StockStatusID", "StockStatus", product.StockStatusID);
            ViewBag.MachineTypeID = new SelectList(db.MachineTypes, "MachineTypeID", "MachineTypeName", product.MachineTypeID);
            ViewBag.MaskTypeID = new SelectList(db.MaskTypes, "MaskTypeID", "MaskTypeName", product.MaskTypeID);
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemName", product.ItemID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Price,MaskSizeID,StockStatusID,UnitsAvailable,MaskTypeID,MachineTypeID,ItemID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaskSizeID = new SelectList(db.MaskSizes, "MaskSizeID", "Size", product.MaskSizeID);
            ViewBag.StockStatusID = new SelectList(db.StockStatus, "StockStatusID", "StockStatus", product.StockStatusID);
            ViewBag.MachineTypeID = new SelectList(db.MachineTypes, "MachineTypeID", "MachineTypeName", product.MachineTypeID);
            ViewBag.MaskTypeID = new SelectList(db.MaskTypes, "MaskTypeID", "MaskTypeName", product.MaskTypeID);
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemName", product.ItemID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
