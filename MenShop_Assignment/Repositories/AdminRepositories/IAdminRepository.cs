using MenShop_Assignment.Models.AdminModel;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.Repositories.AdminRepositories
{
    public interface IAdminRepository
    {
        Task<IActionResult> CreateUserByAdmin(StaffRegister model);
        Task<IActionResult> GetUsers(string? email, string? roleId, int? branchId);
        Task<IActionResult> UpdateUserByEmail(string email, StaffUpdate model);
    }
}
