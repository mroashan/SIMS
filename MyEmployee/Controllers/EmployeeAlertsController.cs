using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyEmployee.Models;

namespace MyEmployee.Controllers
{
    public class EmployeeAlertsController : Controller
    {
        private readonly simsContext _context;

        public EmployeeAlertsController(simsContext context)
        {
            _context = context;
        }

        // GET: EmployeeAlerts
        public async Task<IActionResult> Index()
        {
            var simsContext = _context.EmployeeAlert.Include(e => e.Employee).OrderBy(e => e.Employee.Lastname).ThenBy(e => e.Employee.Firstname).ThenBy(a=>a.Alert);
            return View(await simsContext.ToListAsync());
        }

        //// GET: EmployeeAlerts/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var employeeAlert = await _context.EmployeeAlert
        //        .Include(e => e.Employee)
        //        .FirstOrDefaultAsync(m => m.AlertId == id);
        //    if (employeeAlert == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(employeeAlert);
        //}


        // GET: EmployeeAlerts/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Fullname");
            return View();
        }

        // POST: EmployeeAlerts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlertId,EmployeeId,Alert")] EmployeeAlert employeeAlert)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeAlert);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Firstname", employeeAlert.EmployeeId);
            return View(employeeAlert);
        }

        // GET: EmployeeAlerts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAlert = await _context.EmployeeAlert.FindAsync(id);
            if (employeeAlert == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Fullname", employeeAlert.EmployeeId);
            return View(employeeAlert);
        }

        // POST: EmployeeAlerts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlertId,EmployeeId,Alert")] EmployeeAlert employeeAlert)
        {
            if (id != employeeAlert.AlertId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeAlert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeAlertExists(employeeAlert.AlertId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Firstname", employeeAlert.EmployeeId);
            return View(employeeAlert);
        }

        // GET: EmployeeAlerts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAlert = await _context.EmployeeAlert
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.AlertId == id);
            if (employeeAlert == null)
            {
                return NotFound();
            }

            return View(employeeAlert);
        }

        // POST: EmployeeAlerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeAlert = await _context.EmployeeAlert.FindAsync(id);
            _context.EmployeeAlert.Remove(employeeAlert);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeAlertExists(int id)
        {
            return _context.EmployeeAlert.Any(e => e.AlertId == id);
        }
    }
}
