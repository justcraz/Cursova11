using Microsoft.EntityFrameworkCore;
using PharmaKursWork.Data;
using PharmaKursWork.Servises;
// using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.


builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<UserServise>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

using (IServiceScope scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<DataContext>();
    context.Database.Migrate();
    context.EnsureDatabaseSeeded();
}

app.UseRouting();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseAuthorization();

app.MapRazorPages();

app.Run();