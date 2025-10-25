using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using PawsitiveMatch.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:5001/")
});

builder.Services.AddScoped<ApiService>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
