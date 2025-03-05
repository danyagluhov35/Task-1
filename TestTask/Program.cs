using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using TestTask.Entity;
using TestTask.IService;
using TestTask.Service;

var builder = WebApplication.CreateBuilder(args);


LogManager.Setup().LoadConfigurationFromFile("nlog.config");

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddDbContext<ApplicationContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IOrderService, OrderService>();




var app = builder.Build();

app.UseStaticFiles();
app.UseDefaultFiles();
app.UseRouting();

app.MapControllerRoute
    (
        name : "default",
        pattern : "{Controller=Home}/{action=Index}/{id?}"
    );


app.Run();
