using MenShop_Assignment.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.Repositories.AdminRepositories
{
    public interface IAdminRepository
    {
        Task<IActionResult> CreateUserByAdmin(AccountRegister model);
        Task<IActionResult> GetUsers(string? email, string? roleId, int? branchId);
        Task<IActionResult> UpdateUserById(string id, AccountUpdate model);
    }
}
