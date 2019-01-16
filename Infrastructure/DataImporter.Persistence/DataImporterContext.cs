using System;
using System.Collections.Generic;
using System.Text;
using DataImporter.Domain;
using Microsoft.EntityFrameworkCore;

namespace DataImporter.Persistence
{
    public class DataImporterContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
    }
}
