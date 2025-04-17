﻿namespace MenShop_Assignment.Datas
{
    public class Branch
    {
        public int BranchId {  get; set; }
        public string? Address { get; set; }
        public string? ManagerId {  get; set; }
        public User? Manager { get; set; }
        public ICollection<User>? Employees { get; set; }
        public ICollection<BranchDetail>? BranchDetails { get; set; }
        public ICollection<OutputReceipt>? OutputReceipts { get; set; }
    }
}
