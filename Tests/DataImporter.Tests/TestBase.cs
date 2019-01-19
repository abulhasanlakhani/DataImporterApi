using System;
using DataImporter.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DataImporter.Tests
{
    public class TestBase
    {
        public DataImporterContext GetDbContext(bool useSqlLite = false)
        {
            var builder = new DbContextOptionsBuilder<DataImporterContext>();

            if (useSqlLite)
            {
                builder.UseSqlite("DataSource=:memory:", x => { });
            }
            else
            {
                builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            }

            var dbContext = new DataImporterContext(builder.Options);

            if (useSqlLite)
            {
                // SQLite needs to open connection to the DB.
                // Not required for in-memory-database.
                dbContext.Database.OpenConnection();
            }

            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}
