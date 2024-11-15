// Controllers/AdminController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlareTech.Models;
using FlareTech.Data;
using FlareTech.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlareTech.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var viewModel = new DashboardViewModel
            {
                TotalCustomers = await _context.Customers.CountAsync(),
                TotalActiveSubscriptions = await _context.Subscriptions
                    .Where(s => s.IsActive)
                    .CountAsync(),
                TotalRevenue = await _context.Invoices
                    .Where(i => i.PaymentStatus == PaymentStatus.Paid)
                    .SumAsync(i => i.Amount)
            };

            return View(viewModel);
        }

        // GET: Admin/AuditTrail
        public async Task<IActionResult> AuditTrail()
        {
            var audits = await _context.AuditTrails
                .Include(a => a.Admin)
                .OrderByDescending(a => a.ActionDate)
                .Take(100)
                .ToListAsync();

            return View(audits);
        }
    }

    

    

    
}