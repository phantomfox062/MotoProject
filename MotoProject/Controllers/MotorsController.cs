using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotoProject.Data;
using MotoProject.Models;

namespace MotoProject.Controllers
{
    public class MotorsController : Controller
    {
        private readonly MotoProjectContext _context;

        public MotorsController(MotoProjectContext context)
        {
            _context = context;
        }

        // GET: Motors
        public async Task<IActionResult> Index()
        {
              return _context.Motors != null ? 
                          View(await _context.Motors.ToListAsync()) :
                          Problem("Entity set 'MotoProjectContext.Motors'  is null.");
        }

        // GET: Motors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Motors == null)
            {
                return NotFound();
            }

            var motors = await _context.Motors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motors == null)
            {
                return NotFound();
            }

            return View(motors);
        }

        // GET: Motors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Motors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,brand,model,date,hp,color")] Motors motors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(motors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(motors);
        }

        // GET: Motors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Motors == null)
            {
                return NotFound();
            }

            var motors = await _context.Motors.FindAsync(id);
            if (motors == null)
            {
                return NotFound();
            }
            return View(motors);
        }

        // POST: Motors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,brand,model,date,hp,color")] Motors motors)
        {
            if (id != motors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(motors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotorsExists(motors.Id))
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
            return View(motors);
        }

        // GET: Motors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Motors == null)
            {
                return NotFound();
            }

            var motors = await _context.Motors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motors == null)
            {
                return NotFound();
            }

            return View(motors);
        }

        // POST: Motors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Motors == null)
            {
                return Problem("Entity set 'MotoProjectContext.Motors'  is null.");
            }
            var motors = await _context.Motors.FindAsync(id);
            if (motors != null)
            {
                _context.Motors.Remove(motors);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MotorsExists(int id)
        {
          return (_context.Motors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
