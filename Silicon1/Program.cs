using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

// Repositories
builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<FeatureRepository>();
builder.Services.AddScoped<FeatureItemRepository>();

// Services
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<FeatureService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//  app.UseExceptionHandler("/Home/Error");
// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
// }

app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
