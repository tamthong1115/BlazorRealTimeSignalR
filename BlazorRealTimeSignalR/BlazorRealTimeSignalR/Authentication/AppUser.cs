using Microsoft.AspNetCore.Identity;

namespace BlazorRealTimeSignalR.Authentication
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
