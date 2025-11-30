namespace KhoaHocOnline.WebAdmin.Models.DTOs
{
    public class VoucherDto
    {
        public int VoucherID { get; set; }
        public string Code { get; set; } = string.Empty;
        public decimal DiscountValue { get; set; }
        public bool IsPercentage { get; set; } = true; // Mặc định là %
        public DateTime ExpiryDate { get; set; } = DateTime.Now.AddMonths(1); // Mặc định hạn 1 tháng
        public bool IsActive { get; set; } = true;
    }
}