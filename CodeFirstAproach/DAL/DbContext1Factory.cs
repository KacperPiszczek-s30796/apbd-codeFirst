using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CodeFirstAproach.DAL;

public class DbContext1Factory : IDesignTimeDbContextFactory<DbContext1>
{
    public DbContext1 CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DbContext1>();
        
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=APBD_31_05_2025;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");

        return new DbContext1(optionsBuilder.Options);
    }
}