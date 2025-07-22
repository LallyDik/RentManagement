using Microsoft.EntityFrameworkCore; // ���� ���� UseSqlServer
using Microsoft.Extensions.Logging; // ���� ���� ������

namespace RentManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ����� ������ Controller �-Dependency Injection
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // *** ����� �� ������� �����/������� ***

            // ����� Connection String ���� appsettings.json
            // �� ����� ����� appsettings.json ���� ConnectionString ��� "DefaultConnection"
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // ����� ������ ������
            // ���� ���� ������ ������ ������ ������ �������
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // *** ��� �� ������� �����/������� ***

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization(); // ����: ���� ���� ����� ����� (��� JWT)

            app.MapControllers(); // ���� �� ������ ���� �������� ��������

            app.Run();
        }
    }
}