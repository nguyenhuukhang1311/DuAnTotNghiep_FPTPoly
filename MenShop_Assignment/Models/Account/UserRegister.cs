using System.ComponentModel.DataAnnotations;

namespace MenShop_Assignment.Models.Account
{
    public class UserRegister
    {
        [Required]
        public string FullName { get; set; } = null;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public bool? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        //public string? EmployeeAddress { get; set; }
    }
}
