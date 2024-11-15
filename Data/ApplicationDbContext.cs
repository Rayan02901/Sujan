using FlareTech.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlareTech.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<PlanFeature> PlanFeatures { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<AuditTrail> AuditTrails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Admin Configuration
            modelBuilder.Entity<Admin>()
                .HasMany(a => a.AuditTrails)
                .WithOne(at => at.Admin)
                .HasForeignKey(at => at.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            // Customer Configuration
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Subscriptions)
                .WithOne(s => s.Customer)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Plan Configuration
            modelBuilder.Entity<Plan>()
                .HasMany(p => p.PlanFeatures)
                .WithOne(pf => pf.Plan)
                .HasForeignKey(pf => pf.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Plan>()
                .HasMany(p => p.Subscriptions)
                .WithOne(s => s.Plan)
                .HasForeignKey(s => s.PlanId)
                .OnDelete(DeleteBehavior.Restrict);

            // Feature Configuration
            modelBuilder.Entity<Feature>()
                .HasMany(f => f.PlanFeatures)
                .WithOne(pf => pf.Feature)
                .HasForeignKey(pf => pf.FeatureId)
                .OnDelete(DeleteBehavior.Cascade);

            // PlanFeature Configuration
            modelBuilder.Entity<PlanFeature>()
                .HasKey(pf => pf.PlanFeatureId);

            modelBuilder.Entity<PlanFeature>()
                .HasIndex(pf => new { pf.PlanId, pf.FeatureId })
                .IsUnique();

            // Subscription Configuration
            modelBuilder.Entity<Subscription>()
                .HasMany(s => s.Invoices)
                .WithOne(i => i.Subscription)
                .HasForeignKey(i => i.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            // BasePerson Configuration
            modelBuilder.Entity<BasePerson>()
                .UseTpcMappingStrategy();

            // Property Configurations
            modelBuilder.Entity<Admin>()
                .Property(a => a.Username)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Customer>()
                .Property(c => c.CompanyName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Plan>()
                .Property(p => p.BasePrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Feature>()
                .Property(f => f.AdditionalCost)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Subscription>()
                .Property(s => s.MonthlyFee)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Invoice>()
                .Property(i => i.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Invoice>()
                .Property(i => i.DiscountAmount)
                .HasPrecision(18, 2);

            // Seed Features
            modelBuilder.Entity<Feature>().HasData(
                new Feature
                {
                    FeatureId = 1,
                    FeatureName = "Custom Domain Integration",
                    Description = "Connect and use your own domain name with our platform",
                    AdditionalCost = 15.00m
                },
                new Feature
                {
                    FeatureId = 2,
                    FeatureName = "Advanced Analytics",
                    Description = "Detailed insights and reporting with custom dashboards",
                    AdditionalCost = 25.00m
                },
                new Feature
                {
                    FeatureId = 3,
                    FeatureName = "Priority Support",
                    Description = "24/7 dedicated support with 2-hour response time guarantee",
                    AdditionalCost = 50.00m
                },
                new Feature
                {
                    FeatureId = 4,
                    FeatureName = "API Access",
                    Description = "Full REST API access with comprehensive documentation",
                    AdditionalCost = 30.00m
                },
                new Feature
                {
                    FeatureId = 5,
                    FeatureName = "Automated Backups",
                    Description = "Daily automated backups with 30-day retention",
                    AdditionalCost = 20.00m
                },
                new Feature
                {
                    FeatureId = 6,
                    FeatureName = "Team Collaboration",
                    Description = "Multiple user accounts with role-based access control",
                    AdditionalCost = 10.00m
                },
                new Feature
                {
                    FeatureId = 7,
                    FeatureName = "White Labeling",
                    Description = "Remove our branding and use your own",
                    AdditionalCost = 40.00m
                },
                new Feature
                {
                    FeatureId = 8,
                    FeatureName = "Advanced Security",
                    Description = "Enhanced security features including 2FA and IP whitelisting",
                    AdditionalCost = 35.00m
                },
                new Feature
                {
                    FeatureId = 9,
                    FeatureName = "Workflow Automation",
                    Description = "Create custom automated workflows and triggers",
                    AdditionalCost = 45.00m
                },
                new Feature
                {
                    FeatureId = 10,
                    FeatureName = "Premium Integrations",
                    Description = "Connect with popular third-party services and tools",
                    AdditionalCost = 25.00m
                }
            );
        }
    }
}