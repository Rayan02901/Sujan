using FlareTech.Data;
using FlareTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlareTech.Controllers
{
    // Controllers/SubscriptionController.cs
    public class SubscriptionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Subscriptions
        public async Task<IActionResult> Index()
        {
            var subscriptions = await _context.Subscriptions
                .Include(s => s.Customer)
                .Include(s => s.Plan)
                .OrderByDescending(s => s.StartDate)
                .ToListAsync();
            return View(subscriptions);
        }

        // GET: Subscriptions/Create
        public IActionResult Create()
        {
            ViewBag.Customers = new SelectList(_context.Customers, "Id", "CompanyName");
            ViewBag.Plans = new SelectList(_context.Plans, "PlanId", "PlanName");
            return View();
        }

        // POST: Subscriptions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subscription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subscription);
        }
    }
}
