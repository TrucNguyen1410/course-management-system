using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KhoaHocOnline.API.Data;
using KhoaHocOnline.API.Models;

namespace KhoaHocOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChaptersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChaptersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Lấy danh sách chương theo CourseID
        [HttpGet("by-course/{courseId}")]
        public async Task<IActionResult> GetChaptersByCourse(int courseId)
        {
            var chapters = await _context.Chapters
                .Where(c => c.CourseID == courseId)
                .OrderBy(c => c.ChapterID)
                .ToListAsync();
            return Ok(chapters);
        }

        // 2. Thêm chương mới
        [HttpPost]
        public async Task<IActionResult> CreateChapter([FromBody] Chapter chapter)
        {
            _context.Chapters.Add(chapter);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Thêm chương thành công" });
        }

        // 3. Cập nhật chương
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChapter(int id, [FromBody] Chapter chapter)
        {
            if (id != chapter.ChapterID) return BadRequest();
            
            var exist = await _context.Chapters.FindAsync(id);
            if (exist == null) return NotFound();

            exist.Title = chapter.Title;
            exist.Description = chapter.Description;
            
            // Cập nhật thêm Video và Nội dung
            exist.VideoURL = chapter.VideoURL;
            exist.Content = chapter.Content;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Cập nhật thành công" });
        }

        // 4. Xóa chương
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChapter(int id)
        {
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter == null) return NotFound();

            _context.Chapters.Remove(chapter);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Xóa thành công" });
        }
    }
}