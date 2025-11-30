namespace KhoaHocOnline.WebAdmin.Models.DTOs
{
    public class EnrollmentDto
    {
        public int EnrollmentID { get; set; }
        public string FullName { get; set; } = string.Empty; // Tên người học
        public string Email { get; set; } = string.Empty;    // Email tài khoản
        public string CourseTitle { get; set; } = string.Empty;
        public decimal PriceAtEnrollment { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsPaid { get; set; }
        public string Address { get; set; } = string.Empty;
        public string PreferredTime { get; set; } = string.Empty;
    }
}