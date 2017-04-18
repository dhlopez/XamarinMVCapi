using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test3_ArtHistory.DAL;
using Test3_ArtHistory.Models;

namespace Test3_ArtHistoryWebAPI.Controllers
{
    public class PaintingsMVCController : Controller
    {
        private ArtHistoryEntities db = new ArtHistoryEntities();

        // GET: PaintingsMVC
        public ActionResult Index()
        {
            var paintings = db.Paintings.Include(p => p.Artist).Include(p => p.Movement);
            return View(paintings.ToList());
        }

        // GET: PaintingsMVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painting painting = db.Paintings.Find(id);
            if (painting == null)
            {
                return HttpNotFound();
            }
            return View(painting);
        }

        // GET: PaintingsMVC/Create
        public ActionResult Create()
        {
            PopulateDropDownList();
            return View();
        }

        // POST: PaintingsMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Dated,imageContent,imageMimeType,imageFileName,MovementID,ArtistID")] Painting painting)
        {
            if (ModelState.IsValid)
            {
                foreach (string fName in Request.Files)
                {
                    HttpPostedFileBase f = Request.Files[fName];
                    string mimeType = f.ContentType;
                    int fileLength = f.ContentLength;
                    if (!(mimeType == "" || fileLength == 0))//Looks like we have a file!!!
                    {
                        string fileName = Path.GetFileName(f.FileName);
                        Stream fileStream = Request.Files[fName].InputStream;
                        byte[] fileData = new byte[fileLength];
                        fileStream.Read(fileData, 0, fileLength);

                        if (mimeType.Contains("image") && fName == "FileUpImage")
                        {
                            painting.imageContent = fileData;
                            painting.imageMimeType = mimeType;
                            painting.imageFileName = fileName;
                        }
                    }
                }
                db.Paintings.Add(painting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            PopulateDropDownList(painting.ArtistID, painting.MovementID);
            return View(painting);
        }

        // GET: PaintingsMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painting painting = db.Paintings.Find(id);
            if (painting == null)
            {
                return HttpNotFound();
            }
            PopulateDropDownList(painting.ArtistID, painting.MovementID);
            return View(painting);
        }

        // POST: PaintingsMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Dated,imageContent,imageMimeType,imageFileName,MovementID,ArtistID")] Painting painting, string chkRemoveImage)
        {
            if (ModelState.IsValid)
            {
                if (chkRemoveImage != null)//Remove the image
                {
                    painting.imageContent = null;
                    painting.imageMimeType = null;
                    painting.imageFileName = null;
                }
                foreach (string fName in Request.Files)
                {
                    HttpPostedFileBase f = Request.Files[fName];
                    string mimeType = f.ContentType;
                    int fileLength = f.ContentLength;
                    if (!(mimeType == "" || fileLength == 0))//Looks like we have a file!!!
                    {
                        string fileName = Path.GetFileName(f.FileName);
                        Stream fileStream = Request.Files[fName].InputStream;
                        byte[] fileData = new byte[fileLength];
                        fileStream.Read(fileData, 0, fileLength);

                        if (mimeType.Contains("image") && fName == "FileUpImage")
                        {
                            painting.imageContent = fileData;
                            painting.imageMimeType = mimeType;
                            painting.imageFileName = fileName;
                        }
                    }
                }
                db.Entry(painting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateDropDownList(painting.ArtistID, painting.MovementID);
            return View(painting);
        }

        // GET: PaintingsMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painting painting = db.Paintings.Find(id);
            if (painting == null)
            {
                return HttpNotFound();
            }
            return View(painting);
        }

        // POST: PaintingsMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Painting painting = db.Paintings.Find(id);
            db.Paintings.Remove(painting);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void PopulateDropDownList(object selectedArtist = null, object selectedMovement = null)
        {
            ViewBag.ArtistID = new SelectList(db.Artists.OrderBy(a => a.Name), "ID", "Name", selectedArtist);
            ViewBag.MovementID = new SelectList(db.Movements, "ID", "Name", selectedMovement);
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
