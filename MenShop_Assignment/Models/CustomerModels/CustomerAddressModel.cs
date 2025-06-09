namespace MenShop_Assignment.Models.CustomerModels
{
    public class CustomerAddressModel
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
