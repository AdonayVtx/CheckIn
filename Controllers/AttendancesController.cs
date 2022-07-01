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
    public class AttendancesController : Controller
    {
        private readonly DatabaseContext _context;

        public AttendancesController(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(DateTime? Start, DateTime? End)
        {
        
            var data = from d in _context.Attendances.Include(r => r.Employee)
                       select d;
            if (Start != null && End != null)
            {
                End = End.Value.AddDays(1);
                data = data.Where(e => e.Entrance >= Start && e.Exit <= End);
                return View(await data.ToListAsync());
            }
            else
            {
                var databaseContext = _context.Attendances.Include( r => r.Employee);
                return View(await databaseContext.ToListAsync());

            }
        }

        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                var employee = _context.Employees.FirstOrDefault(e => e.Id == attendance.EmployeeId);
                if (employee != null)
                {
                    var list = _context.Attendances.FirstOrDefault(r => r.EmployeeId == attendance.EmployeeId && r.Exit == null);
                    if (list != null)
                    {
                        list.Exit = DateTime.Now;
                        _context.Update(list);
                    }
                    else
                    {
                        var control = new Attendance();
                        control.Entrance = DateTime.Now;
                        control.EmployeeId = employee.Id;
                        _context.Add(control);
                    }
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit( )
        {
            var databaseContext = _context.Attendances.Include(a => a.Employee);
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Attendances == null)
            {
                return Problem("Entity set 'DatabaseContext.Attendances'  is null.");
            }
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.Id == id);
        }
    }
}
