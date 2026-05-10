using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Reservoom.DbContexts
{
    public class RerservoomDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReservoomDbContext>
    {
        public ReservoomDbContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: true)
                .Build();

            string provider = config["DatabaseProvider"] ?? "Sqlite";
            string connectionString = config.GetConnectionString("Default")
                ?? "Data Source=reservoom.db";

            var optionsBuilder = new DbContextOptionsBuilder();

            if (provider == "SqlServer")
                optionsBuilder.UseSqlServer(connectionString);
            else
                optionsBuilder.UseSqlite(connectionString);

            return new ReservoomDbContext(optionsBuilder.Options);
        }
    }
}
