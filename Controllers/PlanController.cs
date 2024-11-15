using FlareTech.Data;
using FlareTech.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlareTech.Controllers
{
    
    public class PlanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Plans
        public async Task<IActionResult> Index()
        {
            var plans = await _context.Plans
                .Include(p => p.PlanFeatures)
                .ThenInclude(pf => pf.Feature)
                .ToListAsync();
            return View(plans);
        }

        // GET: Plans/Create
        public IActionResult Create()
        {
            ViewBag.Features = new SelectList(_context.Features, "FeatureId", "FeatureName");
            return View();
        }

        // POST: Plans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Plan plan, int[] selectedFeatures)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plan);
                await _context.SaveChangesAsync();

                foreach (var featureId in selectedFeatures)
                {
                    var planFeature = new PlanFeature
                    {
                        PlanId = plan.PlanId,
                        FeatureId = featureId,
                        IsIncluded = true
                    };
                    _context.PlanFeatures.Add(planFeature);
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        // GET: Plans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var plan = await _context.Plans
                .Include(p => p.PlanFeatures)
                .FirstOrDefaultAsync(p => p.PlanId == id);

            if (plan == null)
                return NotFound();

            ViewBag.Features = new SelectList(_context.Features, "FeatureId", "FeatureName");
            return View(plan);
        }

        // POST: Plans/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Plan plan, int[] selectedFeatures)
        {
            if (id != plan.PlanId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plan);
                    var existingFeatures = _context.PlanFeatures.Where(pf => pf.PlanId == id);
                    _context.PlanFeatures.RemoveRange(existingFeatures);

                    foreach (var featureId in selectedFeatures)
                    {
                        var planFeature = new PlanFeature
                        {
                            PlanId = plan.PlanId,
                            FeatureId = featureId,
                            IsIncluded = true
                        };
                        _context.PlanFeatures.Add(planFeature);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanExists(plan.PlanId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        private bool PlanExists(int id)
        {
            return _context.Plans.Any(e => e.PlanId == id);
        }
    }
}
