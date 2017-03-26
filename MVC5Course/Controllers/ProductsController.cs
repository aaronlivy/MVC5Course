using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using PagedList;
using System.Data.Entity.Validation;
using MVC5Course.ActionFilters;

namespace MVC5Course.Controllers
{
    
    public class ProductsController : BaseController
    {
        private int DefaultPageSize = 20;
        private FabricsEntities db = new FabricsEntities();
        
        // GET: Products
        public ActionResult Index(string ActiveFilter, string SortBy, string KeyWord = "", int PageNo = 1, bool ShowAll = false)
        {
            //ViewBag.ActiveFilter = new SelectList(new List<string> { "True", "False" });
            var activeOptions = ProductRepo.All().Select(o => o.Active.HasValue ? o.Active.ToString() : "false").Distinct().ToList();

            ViewBag.ActiveFilter = new SelectList(activeOptions);

            DoProductSearch(ActiveFilter, SortBy, KeyWord, PageNo, ShowAll);

            return View();
        }

        private void DoProductSearch(string ActiveFilter="", string SortBy = "", string KeyWord = "", int PageNo = 1, bool ShowAll = false)
        {
            
            var data = ProductRepo.All(ShowAll).AsQueryable();
            if (!string.IsNullOrEmpty(KeyWord))
                data = data.Where(o => o.ProductName.Contains(KeyWord));
            if (!string.IsNullOrEmpty(ActiveFilter))
            {
                bool AF = Boolean.Parse(ActiveFilter);
                data = data.Where(o => o.Active == AF);
            }
            if (SortBy == "+Price")
                data = data.OrderBy(o => o.Price);
            else
                data = data.OrderByDescending(o => o.Price);

            ViewBag.KeyWord = KeyWord;
            ViewBag.SortBy = SortBy;
            ViewBag.PageNo = PageNo;
            ViewBag.ShowAll = ShowAll;

            ViewData.Model = data.ToPagedList(PageNo, DefaultPageSize);
        }

        [HttpPost]
        public ActionResult Index(string ActiveFilter, Product[] Data, string SortBy, string KeyWord = "", int PageNo = 1, bool ShowAll = false)
        {
            if(ModelState.IsValid)
            {
                foreach (var item in Data)
                {
                    var Product = ProductRepo.Find(item.ProductId);
                    Product.Price = item.Price;
                    Product.ProductName = item.ProductName;
                    Product.Stock = item.Stock;
                    ProductRepo.UnitOfWork.Commit();
                }
            }

            var activeOptions = ProductRepo.All().Select(o => o.Active.HasValue ? o.Active.ToString() : "false").Distinct().ToList();

            ViewBag.ActiveFilter = new SelectList(activeOptions);

            DoProductSearch(ActiveFilter, SortBy, KeyWord, PageNo, ShowAll);

            return RedirectToAction("Index");
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = ProductRepo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult ProductOrderLines(int id)
        {
            Product product = ProductRepo.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product.OrderLine);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                ProductRepo.Add(product);
                ProductRepo.UnitOfWork.Commit();                
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = ProductRepo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(View= "Error_DbEntityValidationException",ExceptionType = typeof(DbEntityValidationException))]
        public ActionResult Edit(int id, FormCollection form)
        {
            var Product = ProductRepo.Find(id);
            if (TryUpdateModel(Product, new string[] { "ProductName", "Price", "Active" }))
            {
                
            }

            ProductRepo.UnitOfWork.Commit();
            return RedirectToAction("Index");

            //return View(Product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = ProductRepo.Find(id.Value);
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
            Product product = ProductRepo.Find(id);
            ProductRepo.Delete(product);
            ProductRepo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
