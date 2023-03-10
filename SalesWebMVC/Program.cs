using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMVC.Data;
using SalesWebMVC.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<SalesWebMVCContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("SalesWebMVCContext")
     ?? throw new InvalidOperationException("Connection string 'SalesWebMVCContext' not found.")));



builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var enUs = new CultureInfo("en-US");
    options.DefaultRequestCulture = new RequestCulture(enUs);
    options.SupportedCultures = new List<CultureInfo> { enUs };
    options.SupportedUICultures = new List<CultureInfo> { enUs };
});


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<SeedingService>();
builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DepartmentService>();

var app = builder.Build();

app.UseRequestLocalization();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedingService.Seed(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
