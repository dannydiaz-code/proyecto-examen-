using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaxiApp.Data;
using TaxiApp.Models;

namespace TaxiApp.Controllers
{
    public class TaxisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaxisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Taxis
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Taxis.Include(t => t.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Taxis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxis = await _context.Taxis
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxis == null)
            {
                return NotFound();
            }

            return View(taxis);
        }

        // GET: Taxis/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Taxis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Placa,Observacion,Eliminado,CreatedDate,UsuarioId")] Taxis taxis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taxis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", taxis.UsuarioId);
            return View(taxis);
        }

        // GET: Taxis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxis = await _context.Taxis.FindAsync(id);
            if (taxis == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", taxis.UsuarioId);
            return View(taxis);
        }

        // POST: Taxis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Placa,Observacion,Eliminado,CreatedDate,UsuarioId")] Taxis taxis)
        {
            if (id != taxis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taxis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaxisExists(taxis.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", taxis.UsuarioId);
            return View(taxis);
        }

        // GET: Taxis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxis = await _context.Taxis
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxis == null)
            {
                return NotFound();
            }

            return View(taxis);
        }

        // POST: Taxis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taxis = await _context.Taxis.FindAsync(id);
            _context.Taxis.Remove(taxis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaxisExists(int id)
        {
            return _context.Taxis.Any(e => e.Id == id);
        }
    }
}
