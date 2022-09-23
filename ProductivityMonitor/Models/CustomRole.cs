using Microsoft.AspNetCore.Identity;

namespace ProductivityMonitor.Models
{
    public class CustomRole:IdentityRole
    {
        public String Role_Desc { get; set; }
    }
}
