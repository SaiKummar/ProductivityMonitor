using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductivityMonitor.Models;

namespace ProductivityMonitor.Utilities
{
    public class AppDbContext:IdentityDbContext<CustomUser,CustomRole,string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
    }
}
