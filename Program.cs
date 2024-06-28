using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PROG7311_AgriEnergyConnectApp.Interfaces;
using PROG7311_AgriEnergyConnectApp.MyDbContext;
using PROG7311_AgriEnergyConnectApp.Repository;
using PROG7311_AgriEnergyConnectApp.Seeders;
using static System.Formats.Asn1.AsnWriter;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFarmerRepository, FarmerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddRazorPages();

// Configure Entity Framework Core to use SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity services
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

//// Seed roles
//using (var scope = app.Services.CreateScope())
//{
//	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//	var roles = new[] { "Employee", "Farmer" };

//	foreach (var role in roles)
//	{
//		if(!await roleManager.RoleExistsAsync(role))
//			await roleManager.CreateAsync(new IdentityRole(role));
//	}
//}


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

app.UseAuthentication(); // Add this line
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
//=========================__________END OF FILE__________=========================//