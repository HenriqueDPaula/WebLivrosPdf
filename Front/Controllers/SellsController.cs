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
    public class SellsController : Controller
    {
        private ECommerceDbContext db = new ECommerceDbContext();

        // GET: Sells
        public ActionResult Index()
        {
            return View(db.Sells.ToList());
        }

        // GET: Sells/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sell sell = db.Sells.Find(id);
            if (sell == null)
            {
                return HttpNotFound();
            }
            return View(sell);
        }

        // GET: Sells/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sells/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SellId,SellNumver,Date,BuyerName,BuyerDoc,PhoneNumber,TotalPrice")] Sell sell)
        {
            if (ModelState.IsValid)
            {
                sell.Date = DateTime.Now;
                db.Sells.Add(sell);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = sell.SellId });
            }

            return View(sell);
        }

        // GET: Sells/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sell sell = db.Sells
                .Include(s => s.SellItems)
                .FirstOrDefault(s => s.SellId == id.Value);

            if (sell == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProductId = new SelectList(db.Products
                .Where(o => o.Deleted == false), "ProductId", "Name");
            ViewBag.SellId = new SelectList(db.Sells, "SellId", "SellNumver");
            return View(sell);
        }

        // POST: Sells/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SellId,SellNumver,Date,BuyerName,BuyerDoc,PhoneNumber,TotalPrice")] Sell sell)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sell).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sell);
        }

        // GET: Sells/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sell sell = db.Sells.Find(id);
            if (sell == null)
            {
                return HttpNotFound();
            }
            return View(sell);
        }

        // POST: Sells/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Sell sell = db.Sells.Find(id);
            
            // exclui todos os itens da venda primeiro
            db.SellItems
                .Where(i => i.SellId == id)
                .ToList()
                .ForEach(i => db.SellItems.Remove(i));
            db.Sells.Remove(sell);
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
