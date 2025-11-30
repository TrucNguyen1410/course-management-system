namespace KhoaHocOnline.WebAdmin.Models.DTOs
{
    public class ReportDto
    {
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public int TotalVouchersUsed { get; set; }
        
        public List<ChartDataDto> RevenueByMonth { get; set; } = new();
        public List<ChartDataDto> RevenueByCategory { get; set; } = new();
        
        // --- MỚI THÊM: Danh sách chi tiết voucher ---
        public List<VoucherDetailDto> VoucherDetails { get; set; } = new();
    }

    public class ChartDataDto
    {
        public string Label { get; set; } = "";
        public decimal Value { get; set; }
    }

    // --- CLASS CON MỚI ---
    public class VoucherDetailDto
    {
        public string StudentName { get; set; } = "";
        public string CourseName { get; set; } = "";
        public decimal OriginalPrice { get; set; }
        public decimal PaidPrice { get; set; }
        public DateTime Date { get; set; }
    }
}