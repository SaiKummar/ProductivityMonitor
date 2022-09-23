using Microsoft.AspNetCore.Identity;

namespace ProductivityMonitor.Models
{
    public class CustomUser:IdentityUser
    {
        public DateTime User_CDate { get; set; }
        public int User_Empl_Id { get; set; }
        public string User_Status { get; set; }
        public DateTime User_LuDate { get; set; }
    }
}
