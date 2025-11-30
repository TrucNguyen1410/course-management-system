namespace KhoaHocOnline.WebUser.Models.DTOs
{
    public class VoucherDto
    {
        public string Code { get; set; } = string.Empty;
        public decimal DiscountValue { get; set; }
        public bool IsPercentage { get; set; }
    }
}