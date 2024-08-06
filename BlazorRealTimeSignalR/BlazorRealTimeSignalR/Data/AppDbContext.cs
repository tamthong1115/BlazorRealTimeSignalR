using BlazorRealTimeSignalR.Authentication;
using ChatModels;
using ChatModels.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorRealTimeSignalR.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<GroupChat> GroupChats { get; set; }
        public DbSet<AvailableUser> AvailableUsers { get; set; }
        public DbSet<IndividualChat> IndividualChats { get; set; }

    }
}
