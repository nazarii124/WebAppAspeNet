using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppAspeNet.Data;
using WebAppAspeNet.Models;

namespace WebAppAspeNet.Controllers
{
    public class jokesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public jokesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: jokes
        public async Task<IActionResult> Index()
        {
              return _context.jokes != null ? 
                          View(await _context.jokes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.jokes'  is null.");
        }
        // GET: jokes searchform
        public async Task<IActionResult> ShowSearchForm()
        {
            //return _context.jokes != null ?
              //          View(await _context.jokes.ToListAsync()) :
                //        Problem("Entity set 'ApplicationDbContext.jokes'  is null.");
            return View();  
         }
        // Post: Searchform Resultados
        public async Task<IActionResult>  ShowFormResultados(string SearchFrase)
        {
            return _context.jokes != null ?
                    View("Index",await _context.jokes.Where(j => j.JokePergunta.Contains(SearchFrase) ).ToListAsync()) :
                    Problem("Entity set 'ApplicationDbContext.jokes'  is null.");
            
        }

        // GET: jokes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.jokes == null)
            {
                return NotFound();
            }

            var jokes = await _context.jokes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jokes == null)
            {
                return NotFound();
            }

            return View(jokes);
        }

        // GET: jokes/Create

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: jokes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JokePergunta,JokeResposta, Autor")] jokes jokes)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                jokes.Autor = userId;
                _context.Add(jokes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jokes);
        }

        // GET: jokes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.jokes == null)
            {
                return NotFound();
            }

            var jokes = await _context.jokes.FindAsync(id);
            if (jokes == null)
            {
                return NotFound();
            }
            return View(jokes);
        }

        // POST: jokes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JokePergunta,JokeResposta")] jokes jokes)
        {
            if (id != jokes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jokes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!jokesExists(jokes.Id))
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
            return View(jokes);
        }

        // GET: jokes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.jokes == null)
            {
                return NotFound();
            }

            var jokes = await _context.jokes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jokes == null)
            {
                return NotFound();
            }

            return View(jokes);
        }

        // POST: jokes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.jokes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.jokes'  is null.");
            }
            var jokes = await _context.jokes.FindAsync(id);
            if (jokes != null)
            {
                _context.jokes.Remove(jokes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool jokesExists(int id)
        {
          return (_context.jokes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
