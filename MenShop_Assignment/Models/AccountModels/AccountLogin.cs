using System.ComponentModel.DataAnnotations;

namespace MenShop_Assignment.Models.Account
{
    public class AccountLogin
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
