using FlareTech.Data;
using FlareTech.Models.Enums;
using Microsoft.EntityFrameworkCore;
using FlareTech.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlareTech.Controllers
{
    // Controllers/InvoiceController.cs
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var invoices = await _context.Invoices
                .Include(i => i.Subscription)
                .ThenInclude(s => s.Customer)
                .OrderByDescending(i => i.InvoiceDate)
                .ToListAsync();
            return View(invoices);
        }

        // GET: Invoices/Generate/5 (for subscription)
        public async Task<IActionResult> Generate(int? id)
        {
            if (id == null)
                return NotFound();

            var subscription = await _context.Subscriptions
                .Include(s => s.Customer)
                .Include(s => s.Plan)
                .FirstOrDefaultAsync(s => s.SubscriptionId == id);

            if (subscription == null)
                return NotFound();

            var invoice = new Invoice
            {
                SubscriptionId = subscription.SubscriptionId,
                Amount = subscription.MonthlyFee,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                PaymentStatus = PaymentStatus.Pending
            };

            return View(invoice);
        }

        // POST: Invoices/Generate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Generate(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }
    }
}
