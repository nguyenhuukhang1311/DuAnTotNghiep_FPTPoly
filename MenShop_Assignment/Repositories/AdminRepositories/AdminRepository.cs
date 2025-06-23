// Repositories/AdminRepository.cs
using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.AccountModels;
using MenShop_Assignment.Models.Account;
using MenShop_Assignment.Repositories.AdminRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> CreateUserByAdmin(AccountRegister model)
        {
            if (string.Equals(model.Role, "Khách hàng", StringComparison.OrdinalIgnoreCase))
                return new BadRequestObjectResult("Admin không được phép tạo tài khoản với vai trò Khách hàng.");

            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
                return new BadRequestObjectResult("Email đã tồn tại.");

            var user = new User
            {
                FullName = model.FullName,
                UserName = model.Email,
                Email = model.Email,
                Gender = model.Gender,
                BirthDate = model.BirthDate,
                CreatedDate = DateTime.UtcNow,
                EmployeeAddress = model.EmployeeAddress
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new BadRequestObjectResult(result.Errors);

            var role = await _roleManager.FindByIdAsync(model.Role);
            if (role == null)
                return new BadRequestObjectResult($"Vai trò với Id {model.Role} không tồn tại.");

            var roleResult = await _userManager.AddToRoleAsync(user, role.Name);
            if (!roleResult.Succeeded)
                return new BadRequestObjectResult(roleResult.Errors);

            return new OkObjectResult(new { message = $"Tạo tài khoản thành công với vai trò {role.Name}." });
        }

        public async Task<IActionResult> GetUsers(string? email, string? roleId, int? branchId)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                    return new NotFoundObjectResult($"Không tìm thấy người dùng với email: {email}");

                var roles = await _userManager.GetRolesAsync(user);
                return new OkObjectResult(new[] {
                    new {
                        user.Id, user.Email, user.UserName,
                        user.BirthDate, user.Gender, user.CreatedDate,
                        user.EmployeeAddress, Roles = roles
                    }
                });
            }

            List<User> users;
            if (!string.IsNullOrEmpty(roleId))
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role == null)
                    return new NotFoundObjectResult($"Không tìm thấy vai trò với Id: {roleId}");

                users = (await _userManager.GetUsersInRoleAsync(role.Name)).ToList();
            }
            else
            {
                users = _userManager.Users.ToList();
            }

            if (branchId.HasValue)
                users = users.Where(u => u.BranchId == branchId.Value).ToList();

            var result = new List<object>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new
                {
                    user.Id,
                    user.Email,
                    user.UserName,
                    user.BirthDate,
                    user.Gender,
                    user.CreatedDate,
                    user.EmployeeAddress,
                    Roles = roles
                });
            }

            return new OkObjectResult(result);
        }

        public async Task<IActionResult> UpdateUserById(string id, AccountUpdate model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new NotFoundObjectResult($"Không tìm thấy người dùng với ID: {id}");

            user.UserName = model.UserName ?? user.UserName;
            user.BirthDate = model.BirthDate ?? user.BirthDate;
            user.Gender = model.Gender ?? user.Gender;
            user.EmployeeAddress = model.EmployeeAddress ?? user.EmployeeAddress;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return new BadRequestObjectResult(result.Errors);

            return new OkObjectResult(new { message = "Cập nhật người dùng thành công." });
        }

    }
}
