using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace KhoaHocOnline.API.Models
{
    [Table("Lessons")]
    public class Lesson
    {
        [Key] public int LessonID { get; set; }
        public string? Title { get; set; } // <-- ĐÃ THÊM ?
        public string? VideoURL { get; set; }
        public string? Content { get; set; }
        public int DurationInMinutes { get; set; }
        public int LessonOrder { get; set; }
        public int? ChapterID { get; set; }
        [ForeignKey("ChapterID")] public virtual Chapter? Chapter { get; set; }
    }
}