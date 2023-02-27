using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using appjoke.Data;
using appjoke.Models;
using Microsoft.AspNetCore.Authorization;

namespace appjoke.Controllers
{
    public class jokesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public jokesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: jokescontroller
        public async Task<IActionResult> Index()
        {
              return View(await _context.joke.ToListAsync());
        }
        // GET: Jokes/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View("ShowSearchForm");
        }
        // PoST: Jokes/ShowSearch
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.joke.Where(j=> j.JokeQuestion.Contains
            (SearchPhrase) ).ToListAsync());
        }
        // GET: jokes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.joke == null)
            {
                return NotFound();
            }

            var joke = await _context.joke
                .FirstOrDefaultAsync(m => m.ID == id);
            if (joke == null)
            {
                return NotFound();
            }

            return View(joke);
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
        public async Task<IActionResult> Create([Bind("ID,JokeQuestion,JokeAnswer")] joke joke)
        {
            if (ModelState.IsValid)
            {
                _context.Add(joke);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(joke);
        }

        // GET: jokes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.joke == null)
            {
                return NotFound();
            }

            var joke = await _context.joke.FindAsync(id);
            if (joke == null)
            {
                return NotFound();
            }
            return View(joke);
        }

        // POST: jokes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,JokeQuestion,JokeAnswer")] joke joke)
        {
            if (id != joke.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(joke);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!jokeExists(joke.ID))
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
            return View(joke);
        }

        // GET: jokes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.joke == null)
            {
                return NotFound();
            }

            var joke = await _context.joke
                .FirstOrDefaultAsync(m => m.ID == id);
            if (joke == null)
            {
                return NotFound();
            }

            return View(joke);
        }

        // POST: jokes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.joke == null)
            {
                return Problem("Entity set 'ApplicationDbContext.joke'  is null.");
            }
            var joke = await _context.joke.FindAsync(id);
            if (joke != null)
            {
                _context.joke.Remove(joke);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool jokeExists(int id)
        {
          return _context.joke.Any(e => e.ID == id);
        }
    }
}
