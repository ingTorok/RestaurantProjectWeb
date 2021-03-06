﻿using System;
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
    [Authorize(Roles ="admin,waiter")]
    public class TablesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tables
        public ActionResult Index()
        {
            return View(db.Tables.ToList());
        }

        // GET: Tables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.Tables.Find(id);
            FillAssignableLocations(table);

            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // GET: Tables/Create
        public ActionResult Create()
        {
            var table = new Table();
            FillAssignableLocations(table);

            return View(table);
        }

        // POST: Tables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LocationId")] Table table)
        {
            if (ModelState.IsValid)
            {
                var location = db.Locations.Find(table.LocationId);

                table.Location = location;
                db.Tables.Add(table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(table);
        }

        // GET: Tables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.Tables.Find(id);

            FillAssignableLocations(table);

            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        private void FillAssignableLocations(Table table)
        {
            table.AssignableLocations = db.Locations
                                          .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
                                          .ToList()
                                          ;
            if (table.Location!=null)
            {
                table.LocationId = table.Location.Id;
            }
        }

        // POST: Tables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LocationId")] Table table)
        {
            if (ModelState.IsValid)
            {
                //a version when the Location is NOT virtual
                //var location = db.Locations.Find(table.LocationId);
                //db.Tables.Attach(table);
                //var tableEntry = db.Entry(table);
                //tableEntry.Reference(x => x.Location)
                //    .Load();
                //table.Location = location;
                //db.Entry(table).State = EntityState.Modified;

                //a second version when the Location is not virtual
                //var tableToUpdate = db.Tables
                //                      .Include(x => x.Location)
                //                      .FirstOrDefault(x => x.Id == table.Id);


                //a version when the Location property IS virtual
                var location = db.Locations.Find(table.LocationId);
                //seach in the db the Table item, we will change in db and not in the memory
                var tableToUpdate = db.Tables.Find(table.Id);
                tableToUpdate.Name = table.Name;

                tableToUpdate.Location = location;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table);
        }

        // GET: Tables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.Tables.Find(id);
            FillAssignableLocations(table);

            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // POST: Tables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Table table = db.Tables.Find(id);
            db.Tables.Remove(table);
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
