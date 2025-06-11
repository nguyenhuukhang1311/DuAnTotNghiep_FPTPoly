namespace MenShop_Assignment.DTO
{
    public class BatchApprovalDto
    {
        public int Total { get; set; }
        public int Approved { get; set; }
        public int Failed { get; set; }
        public List<string> ApprovedIds { get; set; } = new();
        public List<FailedOrderDto> FailedOrders { get; set; } = new();
    }
}
