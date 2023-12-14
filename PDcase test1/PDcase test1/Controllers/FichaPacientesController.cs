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
    public class FichaPacientesController : Controller
    {
        private readonly ApplicationContext _context;

        public FichaPacientesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: FichaPacientes
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.FichaPacientes.Include(f => f.Especialidade).Include(f => f.PlanoDeSaude);
            return View(await applicationContext.ToListAsync());
        }

        // GET: FichaPacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fichaPaciente = await _context.FichaPacientes
                .Include(f => f.Especialidade)
                .Include(f => f.PlanoDeSaude)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fichaPaciente == null)
            {
                return NotFound();
            }

            return View(fichaPaciente);
        }

        // GET: FichaPacientes/Create
        public IActionResult Create()
        {
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "Id", "Nome");
            ViewData["PlanoDeSaudeId"] = new SelectList(_context.PlanosDeSaude, "Id", "Nome");
            return View();
        }

        // POST: FichaPacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomePaciente,NumeroCarteiraPlano,EspecialidadeId,PlanoDeSaudeId")] FichaPaciente fichaPaciente)
        {
            if (ModelState.IsValid)
            {
             var existFichaPaciente = _context.FichaPacientes
            .Include(fp => fp.PlanoDeSaude)
            .Include(fp => fp.Especialidade)
            .Where(fp => fp.PlanoDeSaudeId == fichaPaciente.PlanoDeSaudeId && fp.EspecialidadeId == fichaPaciente.EspecialidadeId)
            .FirstOrDefault();          

                if (existFichaPaciente != null)
                {
                    ModelState.AddModelError("", $"Esta especialidade {existFichaPaciente.Especialidade.Nome} já foi utilizada para o plano {existFichaPaciente.PlanoDeSaude.Nome}");
                    ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "Id", "Nome");
                    ViewData["PlanoDeSaudeId"] = new SelectList(_context.PlanosDeSaude, "Id", "Nome");
                    return View(fichaPaciente);
                }          

                _context.Add(fichaPaciente);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "Id", "Nome");
            ViewData["PlanoDeSaudeId"] = new SelectList(_context.PlanosDeSaude, "Id", "Nome");
            return View(fichaPaciente);
        }

        // GET: FichaPacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fichaPaciente = await _context.FichaPacientes.FindAsync(id);
            if (fichaPaciente == null)
            {
                return NotFound();
            }
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "Id", "Nome");
            ViewData["PlanoDeSaudeId"] = new SelectList(_context.PlanosDeSaude, "Id", "Nome");
            return View(fichaPaciente);
        }

        // POST: FichaPacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomePaciente,NumeroCarteiraPlano,EspecialidadeId,PlanoDeSaudeId")] FichaPaciente fichaPaciente)
        {
            if (id != fichaPaciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existFichaPaciente = _context.FichaPacientes
                    .Include(fp => fp.PlanoDeSaude)
                    .Include(fp => fp.Especialidade)
                    .Where(fp => fp.PlanoDeSaudeId == fichaPaciente.PlanoDeSaudeId && fp.EspecialidadeId == fichaPaciente.EspecialidadeId)
                    .FirstOrDefault();
                   
                    if (existFichaPaciente != null)
                    {
                        ModelState.AddModelError("", $"Esta especialidade {existFichaPaciente.Especialidade.Nome} já foi utilizada para o plano {existFichaPaciente.PlanoDeSaude.Nome}");
                        ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "Id", "Nome");
                        ViewData["PlanoDeSaudeId"] = new SelectList(_context.PlanosDeSaude, "Id", "Nome");
                        return View(fichaPaciente);
                    }
                    _context.Update(fichaPaciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FichaPacienteExists(fichaPaciente.Id))
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
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "Id", "Nome");
            ViewData["PlanoDeSaudeId"] = new SelectList(_context.PlanosDeSaude, "Id", "Nome");
            return View(fichaPaciente);
        }

        // GET: FichaPacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fichaPaciente = await _context.FichaPacientes
                .Include(f => f.Especialidade)
                .Include(f => f.PlanoDeSaude)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fichaPaciente == null)
            {
                return NotFound();
            }

            return View(fichaPaciente);
        }

        // POST: FichaPacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fichaPaciente = await _context.FichaPacientes.FindAsync(id);
            _context.FichaPacientes.Remove(fichaPaciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FichaPacienteExists(int id)
        {
            return _context.FichaPacientes.Any(e => e.Id == id);
        }
    }
}
