namespace MenShop_Assignment.Models.Branch
{
    public static class AddressValidator
    {
        public static bool IsValidVietnamAddress(string fullAddress)
        {
            if (string.IsNullOrWhiteSpace(fullAddress))
                return false;

            foreach (var province in VietnamProvinces.Provinces)
            {
                if (fullAddress.EndsWith(province, StringComparison.OrdinalIgnoreCase) ||
                    fullAddress.Contains(province, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}
