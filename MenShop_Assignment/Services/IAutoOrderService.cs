using MenShop_Assignment.DTO;
namespace MenShop_Assignment.Services
{
    public interface IAutoOrderService
    {
        Task<ApprovalResultDto> ApproveOrderAsync(int orderId);
        Task<BatchApprovalDto> ApproveAllOrdersAsync();
        Task<PendingOrdersDto> GetPendingOrdersAsync();
    }
}
