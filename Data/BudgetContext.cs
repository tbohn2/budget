using Microsoft.EntityFrameworkCore;
using Budget_App.Models;

namespace Budget_App.Data
{
    public class BudgetContext : DbContext
    {
        public BudgetContext(DbContextOptions<BudgetContext> options) : base(options) { }

        public DbSet<Year> Years { get; set; }
        public DbSet<Month> Months { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Year>()
                .HasMany(y => y.Months)
                .WithOne(m => m.Year)
                .HasForeignKey(m => m.YearId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
