namespace KhoaHocOnline.WebAdmin.Models.DTOs
{
    public class StudentProgressDto
    {
        public string StudentName { get; set; } = "";
        public string Email { get; set; } = "";
        public string CourseName { get; set; } = "";
        public int TotalChapters { get; set; }
        public int CompletedChapters { get; set; }
        public DateTime? LastActiveDate { get; set; }

        // Tính phần trăm để vẽ thanh loading
        public int Percent => TotalChapters == 0 ? 0 : (int)((double)CompletedChapters / TotalChapters * 100);
    }
}