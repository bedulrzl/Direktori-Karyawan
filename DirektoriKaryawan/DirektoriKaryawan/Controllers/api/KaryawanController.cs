using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DirektoriKaryawan.Data;
using DirektoriKaryawan.Models;

namespace DirektoriKaryawan.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class KaryawanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KaryawanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Karyawan
        [HttpGet]
        public IEnumerable<Karyawan> GetKaryawan()
        {
            return _context.Karyawan;
        }

        // GET: api/Karyawan/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKaryawan([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var karyawan = await _context.Karyawan.FindAsync(id);

            if (karyawan == null)
            {
                return NotFound();
            }

            return Ok(karyawan);
        }

        // PUT: api/Karyawan/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKaryawan([FromRoute] int id, [FromBody] Karyawan karyawan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != karyawan.karyawanID)
            {
                return BadRequest();
            }

            _context.Entry(karyawan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KaryawanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Karyawan
        [HttpPost]
        public async Task<IActionResult> PostKaryawan([FromBody] Karyawan karyawan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Karyawan.Add(karyawan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKaryawan", new { id = karyawan.karyawanID }, karyawan);
        }

        // DELETE: api/Karyawan/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKaryawan([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var karyawan = await _context.Karyawan.FindAsync(id);
            if (karyawan == null)
            {
                return NotFound();
            }

            _context.Karyawan.Remove(karyawan);
            await _context.SaveChangesAsync();

            return Ok(karyawan);
        }

        private bool KaryawanExists(int id)
        {
            return _context.Karyawan.Any(e => e.karyawanID == id);
        }
    }
}