namespace KhoaHocOnline.WebAdmin.Models.DTOs
{
    public class CategoryDto
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        // ĐÃ SỬA: Đổi tên thành CoursesSummaryDto (thêm chữ s) để khớp với Razor
        public List<CoursesSummaryDto> Courses { get; set; } = new List<CoursesSummaryDto>();
    }

    // ĐÃ SỬA: Đổi tên class này thành CoursesSummaryDto (thêm chữ s)
    public class CoursesSummaryDto
    {
        public int CourseID { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}