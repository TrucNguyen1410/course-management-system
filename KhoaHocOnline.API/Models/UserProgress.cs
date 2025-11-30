using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoaHocOnline.API.Models
{
    public class UserProgress
    {
        [Key]
        public int Id { get; set; }

        public string UserID { get; set; } = string.Empty; // Ai học?

        public int ChapterID { get; set; } // Học chương nào?
        
        public int CourseID { get; set; } // Thuộc khóa nào (để dễ query)

        public bool IsCompleted { get; set; } = false; // Đã xong chưa?

        public DateTime CompletedDate { get; set; } = DateTime.Now;
    }
}