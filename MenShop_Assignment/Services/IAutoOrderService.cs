using MenShop_Assignment.DTO;
namespace MenShop_Assignment.Services
{
    public interface IAutoOrderService
    {
        Task<ApprovalResultDto> ApproveOrderAsync(string orderId);
        Task<BatchApprovalDto> ApproveAllOrdersAsync();
        Task<PendingOrdersDto> GetPendingOrdersAsync();
    }
}
