using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Server.DAO;

namespace Server.Controllers
{
    public class NhaXuaBansController : ApiController
    {
        private DbContent _db = new DbContent();

        // GET: api/NhaXuaBans
        [HttpGet]
        public IEnumerable<NhaXuaBan> GetNhaXuaBans()
        {
            
            return _db.NhaXuaBans.ToList();
        }
        [HttpGet]
        public IEnumerable<NhaXuaBan> GetNxb()
        {

            return _db.NhaXuaBans.ToList();
        }
        // GET: api/NhaXuaBans/5
        [ResponseType(typeof(NhaXuaBan))]
        public IHttpActionResult GetNhaXuaBan(int id)
        {
            NhaXuaBan nhaXuaBan = _db.NhaXuaBans.Find(id);
            if (nhaXuaBan == null)
            {
                return NotFound();
            }
            return Ok(nhaXuaBan);
        }

        // PUT: api/NhaXuaBans/5
   
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNhaXuaBan(NhaXuaBan nhaXuaBan)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
          
                var existingNxb = _db.NhaXuaBans.FirstOrDefault(s => s.MaNXB == nhaXuaBan.MaNXB);
                if (existingNxb != null)
                {
                    existingNxb.TenNXB = nhaXuaBan.TenNXB;
                    existingNxb.DiaChi = nhaXuaBan.DiaChi;
                    existingNxb.DienThoai = nhaXuaBan.DienThoai;
                _db.SaveChanges();
            }    
                else
                {
                    return NotFound();
                }
            return Ok();  
    }
    // POST: api/NhaXuaBans
    [ResponseType(typeof(NhaXuaBan))]
        public IHttpActionResult PostNhaXuaBan(NhaXuaBan nhaXuaBan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.NhaXuaBans.Add(nhaXuaBan);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nhaXuaBan.MaNXB }, nhaXuaBan);
        }

        [HttpDelete]
        [ResponseType(typeof(NhaXuaBan))]
        public IHttpActionResult DeleteNhaXuaBan(int id)
        {
            NhaXuaBan nhaXuaBan = _db.NhaXuaBans.Find(id);
            if (nhaXuaBan == null)
            {
                return NotFound();
            }

            _db.NhaXuaBans.Remove(nhaXuaBan);
            _db.SaveChanges();

            return Ok(nhaXuaBan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NhaXuaBanExists(int id)
        {
            return _db.NhaXuaBans.Count(e => e.MaNXB == id) > 0;
        }
    }
}