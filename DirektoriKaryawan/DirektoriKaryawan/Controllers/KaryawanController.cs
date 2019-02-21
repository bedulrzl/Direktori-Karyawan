using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DirektoriKaryawan.Data;
using DirektoriKaryawan.Models;

namespace DirektoriKaryawan.Controllers
{
    public class KaryawanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KaryawanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Karyawan
        public async Task<IActionResult> Index()
        {
            return View(await _context.Karyawan.ToListAsync());
        }

        // GET: Karyawan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karyawan = await _context.Karyawan
                .FirstOrDefaultAsync(m => m.karyawanID == id);
            if (karyawan == null)
            {
                return NotFound();
            }

            return View(karyawan);
        }

        // GET: Karyawan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Karyawan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("karyawanID,Nama,Jabatan")] Karyawan karyawan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(karyawan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(karyawan);
        }

        // GET: Karyawan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karyawan = await _context.Karyawan.FindAsync(id);
            if (karyawan == null)
            {
                return NotFound();
            }
            return View(karyawan);
        }

        // POST: Karyawan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("karyawanID,Nama,Jabatan")] Karyawan karyawan)
        {
            if (id != karyawan.karyawanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(karyawan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KaryawanExists(karyawan.karyawanID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(karyawan);
        }

        // GET: Karyawan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karyawan = await _context.Karyawan
                .FirstOrDefaultAsync(m => m.karyawanID == id);
            if (karyawan == null)
            {
                return NotFound();
            }

            return View(karyawan);
        }

        // POST: Karyawan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var karyawan = await _context.Karyawan.FindAsync(id);
            _context.Karyawan.Remove(karyawan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KaryawanExists(int id)
        {
            return _context.Karyawan.Any(e => e.karyawanID == id);
        }
    }
}
