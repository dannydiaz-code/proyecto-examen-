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
    public class RegistroDiariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistroDiariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RegistroDiarios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RegistrosDiarios.Include(r => r.Taxi).Include(r => r.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RegistroDiarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroDiario = await _context.RegistrosDiarios
                .Include(r => r.Taxi)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroDiario == null)
            {
                return NotFound();
            }

            return View(registroDiario);
        }

        // GET: RegistroDiarios/Create
        public IActionResult Create()
        {
            ViewData["TaxiId"] = new SelectList(_context.Taxis, "Id", "Id");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: RegistroDiarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TotalDia,Combustible,PagoBase,PagoConductor,PagoDueno,Gastos,Observacion,Eliminado,CreatedDate,Fecha,UsuarioId,TaxiId")] RegistroDiario registroDiario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registroDiario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaxiId"] = new SelectList(_context.Taxis, "Id", "Id", registroDiario.TaxiId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", registroDiario.UsuarioId);
            return View(registroDiario);
        }

        // GET: RegistroDiarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroDiario = await _context.RegistrosDiarios.FindAsync(id);
            if (registroDiario == null)
            {
                return NotFound();
            }
            ViewData["TaxiId"] = new SelectList(_context.Taxis, "Id", "Id", registroDiario.TaxiId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", registroDiario.UsuarioId);
            return View(registroDiario);
        }

        // POST: RegistroDiarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TotalDia,Combustible,PagoBase,PagoConductor,PagoDueno,Gastos,Observacion,Eliminado,CreatedDate,Fecha,UsuarioId,TaxiId")] RegistroDiario registroDiario)
        {
            if (id != registroDiario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registroDiario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroDiarioExists(registroDiario.Id))
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
            ViewData["TaxiId"] = new SelectList(_context.Taxis, "Id", "Id", registroDiario.TaxiId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", registroDiario.UsuarioId);
            return View(registroDiario);
        }

        // GET: RegistroDiarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroDiario = await _context.RegistrosDiarios
                .Include(r => r.Taxi)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroDiario == null)
            {
                return NotFound();
            }

            return View(registroDiario);
        }

        // POST: RegistroDiarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registroDiario = await _context.RegistrosDiarios.FindAsync(id);
            _context.RegistrosDiarios.Remove(registroDiario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroDiarioExists(int id)
        {
            return _context.RegistrosDiarios.Any(e => e.Id == id);
        }
    }
}
