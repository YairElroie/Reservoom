using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.DbContexts
{
    public class ReservoomDbContextFactory
    {
        private readonly string _connectionString;
        private readonly string _databaseProvider;

        public ReservoomDbContextFactory(string connectionString, string databaseProvider = "Sqlite")
        {
            _connectionString = connectionString;
            _databaseProvider = databaseProvider;
        }

        public ReservoomDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder();

            if (_databaseProvider == "SqlServer")
                optionsBuilder.UseSqlServer(_connectionString);
            else
                optionsBuilder.UseSqlite(_connectionString);

            return new ReservoomDbContext(optionsBuilder.Options);
        }
    }
}
