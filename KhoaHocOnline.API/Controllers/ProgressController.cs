using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KhoaHocOnline.API.Data;
using KhoaHocOnline.API.Models;

namespace KhoaHocOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProgressController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Lấy danh sách ID các chương đã hoàn thành của 1 User trong 1 Khóa học
        [HttpGet("{userId}/{courseId}")]
        public async Task<IActionResult> GetCompletedChapters(string userId, int courseId)
        {
            var completedIds = await _context.UserProgresses
                .Where(p => p.UserID == userId && p.CourseID == courseId && p.IsCompleted)
                .Select(p => p.ChapterID)
                .ToListAsync();

            return Ok(completedIds);
        }

        // 2. Đánh dấu hoàn thành chương
        [HttpPost("complete")]
        public async Task<IActionResult> MarkAsCompleted([FromBody] UserProgress progress)
        {
            var existing = await _context.UserProgresses
                .FirstOrDefaultAsync(p => p.UserID == progress.UserID && p.ChapterID == progress.ChapterID);

            if (existing != null)
            {
                existing.IsCompleted = true; // Nếu có rồi thì update
                existing.CompletedDate = DateTime.Now;
            }
            else
            {
                progress.IsCompleted = true;
                progress.CompletedDate = DateTime.Now;
                _context.UserProgresses.Add(progress);
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Đã lưu tiến độ" });
        }
    }
}