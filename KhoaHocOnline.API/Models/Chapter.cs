using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoaHocOnline.API.Models
{
    public class Chapter
    {
        [Key]
        public int ChapterID { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        // --- 2 CỘT MỚI CHO VIDEO VÀ BÀI GIẢNG ---
        public string VideoURL { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public int CourseID { get; set; }
        [ForeignKey("CourseID")]
        public virtual Course? Course { get; set; }
    }
}