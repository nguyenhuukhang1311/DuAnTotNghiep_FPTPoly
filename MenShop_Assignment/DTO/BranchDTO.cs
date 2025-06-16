namespace MenShop_Assignment.DTO
{
    // BranchCreateDto.cs
    public class BranchCreateDto
    {
        public string Address { get; set; }
    }

    // BranchUpdateDto.cs
    public class BranchUpdateDto
    {
        public string Address { get; set; }
    }
    public class BranchDto
    {
        public int BranchId { get; set; }
        public string Address { get; set; }
        public string? ManagerId { get; set; }
    }
}

