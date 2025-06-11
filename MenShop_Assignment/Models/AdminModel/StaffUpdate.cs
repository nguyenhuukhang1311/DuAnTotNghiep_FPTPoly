using System.ComponentModel.DataAnnotations;

namespace MenShop_Assignment.Models.AdminModel
{
    public class StaffUpdate
    {
        public string? UserName { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? Gender { get; set; }
        public string? EmployeeAddress { get; set; }
    }
}
