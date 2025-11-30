using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KhoaHocOnline.API.Data;
using KhoaHocOnline.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace KhoaHocOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Lấy danh sách (GET)
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(await _context.Courses
                .Include(c => c.Category)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync());
        }

        // 2. Lấy chi tiết (GET ID)
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Category)
                .Include(c => c.Chapters)
                .FirstOrDefaultAsync(c => c.CourseID == id);

            if (course == null) return NotFound();
            return Ok(course);
        }

        // 3. Thêm mới (POST)
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] Course course)
        {
            course.CreatedAt = DateTime.Now;
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Thêm khóa học thành công" });
        }

        // 4. Cập nhật (PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course course)
        {
            if (id != course.CourseID) return BadRequest();

            var existingCourse = await _context.Courses.FindAsync(id);
            if (existingCourse == null) return NotFound();

            // Cập nhật các trường
            existingCourse.Title = course.Title;
            existingCourse.Description = course.Description;
            existingCourse.Price = course.Price;
            existingCourse.SalePrice = course.SalePrice;
            existingCourse.ThumbnailURL = course.ThumbnailURL;
            existingCourse.CategoryID = course.CategoryID;
            existingCourse.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Cập nhật thành công" });
        }

        // 5. Xóa (DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Xóa thành công" });
        }
    }
}