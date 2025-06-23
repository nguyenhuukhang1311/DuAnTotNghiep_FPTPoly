using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.Account;
using MenShop_Assignment.Repositories.AdminRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.APIControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAdminRepository _adminRepo;

        public AdminController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager,
            IAdminRepository adminRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _adminRepo = adminRepo;
        }

        [HttpPost("create-staff")]

        public async Task<IActionResult> CreateUserByAdmin([FromBody] AccountRegister model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _adminRepo.CreateUserByAdmin(model);
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers([FromQuery] string? email, [FromQuery] string? roleId, [FromQuery] int? branchId)
        {
            return await _adminRepo.GetUsers(email, roleId, branchId);
        }

        [HttpPut("update-user-by-id")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserById([FromQuery] string id, [FromBody] AccountUpdate model)
        {
            return await _adminRepo.UpdateUserById(id, model);
        }

    }
}
