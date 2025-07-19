using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyShop.Infrastructure.Persistence;

namespace MyShop.Infrastructure
{
    public class MyShopDbContextFactory : IDesignTimeDbContextFactory<MyShopDbContext>
    {
        public MyShopDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyShopDbContext>();

            // ⚠️ Passe den Connection-String an deine Datenbank an!
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=OrderDb;Trusted_Connection=True;");

            return new MyShopDbContext(optionsBuilder.Options);
        }
    }
}