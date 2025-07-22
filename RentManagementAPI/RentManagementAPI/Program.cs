using Microsoft.EntityFrameworkCore; // נדרש עבור UseSqlServer
using Microsoft.Extensions.Logging; // נדרש עבור לוגינג

namespace RentManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // הוספת שירותי Controller ל-Dependency Injection
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // *** התחלה של שינויים חדשים/עדכונים ***

            // הגדרת Connection String מתוך appsettings.json
            // יש לוודא שקובץ appsettings.json מכיל ConnectionString בשם "DefaultConnection"
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // הוספת שירותי לוגינג
            // מנקה ספקי לוגינג קיימים ומוסיף לוגינג לקונסול
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // *** סוף של שינויים חדשים/עדכונים ***

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization(); // חשוב: נדרש עבור אבטחה בעתיד (כמו JWT)

            app.MapControllers(); // ממפה את הבקרים שלנו לניתובים המתאימים

            app.Run();
        }
    }
}