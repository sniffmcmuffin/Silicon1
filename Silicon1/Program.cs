using idInfrastructure.Contexts;
using idInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddControllersWithViews();

// Databases
// builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("AccountsServer")));

// Repositories
//builder.Services.AddScoped<AddressRepository>();
//builder.Services.AddScoped<UserRepository>();
//builder.Services.AddScoped<FeatureRepository>();
//builder.Services.AddScoped<FeatureItemRepository>();

//// Services
//builder.Services.AddScoped<AddressService>();
//builder.Services.AddScoped<UserService>();
// builder.Services.AddScoped<FeatureService>();

// Identity Individual Accounts stuff
builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//  app.UseExceptionHandler("/Home/Error");
// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
// }

app.UseHsts();
// app.UseStatusCodePagesWithReExecute("/error", "?statuscode={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
