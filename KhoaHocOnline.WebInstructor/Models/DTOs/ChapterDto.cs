namespace KhoaHocOnline.WebAdmin.Models.DTOs
{
    public class ChapterDto
    {
        public int ChapterID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        // --- THÊM MỚI ---
        public string VideoURL { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        
        public int CourseID { get; set; }
    }
}