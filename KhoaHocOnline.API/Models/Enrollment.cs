using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoaHocOnline.API.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }

        public string? UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual ApplicationUser? User { get; set; }

        public int CourseID { get; set; }
        [ForeignKey("CourseID")]
        public virtual Course? Course { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceAtEnrollment { get; set; }
        
        public bool IsPaid { get; set; } = false;

        // --- CÁC TRƯỜNG MỚI BẠN YÊU CẦU ---
        public string FullName { get; set; } = string.Empty; // Họ tên người học
        public DateTime? DateOfBirth { get; set; } // Ngày sinh
        public string Address { get; set; } = string.Empty; // Địa chỉ
        public string PreferredTime { get; set; } = string.Empty; // Thời gian học mong muốn
        public string Note { get; set; } = string.Empty; // Ghi chú thêm
    }
}