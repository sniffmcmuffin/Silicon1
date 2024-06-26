using idInfrastructure.Contexts;
using idInfrastructure.Entities;
using idInfrastructure.Repositories;
using idInfrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Silicon1.Helpers.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

// Databases
// builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("AccountsServer")));

// Repositories
//builder.Services.AddScoped<AddressRepository>();
//builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<FeatureRepository>();
builder.Services.AddScoped<FeatureItemRepository>();

//// Services
//builder.Services.AddScoped<AddressService>();
//builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<FeatureService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CourseService>();

// Identity Individual Accounts stuff
builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(x =>
{   
    x.LoginPath = "/signin";
    x.LogoutPath = "/signout";
	x.AccessDeniedPath = "/denied"; 

	x.Cookie.HttpOnly = true;
    x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    x.SlidingExpiration = true; // N�r user g�r n�tt p� sidan s� nollst�lls timern ovan.
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Ha allitd med iom https.
});

// External Authentication
builder.Services.AddAuthentication().AddFacebook(x =>
{
    x.AppId = "965511941229847";
    x.AppSecret = "732e59ba1099d3ab1332e233a166eb67";
    x.Fields.Add("first_name");
    x.Fields.Add("last_name");
});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("SuperAdmins", policy => policy.RequireRole("SuperAdmin"));
    x.AddPolicy("Admins", policy => policy.RequireRole("SuperAdmin", "Admin"));
    x.AddPolicy("Managers", policy => policy.RequireRole("Admin", "SuperAdmin", "Manager"));
    x.AddPolicy("AuthenticatedUsers", policy => policy.RequireRole("Admin", "SuperAdmin", "Manager", "User"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//  app.UseExceptionHandler("/Home/Error");
// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
// }

app.UseHsts();
app.UseStatusCodePagesWithReExecute("/error", "?statuscode={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseUserSessionValidation(); 
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

	string[] roles = ["SuperAdmin", "Admin", "Manager", "User"];

    foreach (var role in roles)
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
