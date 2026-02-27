using EmployeeManagementSystem.Client.Services;
using EmployeeManagementSystem.Client.Services.Abstractions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace EmployeeManagementSystem.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5205/") });
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        await builder.Build().RunAsync();
    }
}