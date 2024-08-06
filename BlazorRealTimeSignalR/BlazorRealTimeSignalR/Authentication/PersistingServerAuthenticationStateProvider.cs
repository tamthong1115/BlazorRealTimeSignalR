using ChatModels.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace BlazorRealTimeSignalR.Authentication
{
    public class PersistingServerAuthenticationStateProvider : ServerAuthenticationStateProvider, IDisposable
    {

        private readonly PersistentComponentState state;
        private readonly IdentityOptions options;
        private readonly PersistingComponentStateSubscription subscription;
        private Task<AuthenticationState>? authenticationStateTask;

        public PersistingServerAuthenticationStateProvider(
            PersistentComponentState persistentComponentState,
            IOptions<IdentityOptions> optionsAccesssor)
        {
            state = persistentComponentState;
            options = optionsAccesssor.Value;

            AuthenticationStateChanged += OnAuthenticationStateChanged;
            subscription = state.RegisterOnPersisting(OnpersistingAsync, RenderMode.InteractiveWebAssembly);
        }

        private void OnAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            authenticationStateTask = task;
        }

        private async Task<AuthenticationState> OnpersistingAsync()
        {
            var authState = await authenticationStateTask;
            var principal = authState.User;
            if(principal.Identity?.IsAuthenticated == true)
            {
                var userId = principal.FindFirst(options.ClaimsIdentity.UserIdClaimType)?.Value;
                var email = principal.FindFirst(options.ClaimsIdentity.EmailClaimType)?.Value;
                var fullName = principal.Claims.Last(fn => fn.Type == ClaimTypes.Name).Value;

                if(userId != null && email!=null && fullName != null)
                {
                    state.PersistAsJson(nameof(UserInfo),new UserInfo
                    {
                        Id = userId,
                        Email = email,
                        FullName = fullName
                    });
                }
            }
        
            return authState;
        }
        public void Dispose()
        {
            subscription.Dispose();
            AuthenticationStateChanged -= OnAuthenticationStateChanged;
        }
    }
}
