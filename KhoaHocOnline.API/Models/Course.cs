using System;
using System.Collections.Generic; // Cần cho ICollection
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoaHocOnline.API.Models
{
    [Table("Courses")]
    public class Course
    {
        [Key] 
        public int CourseID { get; set; }

        public string? Title { get; set; } 
        public string? Description { get; set; }
        public string? ThumbnailURL { get; set; }
        
        [Column(TypeName = "decimal(18,2)")] // Định dạng tiền tệ chuẩn SQL
        public decimal Price { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? SalePrice { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Mặc định lấy giờ hiện tại
        public DateTime? UpdatedAt { get; set; }

        // --- KHÓA NGOẠI ---
        public int? CategoryID { get; set; }
        [ForeignKey("CategoryID")] 
        public virtual Category? Category { get; set; }

        public string? InstructorID { get; set; }
        [ForeignKey("InstructorID")] 
        public virtual ApplicationUser? Instructor { get; set; }

        // --- DANH SÁCH CON (CHƯƠNG) ---
        // Khởi tạo luôn new List để không bao giờ bị null
        public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();

        // ĐÃ XÓA DÒNG: public ICollection<Course> Courses { get; set; } 
        // (Vì Course không thể chứa List<Course> đệ quy như vậy được, đó là thuộc tính của Category)
    }
}