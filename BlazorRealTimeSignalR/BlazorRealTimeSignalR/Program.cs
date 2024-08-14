using BlazorRealTimeSignalR.Authentication;
using BlazorRealTimeSignalR.ChatHubs;
using BlazorRealTimeSignalR.Client.AppStates;
using BlazorRealTimeSignalR.Client.ChatServices;
using BlazorRealTimeSignalR.Client.Pages;
using BlazorRealTimeSignalR.Components;
using BlazorRealTimeSignalR.Data;
using BlazorRealTimeSignalR.Repos;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("ServerClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BaseAddress"] ?? "https://localhost:7192");
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddDbContext<AppDbContext>
    (o => o.UseSqlite(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddControllers();
builder.Services.AddScoped<AvailableUserState>();
builder.Services.AddScoped<ChatRepo>();
builder.Services.AddSignalR();
builder.Services.AddScoped<MyHubConnectionService>();

builder.Services.AddIdentityCore<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options => {
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies(o => { });

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingServerAuthenticationStateProvider>();
var app = builder.Build();

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

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorRealTimeSignalR.Client._Imports).Assembly);




app.MapControllers();
app.MapHub<ChatHub>("/chathub");
app.MapAdditionalIdentityEndpoints();
app.Run();