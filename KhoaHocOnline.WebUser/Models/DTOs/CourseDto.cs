namespace KhoaHocOnline.WebUser.Models.DTOs
{
    // 1. Dùng cho trang chi tiết khóa học
    public class CourseDto
    {
        public int CourseID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ThumbnailURL { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }
        
        public int? CategoryID { get; set; }
        public CategoryDto? Category { get; set; } 

        public List<ChapterDto> Chapters { get; set; } = new List<ChapterDto>();
    }

    // 2. Dùng cho danh sách chương học
    public class ChapterDto
    {
        public int ChapterID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string VideoURL { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }

    // 3. Dùng cho trang Đăng ký thành công (Hóa đơn)
    public class EnrollmentSuccessDto
    {
        public int EnrollmentID { get; set; }
        public decimal PricePaid { get; set; } 
        public string CourseTitle { get; set; } = "";
        public decimal OriginalPrice { get; set; } 
        public string StudentName { get; set; } = "";
        public string StudentAddress { get; set; } = "";
    }

    // 4. Dùng cho trang "Khóa học của tôi" (ĐÂY LÀ CÁI BẠN ĐANG THIẾU)
    public class MyCourseDto
    {
        public int CourseID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ThumbnailURL { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsPaid { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}