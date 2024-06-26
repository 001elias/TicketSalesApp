using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Users/Login"; // Set the login path
        options.AccessDeniedPath = "/Users/AccessDenied"; // Set the access denied path
        // Other options can be configured here as needed
    });

    // Session
    builder.Services.AddSession(options =>
    {
        // Optional: Configure session options here if needed
        options.IdleTimeout = TimeSpan.FromMinutes(30); // Example: Set session timeout to 30 minutes
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    var app = builder.Build();




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

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=Login}/{id?}");

app.Run();
