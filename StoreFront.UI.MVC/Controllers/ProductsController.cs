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
using PagedList;
using PagedList.Mvc;
using StoreFront.UI.MVC.Models;

namespace StoreFront.UI.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private StoreFrontEntities1 db = new StoreFrontEntities1();

        //adding to cart method
        [HttpPost]
        public ActionResult AddToCart(int qty, int productID, string size)
        {
            //initalize empty local cart
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            string maskSize = null;
            //if size is null
            if (size != null)
            {
                maskSize = size;
            }

            //check global cart
            if (Session["cart"] != null)
            {
                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
            }
            //if global cart is empty
            else
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }

            //get product being added
            Product product = db.Products.Where(p => p.ProductID == productID).FirstOrDefault();

            //if product is null return to main shoping page
            if (product ==  null)
            {
                return RedirectToAction("Index");
            }

            //if product is found
            else
            {
                CartItemViewModel item = new CartItemViewModel(qty, product, size);

                //if product already in cart increase qty
                if (shoppingCart.ContainsKey(product.ProductID))
                {
                    shoppingCart[product.ProductID].Qty += qty;
                }
                //add to cart if not in cart already
                else
                {
                    shoppingCart.Add(product.ProductID, item);
                }

                //update session cart
                Session["Cart"] = shoppingCart;
            }
            return RedirectToAction("Index", "ShoppingCart");
        }

        // GET: Products
        public ActionResult Index(string searchString, int page = 1)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Session["HoldFilter"] = searchString;
            }
            else
            {
                searchString = Session["HoldFilter"] as string;
            }

            int pageSize = 6;


            var products = db.Products.Include(p => p.MaskSize).Include(p => p.StockStatu).Include(p => p.MachineType).Include(p => p.MaskType).Include(p => p.Category).Include(p => p.Manufacture)
                            .OrderBy(x => x.ProductName).ToList();

            ViewBag.SearchString = searchString;

            //if no searching string or serach string is all will return all products
            if (string.IsNullOrEmpty(searchString) || searchString == "all")
            {
                return View(products.ToPagedList(page, pageSize));
            }

            //filtered prodcuts based on searchString paramater
            else
            {
                return View(products.Where(x => x.MaskTypeID != null ? x.MaskType.MaskTypeName == searchString :
                            x.Category.CategoryName == searchString).ToPagedList(page, pageSize));
            }
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            List<SelectListItem> items;

            if (product.MaskSizeID != null)
            {
                items = new List<SelectListItem>();
                var sizes = product.MaskSize.Size;
                string[] maskSizes = sizes.Split(',');
                foreach (var item in maskSizes)
                {
                    items.Add(new SelectListItem { Text = item });
                }

                ViewBag.MaskSizeList = items;
            }
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
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.ManufacturerID = new SelectList(db.Manufactures, "ManufacturerID", "ManufacturerName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Price,MaskSizeID,StockStatusID,UnitsAvailable,MaskTypeID,MachineTypeID,ProductName,ProductDescription,ProductImage,CategoryID,ManufacturerID,IsFeatured")]
            Product product, HttpPostedFileBase productImage)
        {
            if (ModelState.IsValid)
            {
                string imgName = "no-image.png";

                if (productImage != null)
                {
                    imgName = productImage.FileName;
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };

                    if (goodExts.Contains(ext.ToLower()) && productImage.ContentLength <= 4194304)
                    {
                        imgName = Guid.NewGuid() + ext.ToLower();

                        string savePath = Server.MapPath("~/Content/img/product/");
                        Image convertedImage = Image.FromStream(productImage.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                        ImageService.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);
                    }
                    else
                    {
                        imgName = "no-image.png";
                    }
                }

                product.ProductImage = imgName;

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaskSizeID = new SelectList(db.MaskSizes, "MaskSizeID", "Size", product.MaskSizeID);
            ViewBag.StockStatusID = new SelectList(db.StockStatus, "StockStatusID", "StockStatus", product.StockStatusID);
            ViewBag.MachineTypeID = new SelectList(db.MachineTypes, "MachineTypeID", "MachineTypeName", product.MachineTypeID);
            ViewBag.MaskTypeID = new SelectList(db.MaskTypes, "MaskTypeID", "MaskTypeName", product.MaskTypeID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
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
            ViewBag.MachineTypeID = new SelectList(db.MachineTypes, "MachineTypeID", "MachineTypeName", product.MachineTypeID);
            ViewBag.MaskTypeID = new SelectList(db.MaskTypes, "MaskTypeID", "MaskTypeName", product.MaskTypeID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.ManufacturerID = new SelectList(db.Manufactures, "ManufacturerID", "ManufacturerName", product.ManufacturerID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Price,MaskSizeID,StockStatusID,UnitsAvailable,MaskTypeID,MachineTypeID,ProductName,ProductDescription,ProductImage,CategoryID,ManufacturerID,IsFeatured")]
            Product product, HttpPostedFileBase productImage)
        {
            if (ModelState.IsValid)
            {
                if (productImage != null)
                {
                    string imgName = productImage.FileName;

                    string ext = imgName.Substring(imgName.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".gif", ".png" };

                    if (goodExts.Contains(ext.ToLower()) && productImage.ContentLength <= 4194304)
                    {
                        imgName = Guid.NewGuid() + ext.ToLower();
                        string savePath = Server.MapPath("~/Content/img/product/");
                        Image convertedImage = Image.FromStream(productImage.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                        ImageService.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);

                        if (product.ProductImage != null && product.ProductImage != "no-image.jpg")
                        {
                            string path = Server.MapPath("~/Content/img/product/");
                            ImageService.Delete(path, product.ProductImage);
                        }

                        product.ProductImage = imgName;
                    }
                }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaskSizeID = new SelectList(db.MaskSizes, "MaskSizeID", "Size", product.MaskSizeID);
            ViewBag.StockStatusID = new SelectList(db.StockStatus, "StockStatusID", "StockStatus", product.StockStatusID);
            ViewBag.MachineTypeID = new SelectList(db.MachineTypes, "MachineTypeID", "MachineTypeName", product.MachineTypeID);
            ViewBag.MaskTypeID = new SelectList(db.MaskTypes, "MaskTypeID", "MaskTypeName", product.MaskTypeID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
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

        //ajax delete for prodcut
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AjaxDelete(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            var message = $"Deleted {product.ProductName} from the database";

            return Json(new
            {
                id = id,
                message = message
            });
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
