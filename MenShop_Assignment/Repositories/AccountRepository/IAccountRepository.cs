using MenShop_Assignment.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace MenShop_Assignment.Repositories.AccountRepository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> RegisterAsync(AccountRegister model);
        Task<(string? token, IList<string>? roles, string? error)> LoginAsync(AccountLogin model);
    }
}