using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OopRestaurant.Models;

namespace OopRestaurant.Controllers
{
   
    public class MenuItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Public Actions
        // GET: MenuItems
        public ActionResult Index()
        {
            return View(db.MenuItems.ToList());
        }

        // GET: MenuItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MenuItem menuItem = db.MenuItems.Find(id);

            if (menuItem == null)
            {
                return HttpNotFound();
            }

            GetEntryAndLoadCategory(menuItem);

            return View(menuItem);
        }
        #endregion Public Actions

        #region Private Actions - only for logged in Users
        /// <summary>
        /// Only logged in useres can create
        /// </summary>
        [Authorize(Roles ="admin,cook")]
        // GET: MenuItems/Create
        public ActionResult Create()
        {
            var menuItem = new MenuItem();

            FillAssignableCategories(menuItem);

            return View(menuItem);
        }

        private void FillAssignableCategories(MenuItem menuItem)
        {
            foreach (var category in db.Categories.ToList())
            {
                menuItem.AssignableCategories.Add(new SelectListItem() { Text = category.Name, Value = category.Id.ToString() });
            }
        }

        // POST: MenuItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,cook")]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,CategoryId")] MenuItem menuItem)
        {
            var category = db.Categories.Find(menuItem.CategoryId);

            menuItem.Category = category;

            ModelState.Clear();
            var isValid = TryValidateModel(menuItem);

            if (ModelState.IsValid)
            {
                db.MenuItems.Add(menuItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            FillAssignableCategories(menuItem);

            return View(menuItem);
        }

        // GET: MenuItems/Edit/5
        [Authorize(Roles = "admin,cook")]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MenuItem menuItem = db.MenuItems.Find(id);

            if (menuItem == null)
            {
                return HttpNotFound();
            }

            FillAssignableCategories(menuItem);

            menuItem.CategoryId = menuItem.Category.Id;

            return View(menuItem);
        }

        // POST: MenuItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,cook")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,CategoryId")] MenuItem menuItem)
        {
            var category = db.Categories.Find(menuItem.CategoryId);

            var menuItemEntry = GetEntryAndLoadCategory(menuItem);

            menuItem.Category = category;

            ModelState.Clear();
            var isValid = TryValidateModel(menuItem);


            if (ModelState.IsValid)
            {
                menuItemEntry.State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menuItem);
        }

        private System.Data.Entity.Infrastructure.DbEntityEntry<MenuItem> GetEntryAndLoadCategory(MenuItem menuItem)
        {
            db.MenuItems.Attach(menuItem);

            var menuItemEntry = db.Entry(menuItem);

            menuItemEntry.Reference(x => x.Category)
                         .Load();
            return menuItemEntry;
        }

        // GET: MenuItems/Delete/5
        [Authorize(Roles = "admin,cook")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MenuItem menuItem = db.MenuItems.Find(id);

            if (menuItem == null)
            {
                return HttpNotFound();
            }

            GetEntryAndLoadCategory(menuItem);

            return View(menuItem);
        }

        // POST: MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,cook")]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuItem menuItem = db.MenuItems.Find(id);
            db.MenuItems.Remove(menuItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion Private Actions - only for logged in Users

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
