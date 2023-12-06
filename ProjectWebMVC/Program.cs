using System;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;
//if (environment.IsDevelopment())
//{
//    builder.WebHost.UseUrls("https://www.jamesthew.com.vn");
//}
//else
//{
//    var url = builder.Configuration["Urls:" + environment.EnvironmentName];
//    if (!string.IsNullOrEmpty(url))
//    {
//        builder.WebHost.UseUrls(url);
//    }
//}


builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
if (environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Admin}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "user",
    areaName: "User",
    pattern: "User/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
