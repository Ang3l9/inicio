using Microsoft.AspNetCore.Components; 
using Microsoft.AspNetCore.Components.Web;
using ReservacionVehicleBD.Data;
using ReservacionVehicleBD.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar servicios al contenedor.
builder.Services.AddRazorPages(); // Soporte para Razor Pages
builder.Services.AddServerSideBlazor(); // Blazor Server
builder.Services.AddScoped<AuthService>(); // Servicio de autenticación
builder.Services.AddScoped<WeatherForecastService>();


var app = builder.Build();

// Configuración del pipeline de solicitud HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub(); // Habilitar Blazor Server
app.MapFallbackToPage("/_Host"); // Página host para Blazor

// Configurar redireccionamiento a Login al inicio
app.MapGet("/", context =>
{
    context.Response.Redirect("/login");
    return Task.CompletedTask;
});

app.Run();
