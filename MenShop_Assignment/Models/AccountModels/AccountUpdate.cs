using System.ComponentModel.DataAnnotations;

namespace MenShop_Assignment.Models.Account
{
    public class AccountUpdate
    {
        public string? UserName { get; set; }
        public string? Email {  get; set; }
        public string? Password { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? Gender { get; set; }
        public string? EmployeeAddress { get; set; }
    }
}
