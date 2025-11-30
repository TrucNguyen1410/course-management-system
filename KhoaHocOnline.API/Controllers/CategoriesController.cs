using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KhoaHocOnline.API.Data;
using KhoaHocOnline.API.Models;

namespace KhoaHocOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CategoriesController(ApplicationDbContext context) { _context = context; }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories.Include(c => c.Courses).ToListAsync();
            // Mapping đơn giản để tránh lỗi
            var result = categories.Select(cat => new {
                cat.CategoryID,
                cat.CategoryName,
                cat.Description,
                Courses = cat.Courses.Select(c => new { c.CourseID, c.Title }).ToList()
            });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category cat)
        {
            _context.Categories.Add(cat);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Thêm thành công" });
        }
    }
}