using DataImporter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataImporter.Persistence
{
    public class DataImporterContext : DbContext
    {
        public DataImporterContext(DbContextOptions<DataImporterContext> options) 
            : base(options)
        { }

        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataImporterContext).Assembly);
        }     
    }
}
