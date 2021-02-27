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
    public class ItemsController : Controller
    {
        private StoreFrontEntities1 db = new StoreFrontEntities1();

        // GET: Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Category).Include(i => i.Manufacture);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.ManufacturerID = new SelectList(db.Manufactures, "ManufacturerID", "ManufacturerName");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemID,ItemName,Description,CategoryID,ManufacturerID,ItemImage,IsFeatured")]
            Item item, HttpPostedFileBase itemImage)
        {
            if (ModelState.IsValid)
            {
                string imgName = "no-image.png";

                if (itemImage != null)
                {
                    imgName = itemImage.FileName;

                    string ext = imgName.Substring(imgName.LastIndexOf('.'));
                    string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };

                    if (goodExts.Contains(ext.ToLower()) && itemImage.ContentLength <= 4194304)
                    {
                        imgName = Guid.NewGuid() + ext.ToLower();

                        string savePath = Server.MapPath("~/Content/img/product/");
                        Image convertedImage = Image.FromStream(itemImage.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                        ImageService.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);
                    }
                    else
                    {
                        imgName = "no-image.png";
                    }
                }

                item.ItemImage = imgName;

                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", item.CategoryID);
            ViewBag.ManufacturerID = new SelectList(db.Manufactures, "ManufacturerID", "ManufacturerName", item.ManufacturerID);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", item.CategoryID);
            ViewBag.ManufacturerID = new SelectList(db.Manufactures, "ManufacturerID", "ManufacturerName", item.ManufacturerID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,ItemName,Description,CategoryID,ManufacturerID,ItemImage,IsFeatured")]
            Item item, HttpPostedFileBase itemImage)
        {
            if (ModelState.IsValid)
            {
                if (itemImage != null)
                {
                    string imgName = itemImage.FileName;
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".gif", ".png" };

                    if (goodExts.Contains(ext.ToLower()) && itemImage.ContentLength <= 4194304 )
                    {
                        imgName = Guid.NewGuid() + ext.ToLower();

                        string savePath = Server.MapPath("~/Content/img/product/");
                        Image convertedImage = Image.FromStream(itemImage.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                        ImageService.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);

                        if (item.ItemImage != null && item.ItemImage != "no-image.png")
                        {
                            string path = Server.MapPath("~/Content/img/product");
                            ImageService.Delete(path, item.ItemImage);
                        }

                        item.ItemImage = imgName;
                    }
                }

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", item.CategoryID);
            ViewBag.ManufacturerID = new SelectList(db.Manufactures, "ManufacturerID", "ManufacturerName", item.ManufacturerID);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
