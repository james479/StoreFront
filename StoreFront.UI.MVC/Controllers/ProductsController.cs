using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC3.UI.MVC.Utilities;
using StoreFront.DATA.EF;

namespace StoreFront.UI.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private StoreFrontEntities1 db = new StoreFrontEntities1();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.MaskSize).Include(p => p.StockStatu).Include(p => p.CategoryID).Include(p => p.MachineType).Include(p => p.MaskType).Include(p => p.Manufacture);
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
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.MachineTypeID = new SelectList(db.MachineTypes, "MachineTypeID", "MachineTypeName");
            ViewBag.MaskTypeID = new SelectList(db.MaskTypes, "MaskTypeID", "MaskTypeName");
            ViewBag.ManufacturerID = new SelectList(db.Manufactures, "ManufacturerID", "ManufacturerName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,IsFeatured,Price,MaskSizeID,StockStatusID,UnitsAvailable,IsReplacement,ProductName,CategoryID,Description,MaskTypeID,MachineTypeID,ManufacturerID,ProductImage")]
            Product product, HttpPostedFileBase productImage)
        {
            if (ModelState.IsValid)
            {
                string imgName = "No_Image_Available.jpg";

                if (productImage != null)
                {
                    imgName = productImage.FileName;

                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };

                    if (goodExts.Contains(ext.ToLower()) && productImage.ContentLength <= 4194304)
                    {
                        imgName = Guid.NewGuid() + ext.ToLower();

                        string savePath = Server.MapPath("~/Content/img/imgstore/");
                        Image convertedImage = Image.FromStream(productImage.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                        ImageService.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);
                    }

                    else
                    {
                        imgName = "No_Image_Available.jpg";
                    }
                }

                product.ProductImage = imgName;


                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaskSizeID = new SelectList(db.MaskSizes, "MaskSizeID", "Size", product.MaskSizeID);
            ViewBag.StockStatusID = new SelectList(db.StockStatus, "StockStatusID", "StockStatus", product.StockStatusID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.MachineTypeID = new SelectList(db.MachineTypes, "MachineTypeID", "MachineTypeName", product.MachineTypeID);
            ViewBag.MaskTypeID = new SelectList(db.MaskTypes, "MaskTypeID", "MaskTypeName", product.MaskTypeID);
            ViewBag.ManufacturerID = new SelectList(db.Manufactures, "ManufacturerID", "ManufacturerName", product.ManufacturerID);
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
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.MachineTypeID = new SelectList(db.MachineTypes, "MachineTypeID", "MachineTypeName", product.MachineTypeID);
            ViewBag.MaskTypeID = new SelectList(db.MaskTypes, "MaskTypeID", "MaskTypeName", product.MaskTypeID);
            ViewBag.ManufacturerID = new SelectList(db.Manufactures, "ManufacturerID", "ManufacturerName", product.ManufacturerID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,IsFeatured,Price,MaskSizeID,StockStatusID,UnitsAvailable,IsReplacement,ProductName,CategoryID,Description,MaskTypeID,MachineTypeID,ManufacturerID,ProductImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaskSizeID = new SelectList(db.MaskSizes, "MaskSizeID", "Size", product.MaskSizeID);
            ViewBag.StockStatusID = new SelectList(db.StockStatus, "StockStatusID", "StockStatus", product.StockStatusID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.MachineTypeID = new SelectList(db.MachineTypes, "MachineTypeID", "MachineTypeName", product.MachineTypeID);
            ViewBag.MaskTypeID = new SelectList(db.MaskTypes, "MaskTypeID", "MaskTypeName", product.MaskTypeID);
            ViewBag.ManufacturerID = new SelectList(db.Manufactures, "ManufacturerID", "ManufacturerName", product.ManufacturerID);
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
