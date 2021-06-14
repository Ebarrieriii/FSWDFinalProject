using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FSWDFinalProject.DATA.EF;

namespace FSWDFinalProject.UI.MVC.Controllers
{
    public class LocationsController : Controller
    {
        private FinalEntities db = new FinalEntities();

        // GET: Locations
        public ActionResult Index()
        {
            return View(db.Locations.ToList());
        }

        #region AJAX CRUD
        //Delete
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AjaxDelete(int id)
        {
            Location location = db.Locations.Find(id);
            db.Locations.Remove(location);
            db.SaveChanges();

            string confirmMessage = string.Format("Deleted '{0}' from locations!", location.LocationName);
            return Json(new { id = id, message = confirmMessage });
        }

        //Details
        [HttpGet]
        public PartialViewResult LocationDetails(int id)
        {
            Location location = db.Locations.Find(id);
            return PartialView(location);
        }

        //Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AjaxCreate(Location location)
        {
            db.Locations.Add(location);
            db.SaveChanges();
            return Json(location);
        }

        //Edit - GET
        public PartialViewResult LocationEdit(int id)
        {
            Location location = db.Locations.Find(id);
            return PartialView(location);
        }

        //Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AjaxEdit(Location location)
        {
            db.Entry(location).State = EntityState.Modified;
            db.SaveChanges();
            return Json(location);
        }





        #endregion


        //// GET: Locations/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Location location = db.Locations.Find(id);
        //    if (location == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(location);
        //}

        //// GET: Locations/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Locations/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "LocationId,LocationName,Address,City,State,ZipCode,ReservationLimit")] Location location)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Locations.Add(location);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(location);
        //}

        //// GET: Locations/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Location location = db.Locations.Find(id);
        //    if (location == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(location);
        //}

        //// POST: Locations/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "LocationId,LocationName,Address,City,State,ZipCode,ReservationLimit")] Location location)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(location).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(location);
        //}

        //// GET: Locations/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Location location = db.Locations.Find(id);
        //    if (location == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(location);
        //}

        //// POST: Locations/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Location location = db.Locations.Find(id);
        //    db.Locations.Remove(location);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
