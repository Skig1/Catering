using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using SiteCatering.Domain;
using SiteCatering.Domain.Repositories.Abstract;
using SiteCatering.Infrastructure;

namespace SiteCatering
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // УДАЛЯЕМ РУЧНУЮ ЗАГРУЗКУ CONFIGURATIONBUILDER
            // Оставляем ТОЛЬКО builder.Configuration — он уже настроен по умолчанию!

            // Теперь builder.Configuration автоматически:
            // - читает appsettings.json
            // - читает appsettings.Development.json (если ASPNETCORE_ENVIRONMENT=Development)
            // - читает переменные окружения и др.

            // Получаем конфигурацию
            AppConfig config = builder.Configuration
                .GetSection("Project")
                .Get<AppConfig>()!;

            // Подключаем базу данных
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(config.DataBase.ConnectionString)
                .ConfigureWarnings(warnings =>
                    warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));

            builder.Services.AddTransient<IDishRepository, EFDishRepository>();
            builder.Services.AddTransient<DataManager>();

            // Настройка Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // Auth cookie
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "MyCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/admin/accessdenied";
                options.SlidingExpiration = true;
            });

            // Сессии
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1440);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Функционал контроллеров
            builder.Services.AddControllersWithViews();

            WebApplication app = builder.Build();

            // Статичные файлы
            app.UseStaticFiles();

            // Маршрутизация
            app.UseRouting();
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{Controller=Home}/{action=Index}/{id?}");

            // Аутентификация и авторизация
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            await app.RunAsync();
        }
    }
}
