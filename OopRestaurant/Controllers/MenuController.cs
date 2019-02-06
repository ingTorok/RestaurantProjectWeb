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
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Menu
        public ActionResult Index()
        {
            //var model = db.MenuItems
            //            //.Include("Category") - ha a tulajdonság nevét ismerjük, viszont nem dinamikus és csak futási idöben derül ki hogy pl elütés van, vagy a név megváltozott
            //              .Include(mi=>mi.Category)
            //              .OrderBy(mi => mi.Category.Name)
            //              .ToList();
            //return View(model);

            var model = db.Categories
                          .Include(c => c.MenuItems)
                          .OrderBy(c => c.Name)
                          .ToList();

            return View(model);
        }

        // GET: Menu/Details/5
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
            return View(menuItem);
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
