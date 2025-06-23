using MenShop_Assignment.Models.Account;
using MenShop_Assignment.Repositories.AccountRepository;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepo;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountRepository accountRepo, IConfiguration configuration)
        {
            _accountRepo = accountRepo;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AccountRegister model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _accountRepo.RegisterAsync(model);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new { message = "Đăng ký thành công với vai trò Khách hàng."});
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AccountLogin model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (token, roles, error) = await _accountRepo.LoginAsync(model);
            if (token == null) return Unauthorized(error);

            return Ok(new
            {
                token,
                expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
                email = model.Email,
                roles
            });
        }
    }
}
