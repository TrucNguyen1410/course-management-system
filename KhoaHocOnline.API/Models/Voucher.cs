using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoaHocOnline.API.Models
{
    public class Voucher
    {
        [Key]
        public int VoucherID { get; set; }

        [Required]
        public string Code { get; set; } = string.Empty; // Mã: SALE50

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountValue { get; set; } // Giá trị giảm (ví dụ: 50 hoặc 100000)

        public bool IsPercentage { get; set; } // True: Giảm %, False: Giảm tiền mặt
        
        public DateTime ExpiryDate { get; set; } // Hạn sử dụng
        
        public bool IsActive { get; set; } = true; // Trạng thái (Bật/Tắt)
    }
}