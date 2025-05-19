namespace MenShop_Assignment.DTO
{
    // BranchCreateDto.cs
    public class BranchCreateDto
    {
        public string Address { get; set; }
        //public string? ManagerId { get; set; } // Cho phép null
    }

    // BranchUpdateDto.cs
    public class BranchUpdateDto
    {
        public string Address { get; set; }
        //public string? ManagerId { get; set; }
    }

    // BranchDto.cs (dùng để trả về dữ liệu)
    public class BranchDto
    {
        public int BranchId { get; set; }
        public string Address { get; set; }
        public string? ManagerId { get; set; }
    }
}

