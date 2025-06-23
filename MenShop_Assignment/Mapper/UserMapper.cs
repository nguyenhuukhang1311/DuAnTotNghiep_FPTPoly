using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.AccountModels;
using Microsoft.AspNetCore.Identity;

namespace MenShop_Assignment.Mapper
{
    public class UserMapper
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public UserMapper(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<UserViewModel> ToUserViewModel(User user)
        {
            return new UserViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Gender = (bool)user.Gender ? "Nam" : "Nữ",
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? ""
            };
        }
    }
}
