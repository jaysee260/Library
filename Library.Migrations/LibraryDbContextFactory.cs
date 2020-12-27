using System.IO;
using System.Reflection;
using Library.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Library.Migrations
{
    public class LibraryDbContextFactory : IDesignTimeDbContextFactory<LibraryDbContext>
    {
        public LibraryDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration["ConnectionString"];
            var dbContextOptions = new DbContextOptionsBuilder<LibraryDbContext>();
            dbContextOptions.UseNpgsql(
                connectionString,
                options => options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
            );

            return new LibraryDbContext(dbContextOptions.Options);
        }
    }
}