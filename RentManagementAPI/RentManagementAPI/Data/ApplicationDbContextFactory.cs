using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration; // נדרש עבור IConfiguration

namespace RentManagementAPI.Data
{
    // הקלאס הזה מאפשר לכלים של EF Core (כמו Add-Migration) ליצור מופע של ה-DbContext
    // בזמן העיצוב, מבלי להריץ את כל האפליקציה.
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // בונה קונפיגורציה כדי לקרוא את ה-Connection String מ-appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // מגדיר את נתיב הבסיס לקובצי הקונפיגורציה
                .AddJsonFile("appsettings.json") // מוסיף את קובץ appsettings.json
                .Build();

            // מקבל את ה-Connection String
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // יוצר DbContextOptionsBuilder ומגדיר אותו להשתמש ב-SQL Server עם ה-Connection String
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            // מחזיר מופע של ApplicationDbContext עם האפשרויות שהוגדרו
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}