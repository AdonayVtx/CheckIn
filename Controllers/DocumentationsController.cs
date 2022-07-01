using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CheckIn.Models;

namespace CheckIn.Controllers
{
    public class DocumentationsController : Controller
    {
        private readonly DatabaseContext _context;

        public DocumentationsController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Documentations.Include(d => d.Employee);
            return View(await databaseContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Documentations == null)
            {
                return NotFound();
            }

            var documentation = await _context.Documentations
                .Include(d => d.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentation == null)
            {
                return NotFound();
            }

            return View(documentation);
        }

        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Documentation documentation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documentation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", documentation.EmployeeId);
            return View(documentation);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Documentations == null)
            {
                return NotFound();
            }

            var documentation = await _context.Documentations.FindAsync(id);
            if (documentation == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", documentation.EmployeeId);
            return View(documentation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Documentation documentation)
        {
            if (id != documentation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentationExists(documentation.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", documentation.EmployeeId);
            return View(documentation);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Documentations == null)
            {
                return NotFound();
            }

            var documentation = await _context.Documentations
                .Include(d => d.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentation == null)
            {
                return NotFound();
            }

            return View(documentation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Documentations == null)
            {
                return Problem("Entity set 'DatabaseContext.Documentations'  is null.");
            }
            var documentation = await _context.Documentations.FindAsync(id);
            if (documentation != null)
            {
                _context.Documentations.Remove(documentation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentationExists(int id)
        {
          return _context.Documentations.Any(e => e.Id == id);
        }
    }
}
