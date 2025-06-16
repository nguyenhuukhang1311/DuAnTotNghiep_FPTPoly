using MenShop_Assignment.DTO;
using MenShop_Assignment.Models;
using MenShop_Assignment.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MenShop_Assignment.Repositories.Carts
{
    public interface ICartRepository
    {
        Task<CartDTO?> GetCartByCustomerIdAsync(string customerId);
        Task AddToCartAsync(string customerId, int productDetailId, int quantity);
        Task UpdateQuantityAsync(string customerId, int productDetailId, int quantity);
        Task RemoveFromCartAsync(string customerId, int productDetailId);
        Task RemoveCartItemsAsync(string customerId, List<int> productDetailIds);

    }

}
