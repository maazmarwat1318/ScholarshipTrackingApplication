
using MVCPresentationLayer.Configuration;
using MVCPresentationLayer.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add Logger
builder.Logging.ClearProviders();
builder.Logging.AddConsole();




// Adding Options
builder.Services.AddOptions(builder.Configuration);

// Adding HTTP CLient
builder.Services.ConfigureHttpClient();

// Configuring DB abd Auth
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureAuthentication(builder.Configuration);

// Injecting Services
builder.Services.AddServices(builder.Configuration);

builder.Services.AddControllersWithViews();


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
app.UseMiddleware<AuthRedirectMiddleware>();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
