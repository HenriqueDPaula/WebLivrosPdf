using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Front.Contexts;
using Front.Models;

namespace Front.Controllers
{
    public class SellItemsController : Controller
    {
        private ECommerceDbContext db = new ECommerceDbContext();

        // GET: SellItems
        public ActionResult Index()
        {
            var sellItems = db.SellItems.Include(s => s.Product).Include(s => s.Sell);
            return View(sellItems.ToList());
        }

        // GET: SellItems/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellItem sellItem = db.SellItems.Find(id);
            if (sellItem == null)
            {
                return HttpNotFound();
            }
            return View(sellItem);
        }

        // GET: SellItems/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");
            ViewBag.SellId = new SelectList(db.Sells, "SellId", "SellNumver");
            return View();
        }

        // POST: SellItems/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SellItemId,Quantity,UnitPrice,ProductId,SellId")] SellItem sellItem)
        {
            if (ModelState.IsValid)
            {
                db.SellItems.Add(sellItem);
                var sell = db.Sells.Find(sellItem.SellId);
                sell.TotalPrice += sellItem.Quantity * sellItem.UnitPrice;
                db.Entry(sell).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Sells", new { id = sellItem.SellId });
            }

            //ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", sellItem.ProductId);
            //ViewBag.SellId = new SelectList(db.Sells, "SellId", "SellNumver", sellItem.SellId);
            return RedirectToAction("Edit", "Sells", new { id = sellItem.SellId });
        }

        // GET: SellItems/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellItem sellItem = db.SellItems.Find(id);
            if (sellItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", sellItem.ProductId);
            ViewBag.SellId = new SelectList(db.Sells, "SellId", "SellNumver", sellItem.SellId);
            return View(sellItem);
        }

        // POST: SellItems/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SellItemId,Quantity,UnitPrice,ProductId,SellId")] SellItem sellItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sellItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products
                .Where(o => o.Deleted == false), "ProductId", "Name", sellItem.ProductId);
            ViewBag.SellId = new SelectList(db.Sells, "SellId", "SellNumver", sellItem.SellId);
            return View(sellItem);
        }

        // GET: SellItems/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellItem sellItem = db.SellItems.Find(id);
            if (sellItem == null)
            {
                return HttpNotFound();
            }
            return View(sellItem);
        }

        // POST: SellItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            SellItem sellItem = db.SellItems.Find(id);
            db.SellItems.Remove(sellItem);
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
