using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TallamondInventory.Models;
using PagedList;

namespace TallamondInventory.Controllers
{
    public class InventoriesController : Controller
    {
        private TallamondEntities db = new TallamondEntities();

        //// GET: Inventories
        //public ActionResult Index()
        //{
        //    return View(db.Inventories.ToList());
        //}

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "Title" : "";
            ViewBag.DateSortParm = sortOrder == "CreatedDate" ? "date_desc" : "CreatedDate";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var inventory = from s in db.Inventories
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                inventory = inventory.Where(s => s.Title.IndexOf(searchString, 0, StringComparison.CurrentCultureIgnoreCase) != -1);
            }

            switch (sortOrder)
            {
                case "Title":
                    inventory = inventory.OrderByDescending(s => s.Title);
                    break;
                case "CreatedDate":
                    inventory = inventory.OrderBy(s => s.CreatedDate);
                    break;
                case "date_desc":
                    inventory = inventory.OrderByDescending(s => s.CreatedDate);
                    break;
                default:
                    inventory = inventory.OrderBy(s => s.Title);
                    break;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(inventory.ToPagedList(pageNumber, pageSize));
        }

        // GET: Inventories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // GET: Inventories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Handle,Title,Body__HTML_,Type,Tags,Published,Option1_Name,Option1_Value,Option2_Name,Option2_Value,Option3_Name,Option3_Value,Original_Price,Variant_SKU,Variant_Price,Variant_Inventory_Qty,Variant_Taxable,Vendor,VendorShort,Lead_Time,MOQ,Condition,Collection,SEO_Title,SEO_Description,Image_Src,CreatedDate,ModifiedDate")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Inventories.Add(inventory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Handle,Title,Body__HTML_,Type,Tags,Published,Option1_Name,Option1_Value,Option2_Name,Option2_Value,Option3_Name,Option3_Value,Original_Price,Variant_SKU,Variant_Price,Variant_Inventory_Qty,Variant_Taxable,Vendor,VendorShort,Lead_Time,MOQ,Condition,Collection,SEO_Title,SEO_Description,Image_Src,CreatedDate,ModifiedDate")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inventory inventory = db.Inventories.Find(id);
            db.Inventories.Remove(inventory);
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
