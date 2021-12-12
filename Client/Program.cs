using inzLessons.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Radzen;
using BlazorDownloadFile;

namespace inzLessons.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped(sp => new HttpClient(
           sp.GetRequiredService<AuthorizationMessageHandler>()
           .ConfigureHandler(
           authorizedUrls: new[] { "" },
           scopes: new[] { "example.read", "example.write" }))
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            builder.Services.AddHttpClient("ServerAPI",
           client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("ServerAPI"));

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthServices>();
            builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<AuthServices>());
            builder.Services.AddBlazorDownloadFile();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IGroupServices, GroupServices>();
            builder.Services.AddScoped<ILoginServices, LoginServices>();
            builder.Services.AddScoped<IReservationServices, ReservationServices>();
            builder.Services.AddScoped<IMessageServices, MessageServices>();
            builder.Services.AddScoped<IAllowedReservationServices, AllowedReservationServices>();
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddOptions();

            await builder.Build().RunAsync();
        }
    }
}
