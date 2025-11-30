namespace KhoaHocOnline.WebAdmin.Models.DTOs
{
    public class CourseDto
    {
        public int CourseID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ThumbnailURL { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public int? CategoryID { get; set; }
        public CategoryDto? Category { get; set; }
        
        // Class ChapterDto đã được định nghĩa ở file riêng (ChapterDto.cs)
        // Nên ở đây chỉ cần gọi nó ra dùng thôi, KHÔNG khai báo lại class ở dưới nữa.
        public List<ChapterDto> Chapters { get; set; } = new();
    }
}