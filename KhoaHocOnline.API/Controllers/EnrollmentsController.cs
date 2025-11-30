using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KhoaHocOnline.API.Data;
using KhoaHocOnline.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace KhoaHocOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. LẤY DANH SÁCH TẤT CẢ (Dành cho Admin quản lý)
        [HttpGet]
        public async Task<IActionResult> GetEnrollments()
        {
            var list = await _context.Enrollments
                .Include(e => e.User)
                .Include(e => e.Course)
                .OrderByDescending(e => e.EnrollmentDate)
                .ToListAsync();
            
            // Map dữ liệu để trả về JSON gọn gàng
            var result = list.Select(e => new 
            {
                e.EnrollmentID,
                e.UserID,
                FullName = e.FullName,
                Email = e.User?.Email,
                CourseTitle = e.Course?.Title,
                PriceAtEnrollment = e.PriceAtEnrollment,
                e.EnrollmentDate,
                e.IsPaid,
                e.Address,
                e.PreferredTime
            });

            return Ok(result);
        }

        // 2. ĐĂNG KÝ KHÓA HỌC MỚI (Dành cho User)
        [HttpPost]
        public async Task<IActionResult> CreateEnrollment([FromBody] EnrollmentDto request)
        {
            // Kiểm tra xem đã đăng ký chưa
            var exists = await _context.Enrollments
                .AnyAsync(e => e.UserID == request.UserID && e.CourseID == request.CourseID);
                
            if (exists) return BadRequest("Bạn đã đăng ký khóa học này rồi.");

            var enrollment = new Enrollment
            {
                UserID = request.UserID,
                CourseID = request.CourseID,
                PriceAtEnrollment = request.Price, // Lưu giá thực tế (đã giảm voucher)
                EnrollmentDate = DateTime.Now,
                IsPaid = false, // Mặc định chưa thanh toán
                
                // Thông tin bổ sung từ Form
                FullName = request.FullName,
                DateOfBirth = request.DateOfBirth,
                Address = request.Address,
                PreferredTime = request.PreferredTime,
                Note = request.Note
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Đăng ký thành công" });
        }

        // 3. DUYỆT THANH TOÁN (Dành cho Admin)
        [HttpPut("confirm/{id}")]
        public async Task<IActionResult> ConfirmPayment(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null) return NotFound();

            enrollment.IsPaid = true; // Đánh dấu đã đóng tiền
            await _context.SaveChangesAsync();
            return Ok(new { message = "Đã xác nhận thanh toán!" });
        }

        // 4. XÓA ĐƠN HÀNG (Dành cho Admin)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null) return NotFound();

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Đã xóa đơn hàng!" });
        }

        // 5. LẤY ĐƠN HÀNG MỚI NHẤT (Dành cho trang 'Đăng ký thành công' của User)
        [HttpGet("latest/{userId}/{courseId}")]
        public async Task<IActionResult> GetLatestEnrollment(string userId, int courseId)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Where(e => e.UserID == userId && e.CourseID == courseId)
                .OrderByDescending(e => e.EnrollmentDate)
                .FirstOrDefaultAsync();

            if (enrollment == null) return NotFound();

            return Ok(new {
                enrollment.EnrollmentID,
                PricePaid = enrollment.PriceAtEnrollment, // Giá đã giảm
                CourseTitle = enrollment.Course?.Title,
                OriginalPrice = enrollment.Course?.Price, // Giá gốc
                StudentName = enrollment.FullName,
                StudentAddress = enrollment.Address
            });
        }

        // 6. LẤY DANH SÁCH KHÓA HỌC CỦA TÔI (Dành cho trang 'My Courses')
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserCourses(string userId)
        {
            var myCourses = await _context.Enrollments
                .Where(e => e.UserID == userId)
                .Include(e => e.Course)
                .Select(e => new 
                {
                    e.Course.CourseID,
                    e.Course.Title,
                    e.Course.ThumbnailURL,
                    e.Course.Description,
                    e.IsPaid, // Trạng thái thanh toán để hiển thị màu sắc
                    e.EnrollmentDate
                })
                .OrderByDescending(e => e.EnrollmentDate) // Mới nhất lên đầu
                .ToListAsync();

            return Ok(myCourses);
        }
    }

    // DTO nhận dữ liệu từ Client gửi lên
    public class EnrollmentDto
    {
        public string UserID { get; set; }
        public int CourseID { get; set; }
        public decimal Price { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public string PreferredTime { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }
}