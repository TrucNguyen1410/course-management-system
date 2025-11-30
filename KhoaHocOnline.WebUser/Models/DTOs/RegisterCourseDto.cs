namespace KhoaHocOnline.WebUser.Models.DTOs
{
    public class RegisterCourseDto
    {
        public string UserID { get; set; } = string.Empty;
        public int CourseID { get; set; }
        public decimal Price { get; set; }

        // Thông tin nhập từ form
        public string FullName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public string PreferredTime { get; set; } = string.Empty; // Sáng/Chiều/Tối
        public string Note { get; set; } = string.Empty;
    }
}