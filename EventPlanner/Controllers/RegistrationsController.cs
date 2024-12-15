using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Data;
using EventPlanner.Models;
using System.Security.Claims;

namespace EventPlanner.Controllers
{
    public class RegistrationsController : Controller
    {
        private readonly EventplannerContext _context;

        public RegistrationsController(EventplannerContext context)
        {
            _context = context;
        }

        // GET: Registrations
        public async Task<IActionResult> Index()
        {
            var eventplannerContext = _context.Registrations.Include(r => r.RegistrationGathering).Include(r => r.RegistrationOwner);
            return View(await eventplannerContext.ToListAsync());
        }

        // GET: Registrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.RegistrationGathering)
                .Include(r => r.RegistrationOwner)
                .FirstOrDefaultAsync(m => m.RegistrationId == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // GET: Registrations/Create
        public IActionResult Create(int gatheringd)
        {
            ViewData["GatheringId"] = new SelectList(_context.Gatherings, "GatheringId", "GatheringName");
            ViewData["UserId"] = GetCurrentUserId();
            return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistrationId,UserId,GatheringId")] Registration registration)
        {
            int userId = GetCurrentUserId();
            int gatheringId = GetCurrentGatheringId();

            var userExists = await _context.Users.AnyAsync(u => u.UserId == userId);
            if (!userExists)
            {
                TempData["ErrorMessage"] = "User not found. Please log in again.";
                return RedirectToAction("Details", "Gatherings", new { id = gatheringId });
            }

            var existingRegistration = await _context.Registrations
                .FirstOrDefaultAsync(r => r.UserId == userId && r.GatheringId == gatheringId);

            if (existingRegistration != null)
            {
                return RedirectToAction("Details", "Gatherings", new { id = gatheringId });
            }

            var newregistration = new Registration
            {
                UserId = userId,
                GatheringId = gatheringId,
                RegistrationCreated = DateTime.Now
            };

            _context.Add(newregistration);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Gatherings", new { id = gatheringId });
        }

        // GET: Registrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            ViewData["GatheringId"] = new SelectList(_context.Gatherings, "GatheringId", "GatheringName", registration.GatheringId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserName", registration.UserId);
            return View(registration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegistrationId,UserId,GatheringId")] Registration registration)
        {
            if (id != registration.RegistrationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.RegistrationId))
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
            ViewData["GatheringId"] = new SelectList(_context.Gatherings, "GatheringId", "GatheringName", registration.GatheringId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserName", registration.UserId);
            return View(registration);
        }

        // GET: Registrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.RegistrationGathering)
                .Include(r => r.RegistrationOwner)
                .FirstOrDefaultAsync(m => m.RegistrationId == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registrations.Any(e => e.RegistrationId == id);
        }

        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        private int GetCurrentGatheringId()
        {

            if (Request.Form.ContainsKey("gatheringId"))
            {
                return int.Parse(Request.Form["gatheringId"]);
            }

            throw new InvalidOperationException("GatheringId not found.");
        }
    }
}
