using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FSWDFinalProject.DATA.EF;
using FSWDFinalProject.UI.MVC.Utilities;
using Microsoft.AspNet.Identity;

namespace FSWDFinalProject.UI.MVC.Controllers
{
    public class ComputersController : Controller
    {
        private FinalEntities db = new FinalEntities();

        // GET: Computers
        [Authorize(Roles = "Admin, Customer, Employee")]
        public ActionResult Index()
        {
            var currentUser = User.Identity.GetUserId();
            if (User.IsInRole("Customer"))
            {
            var computers = db.Computers.Where(c => c.OwnerId == currentUser).Include(c => c.UserDetail).ToList();

            return View(computers.ToList());
            }

            return View(db.Computers.ToList());

        }

        // GET: Computers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Computer computer = db.Computers.Find(id);
            if (computer == null)
            {
                return HttpNotFound();
            }
            return View(computer);
        }

        // GET: Computers/Create
        public ActionResult Create()
        {
            ViewBag.OwnerId = new SelectList(db.UserDetails, "UserId", "FirstName");
            return View();
        }

        // POST: Computers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComputerId,ComputerModel,OwnerId,ComputerPhoto,Notes,IsActive,DateAdded")] Computer computer, HttpPostedFileBase logo)
        {
                #region File Upload

                if (ModelState.IsValid)
                {
                    //use default img if none is provided
                    string file = "noImg.png";
                    if (logo != null)
                    {
                        file = logo.FileName;
                        string ext = file.Substring(file.LastIndexOf("."));
                        string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };
                        if (goodExts.Contains(ext.ToLower()) && logo.ContentLength <= 4194304)
                        {
                            file = Guid.NewGuid() + ext;

                            #region Resize Image
                            string savePath = Server.MapPath("~/Content/images/computers/");

                            Image convertedImage = Image.FromStream(logo.InputStream);

                            int maxImageSize = 500;

                            int maxThumbSize = 100;

                            ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);

                            #endregion
                        }
                        computer.ComputerPhoto = file;
                    }
                    #endregion
                    db.Computers.Add(computer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerId = new SelectList(db.UserDetails, "UserId", "FirstName", computer.OwnerId);
            return View(computer);
        }

        // GET: Computers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Computer computer = db.Computers.Find(id);
            if (computer == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerId = new SelectList(db.UserDetails, "UserId", "FirstName", computer.OwnerId);
            return View(computer);
        }

        // POST: Computers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComputerId,ComputerModel,OwnerId,ComputerPhoto,Notes,IsActive,DateAdded")] Computer computer, HttpPostedFileBase logo)
        {
            if (ModelState.IsValid)
            {
                string file = computer.ComputerPhoto;
                #region File Upload
                if (logo != null)
                {
                    file = logo.FileName;

                    string ext = file.Substring(file.LastIndexOf('.'));

                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    if (goodExts.Contains(ext.ToLower()) && logo.ContentLength <= 4194304)
                    {
                        file = Guid.NewGuid() + ext;
                        #region Resize Image
                        string savePath = Server.MapPath("~/Content/images/computers/");

                        Image convertedImage = Image.FromStream(logo.InputStream);

                        int maxImageSize = 500;

                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                        #endregion
                        if (computer.ComputerPhoto != null && computer.ComputerPhoto != "noImage.png")
                        {
                            string path = Server.MapPath("~/Content/Images/productImages/");
                            ImageUtility.Delete(path, computer.ComputerPhoto);
                        }
                        computer.ComputerPhoto = file;
                    }
                }
                #endregion
                db.Entry(computer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerId = new SelectList(db.UserDetails, "UserId", "FirstName", computer.OwnerId);
            return View(computer);
        }

        // GET: Computers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Computer computer = db.Computers.Find(id);
            if (computer == null)
            {
                return HttpNotFound();
            }
            return View(computer);
        }

        // POST: Computers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Computer computer = db.Computers.Find(id);

            //Delete the image file
            string path = Server.MapPath("~/Content/images/computers/");
            ImageUtility.Delete(path, computer.ComputerPhoto);

            db.Computers.Remove(computer);
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
