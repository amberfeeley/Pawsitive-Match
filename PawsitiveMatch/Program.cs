using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using PawsitiveMatch.Authentication;
using PawsitiveMatch.Client.Pages;
using PawsitiveMatch.Client.Services;
using PawsitiveMatch.Components;
using PawsitiveMatch.Controllers;
using PawsitiveMatch.Services;
using PawsitiveMatch.SharedModels.Models;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// EF Core/MySQL connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)))
);

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<PetsService>();
builder.Services.AddScoped<StateService>();
builder.Services.AddScoped<PetService>();
builder.Services.AddScoped<ApiService>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("https://localhost:5001/")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:5001/")
});

builder.Services.AddAuthentication("PawsitiveAuth")
    .AddCookie("PawsitiveAuth", options =>
    {
        options.LoginPath = "/api/auth/login";
        options.LogoutPath = "/api/auth/logout";
        options.Cookie.Name = "PawsitiveAuthCookie";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
    });
builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
    if (!dbContext.Pet.Any())
    {
        dbContext.Pet.AddRange(PetsController.Pets);
        dbContext.SaveChanges();
    }
    
    if (!await dbContext.User.AnyAsync(u => u.Email == "admin@admin.com"))
    {
        var authService = new AuthService(dbContext);

        dbContext.User.Add(authService.Admin);
        await dbContext.SaveChangesAsync();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();
app.UseCors("AllowBlazorClient");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapControllers();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(PawsitiveMatch.Client._Imports).Assembly);

app.Run();
