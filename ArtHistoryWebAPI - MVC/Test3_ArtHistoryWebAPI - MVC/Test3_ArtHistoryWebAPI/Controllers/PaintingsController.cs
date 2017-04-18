using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Test3_ArtHistory.DAL;
using Test3_ArtHistory.Models;

namespace Test3_ArtHistoryWebAPI.Controllers
{
    public class PaintingsController : ApiController
    {
        private ArtHistoryEntities db = new ArtHistoryEntities();

        // GET: api/Paintings
        public IQueryable<Painting> GetPaintings()
        {
            return db.Paintings.Include(p=>p.Artist).Include(p=>p.Movement);
        }

        public IQueryable<Painting> GetPaintingsByMovement(int MovementID)
        {
            var pts = db.Paintings.Include(e => e.Movement).Include(p => p.Artist)
                .Where(e => e.MovementID == MovementID);
            return pts;
        }

        public IQueryable<Painting> GetPaintingsByName(string searchName)
        {
            var pts = db.Paintings.Include(e => e.Movement).Include(p => p.Artist);
            if (!String.IsNullOrEmpty(searchName))
            {
                pts = pts.Where(p => p.Name.ToUpper().Contains(searchName.ToUpper()));
            }

            return pts;
        }

        // GET: api/Paintings/5
        [ResponseType(typeof(Painting))]
        public async Task<IHttpActionResult> GetPainting(int id)
        {
            Painting painting = await db.Paintings
                .Include(p => p.Artist)
                .Include(p => p.Movement)
                .Where(p=>p.ID==id).SingleOrDefaultAsync();
            if (painting == null)
            {
                return NotFound();
            }

            return Ok(painting);
        }

        // PUT: api/Paintings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPainting(int id, Painting painting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != painting.ID)
            {
                return BadRequest();
            }

            db.Entry(painting).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaintingExists(id))
                {
                    return BadRequest("Concurrency Error: Painting has been Removed.");
                }
                else
                {
                    return BadRequest("Concurrency Error: Painting has been updated by another user.  Cancel and try editing the record again.");
                }
            }

            
        }

        // POST: api/Paintings
        [ResponseType(typeof(Painting))]
        public async Task<IHttpActionResult> PostPainting(Painting painting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Paintings.Add(painting);
            try
            {
                await db.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { id = painting.ID }, painting);
            }
            catch (Exception)
            {
                return BadRequest("Unable to save changes to the database. Try again, and if the problem persists see your system administrator.");
            }
            
        }

        // DELETE: api/Paintings/5
        [ResponseType(typeof(Painting))]
        public async Task<IHttpActionResult> DeletePainting(int id)
        {
            Painting painting = await db.Paintings.FindAsync(id);
            if (painting == null)
            {
                return BadRequest("Delete Error: Painting has already been Removed.");
            }

            db.Paintings.Remove(painting);
            await db.SaveChangesAsync();

            return Ok(painting);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaintingExists(int id)
        {
            return db.Paintings.Count(e => e.ID == id) > 0;
        }
    }
}