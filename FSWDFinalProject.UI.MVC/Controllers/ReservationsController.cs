using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FSWDFinalProject.DATA.EF;
using Microsoft.AspNet.Identity;

namespace FSWDFinalProject.UI.MVC.Controllers
{
    public class ReservationsController : Controller
    {
        private FinalEntities db = new FinalEntities();

        // GET: Reservations
        //Lock down the view so anonymous users can't reach it.
        [Authorize(Roles = "Admin, Customer, Employee")]
        public ActionResult Index()
        {
            //Show only the current users reservations.
            var currentUser = User.Identity.GetUserId();

            if (User.IsInRole("Customer"))
            {
                var reservation = db.Reservations.Where(c => c.Computer.OwnerId == currentUser).Include(c => c.Computer.UserDetail).Include(r => r.Computer).Include(r => r.Location).ToList();

                return View(reservation.ToList());
            }

            var reservations = db.Reservations.Include(r => r.Computer).Include(r => r.Location);
            return View(reservations.ToList());
           
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            //Show only the current users assets in the drop down when creating a new reservation.
            var currentUserId = User.Identity.GetUserId();

            ViewBag.ComputerId = new SelectList(db.Computers.Where(o => o.OwnerId == currentUserId), "ComputerId", "ComputerModel");
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationId,ComputerId,LocationId,ReservationDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var location = db.Locations.Where(l => l.LocationId == reservation.LocationId).FirstOrDefault();
                //Setting a reservation limit, if the limit is reached, send the user to an error page.
                if (location.Reservations.Where(r => r.ReservationDate == reservation.ReservationDate).ToList().Count >= location.ReservationLimit && !User.IsInRole("Admin"))
                {
                    return RedirectToAction("Error", "Shared");
                }
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            
            ViewBag.ComputerId = new SelectList(db.Computers, "ComputerId", "ComputerModel", reservation.ComputerId);
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName", reservation.LocationId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }

            //Getting only current users computers.
            var currentUserId = User.Identity.GetUserId();

            ViewBag.ComputerId = new SelectList(db.Computers.Where(o => o.OwnerId == currentUserId), "ComputerId", "ComputerModel");
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName", reservation.LocationId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationId,ComputerId,LocationId,ReservationDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var location = db.Locations.AsNoTracking().Where(l => l.LocationId == reservation.LocationId).FirstOrDefault();
               
                

                if (location.Reservations.Where(r => r.ReservationDate == reservation.ReservationDate).ToList().Count >= location.ReservationLimit && !User.IsInRole("Admin"))
                {
                    return RedirectToAction("Error", "Shared");
                }

                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ComputerId = new SelectList(db.Computers, "ComputerId", "ComputerModel", reservation.ComputerId);
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName", reservation.LocationId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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
