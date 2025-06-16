using MenShop_Assignment.Datas;
using MenShop_Assignment.DTO;
using MenShop_Assignment.Models;
using MenShop_Assignment.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MenShop_Assignment.Repositories.Carts
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CartDTO?> GetCartByCustomerIdAsync(string customerId)
        {
            var cart = await _context.Carts
                .Include(c => c.Details)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart == null) return null;

            return new CartDTO
            {
                CustomerId = Guid.Parse(customerId), 
                CreatedDate = cart.CreatedDate ?? DateTime.Now, 
                Details = cart.Details.Select(d => new CartDetailDTO
                {
                    ProductDetailId = d.ProductDetailId ?? 0,
                    Price = d.Price ?? 0m,
                    Quantity = d.Quantity ?? 0,
                }).ToList()
            };
        }



        public async Task AddToCartAsync(string customerId, int productDetailId, int quantity)
        {
            var cart = await _context.Carts
                .Include(c => c.Details)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart == null)
            {
                cart = new Cart
                {
                    CustomerId = customerId,
                    CreatedDate = DateTime.Now,
                    Details = new List<CartDetail>()
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync(); // để lấy CartId
            }

            var item = cart.Details.FirstOrDefault(d => d.ProductDetailId == productDetailId);
            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                var product = await _context.ProductDetails
                .Include(p => p.HistoryPrices)
                .FirstOrDefaultAsync(p => p.DetailId == productDetailId);
                if (product == null) throw new Exception("Sản phẩm không tồn tại.");
                var latestPrice = product.HistoryPrices?
                    .OrderByDescending(h => h.UpdatedDate)
                    .FirstOrDefault()?.SellPrice ?? 0;

                cart.Details.Add(new CartDetail
                {
                    CartId = cart.CartId,
                    ProductDetailId = productDetailId,
                    Quantity = quantity,
                    Price = latestPrice
                });

            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateQuantityAsync(string customerId, int productDetailId, int quantity)
        {
            var cart = await _context.Carts
                .Include(c => c.Details)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            var item = cart?.Details.FirstOrDefault(d => d.ProductDetailId == productDetailId)
                ?? throw new Exception("Sản phẩm không tồn tại trong giỏ.");

            item.Quantity = quantity;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(string customerId, int productDetailId)
        {
            var cart = await _context.Carts
                .Include(c => c.Details)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            var item = cart?.Details.FirstOrDefault(d => d.ProductDetailId == productDetailId)
                ?? throw new Exception("Không tìm thấy sản phẩm.");

            cart.Details.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCartItemsAsync(string customerId, List<int> productDetailIds)
        {
            var cart = await _context.Carts
                .Include(c => c.Details)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart == null || cart.Details == null) return;

            var itemsToRemove = cart.Details
                .Where(d => productDetailIds.Contains(d.ProductDetailId ?? 0))
                .ToList();

            foreach (var item in itemsToRemove)
            {
                cart.Details.Remove(item);
            }

            await _context.SaveChangesAsync();
        }




    }

}
