using EShop.Application;
using EShop.Application.Interfaces;
using EShop.Application.Services;
using EShop.Domain.Interfaces;
using EShop.Web.Middlewares;
using EShop.Web.Services;
using Infra.Data.Persistence;
using Infra.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog ((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});
// Add services to the container.


#region Config Serilog

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()

    // Info → Seq
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
        .WriteTo.Seq("http://localhost:5341"))

    // Error → File
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
        .WriteTo.File(@"C:\ApplicationLog\log-.txt", rollingInterval: RollingInterval.Day))

// Warning → SQL Server
.WriteTo.Logger(lc => lc
    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("LogConnectionString"),
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "Logs",
            AutoCreateSqlTable = true,
            BatchPostingLimit = 100,
            BatchPeriod = TimeSpan.FromSeconds(2)
        },
        columnOptions: null
    ))

    .CreateBootstrapLogger();

builder.Host.UseSerilog();
#endregion /Config Serilog



builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnectionString")));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepositroy, CategoryRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICheckoutService, CheckoutService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();



builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IWorkWithProductImage, WorkWithProductImage>();






RegisterServices(builder.Services);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();


app.UseMiddleware<LoggingMiddleware>();



app.Run();


static void RegisterServices(IServiceCollection services)
{
    ApplicationServicesRegistration.ConfigureApplicationServices(services);

}