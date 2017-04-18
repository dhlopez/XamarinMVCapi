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
    public class MovementsController : ApiController
    {
        private ArtHistoryEntities db = new ArtHistoryEntities();

        // GET: api/Movements
        public IQueryable<Movement> GetMovements()
        {
            return db.Movements;
        }

        // GET: api/Movements/5
        [ResponseType(typeof(Movement))]
        public async Task<IHttpActionResult> GetMovement(int id)
        {
            Movement movement = await db.Movements.FindAsync(id);
            if (movement == null)
            {
                return NotFound();
            }

            return Ok(movement);
        }

        // PUT: api/Movements/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMovement(int id, Movement movement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movement.ID)
            {
                return BadRequest();
            }

            db.Entry(movement).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Movements
        [ResponseType(typeof(Movement))]
        public async Task<IHttpActionResult> PostMovement(Movement movement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Movements.Add(movement);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = movement.ID }, movement);
        }

        // DELETE: api/Movements/5
        [ResponseType(typeof(Movement))]
        public async Task<IHttpActionResult> DeleteMovement(int id)
        {
            Movement movement = await db.Movements.FindAsync(id);
            if (movement == null)
            {
                return NotFound();
            }

            db.Movements.Remove(movement);
            await db.SaveChangesAsync();

            return Ok(movement);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovementExists(int id)
        {
            return db.Movements.Count(e => e.ID == id) > 0;
        }
    }
}