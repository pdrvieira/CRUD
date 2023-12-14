using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PDcase_test1;
using PDcase_test1.Models;

namespace PDcase_test1.Controllers
{
    public class PlanoDeSaudesController : Controller
    {
        private readonly ApplicationContext _context;

        public PlanoDeSaudesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: PlanoDeSaudes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlanosDeSaude.ToListAsync());
        }

        // GET: PlanoDeSaudes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planoDeSaude = await _context.PlanosDeSaude
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planoDeSaude == null)
            {
                return NotFound();
            }

            return View(planoDeSaude);
        }

        // GET: PlanoDeSaudes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlanoDeSaudes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] PlanoDeSaude planoDeSaude)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planoDeSaude);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(planoDeSaude);
        }

        // GET: PlanoDeSaudes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planoDeSaude = await _context.PlanosDeSaude.FindAsync(id);
            if (planoDeSaude == null)
            {
                return NotFound();
            }
            return View(planoDeSaude);
        }

        // POST: PlanoDeSaudes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] PlanoDeSaude planoDeSaude)
        {
            if (id != planoDeSaude.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planoDeSaude);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanoDeSaudeExists(planoDeSaude.Id))
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
            return View(planoDeSaude);
        }

        // GET: PlanoDeSaudes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planoDeSaude = await _context.PlanosDeSaude
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planoDeSaude == null)
            {
                return NotFound();
            }

            return View(planoDeSaude);
        }

        // POST: PlanoDeSaudes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planoDeSaude = await _context.PlanosDeSaude.FindAsync(id);
            _context.PlanosDeSaude.Remove(planoDeSaude);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanoDeSaudeExists(int id)
        {
            return _context.PlanosDeSaude.Any(e => e.Id == id);
        }
    }
}
