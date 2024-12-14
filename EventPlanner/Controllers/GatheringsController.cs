using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Data;
using EventPlanner.Models;
using EventPlanner.Viewmodels;

namespace EventPlanner.Controllers
{
    public class GatheringsController : Controller
    {
        private readonly EventplannerContext _context;

        public GatheringsController(EventplannerContext context)
        {
            _context = context;
        }

        // GET: Gatherings
        public async Task<IActionResult> Index()
        {
            var eventplannerContext = _context.Gatherings.Include(g => g.GatheringCategory);
            return View(await eventplannerContext.ToListAsync());
        }

        // GET: Gatherings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gathering = await _context.Gatherings
                .Include(g => g.GatheringCategory)
                .FirstOrDefaultAsync(m => m.GatheringId == id);
            if (gathering == null)
            {
                return NotFound();
            }

            return View(gathering);
        }

        // GET: Gatherings/Create
        public IActionResult Create()
        {
            var categories = _context.Gatherings.ToList();

            var viewmodel = new GatheringModel
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.GatheringCategory.CategoryName
                }).ToList()
            };
            return View(viewmodel);
        }

        // POST: Gatherings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GatheringModel model)
        {
            if (ModelState.IsValid)
            {
                var newGathering = new Gathering
                {
                    GatheringName = model.Gathering.GatheringName,
                    GatheringDesc = model.Gathering.GatheringDesc,
                    MinorsAllowed = model.Gathering.MinorsAllowed,
                    GatheringStart = model.Gathering.GatheringStart,
                    GatheringEnd = model.Gathering.GatheringEnd,
                    CategoryId = model.SelectedCategoryId
                };

                _context.Gatherings.Add(newGathering);
                await _context.SaveChangesAsync();
                
                return(RedirectToAction("Index"));
            }
            return View(model);
        }

        // GET: Gatherings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gathering = await _context.Gatherings.FindAsync(id);
            if (gathering == null)
            {
                return NotFound();
            }
            ViewData["GatheringId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", gathering.GatheringId);
            return View(gathering);
        }

        // POST: Gatherings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GatheringId,GatheringName,GatheringDesc,MinorsAllowed,GatheringStart,GatheringEnd")] Gathering gathering)
        {
            if (id != gathering.GatheringId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gathering);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GatheringExists(gathering.GatheringId))
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
            ViewData["GatheringId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", gathering.GatheringId);
            return View(gathering);
        }

        // GET: Gatherings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gathering = await _context.Gatherings
                .Include(g => g.GatheringCategory)
                .FirstOrDefaultAsync(m => m.GatheringId == id);
            if (gathering == null)
            {
                return NotFound();
            }

            return View(gathering);
        }

        // POST: Gatherings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gathering = await _context.Gatherings.FindAsync(id);
            if (gathering != null)
            {
                _context.Gatherings.Remove(gathering);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GatheringExists(int id)
        {
            return _context.Gatherings.Any(e => e.GatheringId == id);
        }
    }
}
