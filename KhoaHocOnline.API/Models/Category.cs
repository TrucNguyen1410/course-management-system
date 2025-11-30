using System.ComponentModel.DataAnnotations;

namespace KhoaHocOnline.API.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}