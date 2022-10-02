using System.ComponentModel.DataAnnotations;

namespace ProductivityMonitor.Models.Input
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }

        [Required(ErrorMessage = "pls provide the email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int EmployeeId { get; set; }
    }
}
