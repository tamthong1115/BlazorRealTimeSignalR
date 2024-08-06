using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlazorRealTimeSignalR.Authentication
{
    internal static class IdentityComponentsEndpointRouteBuilderExtensions
    {
      public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
      {
            var accountGroupt = endpoints.MapGroup("/Account");
            accountGroupt.MapPost("/Logout", async(ClaimsPrincipal user, SignInManager<AppUser> signInManager) =>
            {
                Console.WriteLine("Logging out");
                await signInManager.SignOutAsync();
                return TypedResults.LocalRedirect("/Account/Login");
            });

            return accountGroupt;
      }  
    }
}
