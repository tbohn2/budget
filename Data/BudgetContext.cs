using Microsoft.EntityFrameworkCore;
using Budget_App.Models;
using System.Reflection;

namespace Budget_App.Data
{
    public class BudgetContext : DbContext
    {
        public BudgetContext(DbContextOptions<BudgetContext> options) : base(options) { }

        public DbSet<Year> Years { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<Earnings> Earnings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Year>()
                .HasMany(y => y.Months)
                .WithOne()
                .HasForeignKey(m => m.YearId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Month>()
                .HasOne(m => m.Earnings)
                .WithOne()
                .HasForeignKey<Earnings>(e => e.MonthId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Month>()
                .HasOne(m => m.Expenses)
                .WithOne()
                .HasForeignKey<Expenses>(e => e.MonthId)
                .OnDelete(DeleteBehavior.Cascade);

            var decimalPropertiesExpenses = typeof(Expenses)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(decimal));

            foreach (var property in decimalPropertiesExpenses)
            {
                modelBuilder.Entity<Expenses>()
                    .Property(property.Name)
                    .HasPrecision(18, 2);
            }

            var decimalPropertiesEarnings = typeof(Earnings)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(decimal));

            foreach (var property in decimalPropertiesEarnings)
            {
                modelBuilder.Entity<Earnings>()
                    .Property(property.Name)
                    .HasPrecision(18, 2);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
