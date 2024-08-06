using BlazorRealTimeSignalR.Client.AppStates;
using BlazorRealTimeSignalR.Client.Authentication;
using BlazorRealTimeSignalR.Client.ChatServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<MyHubConnectionService>();
builder.Services.AddScoped<AvailableUserState>();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingAuthenticationStateProvider>();

builder.Services.AddScoped(sp => new HttpClient { 
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
});



await builder.Build().RunAsync();
