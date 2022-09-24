using System.ComponentModel.DataAnnotations;

namespace ProductivityMonitor.Models.Input
{
    public class RoleModel
    {
        [Required]
        public string RoleName { get; set; }
        [Required]
        public String RoleDescription { get; set; }
    }
}
