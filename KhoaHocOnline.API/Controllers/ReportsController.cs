using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KhoaHocOnline.API.Data;
using Microsoft.AspNetCore.Authorization;

namespace KhoaHocOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardStats()
        {
            // (Giữ nguyên code cũ của Dashboard)...
            var paidEnrollments = await _context.Enrollments
                .Include(e => e.Course)
                .ThenInclude(c => c.Category)
                .Where(e => e.IsPaid == true)
                .ToListAsync();

            var totalRevenue = paidEnrollments.Sum(e => e.PriceAtEnrollment);
            var totalOrders = paidEnrollments.Count;
            var totalVouchersUsed = paidEnrollments.Count(e => e.PriceAtEnrollment < e.Course?.Price);

            var revenueByMonth = paidEnrollments
                .GroupBy(e => new { e.EnrollmentDate.Month, e.EnrollmentDate.Year })
                .Select(g => new { Label = $"Tháng {g.Key.Month}/{g.Key.Year}", Value = g.Sum(e => e.PriceAtEnrollment) })
                .ToList();

            var revenueByCategory = paidEnrollments
                .GroupBy(e => e.Course.Category.CategoryName)
                .Select(g => new { Label = g.Key, Value = g.Sum(e => e.PriceAtEnrollment) })
                .ToList();

            // Lấy danh sách dùng voucher (Code cũ)
            var voucherOrders = paidEnrollments
                .Where(e => e.PriceAtEnrollment < e.Course?.Price)
                .Select(e => new 
                {
                    StudentName = e.FullName,
                    CourseName = e.Course?.Title ?? "Không xác định",
                    OriginalPrice = e.Course?.Price ?? 0,
                    PaidPrice = e.PriceAtEnrollment,
                    Date = e.EnrollmentDate
                }).ToList();

            return Ok(new {
                TotalRevenue = totalRevenue,
                TotalOrders = totalOrders,
                TotalVouchersUsed = totalVouchersUsed,
                RevenueByMonth = revenueByMonth,
                RevenueByCategory = revenueByCategory,
                VoucherDetails = voucherOrders
            });
        }

        // --- API MỚI: LẤY TIẾN ĐỘ HỌC TẬP CỦA TẤT CẢ HỌC VIÊN ---
        [HttpGet("student-progress")]
        public async Task<IActionResult> GetStudentProgress()
        {
            // 1. Lấy tất cả đơn hàng đã thanh toán
            var enrollments = await _context.Enrollments
                .Include(e => e.Course)
                .Where(e => e.IsPaid == true)
                .OrderByDescending(e => e.EnrollmentDate)
                .ToListAsync();

            var result = new List<object>();

            foreach (var enrollment in enrollments)
            {
                if (enrollment.Course == null) continue;

                // 2. Đếm tổng số chương của khóa học
                var totalChapters = await _context.Chapters
                    .CountAsync(c => c.CourseID == enrollment.CourseID);

                // 3. Đếm số chương user này đã hoàn thành
                var completedChapters = await _context.UserProgresses
                    .CountAsync(p => p.UserID == enrollment.UserID 
                                  && p.CourseID == enrollment.CourseID 
                                  && p.IsCompleted);

                // 4. Lấy bài học gần nhất (nếu có)
                var lastActivity = await _context.UserProgresses
                    .Where(p => p.UserID == enrollment.UserID && p.CourseID == enrollment.CourseID)
                    .OrderByDescending(p => p.CompletedDate)
                    .Select(p => p.CompletedDate)
                    .FirstOrDefaultAsync();

                result.Add(new
                {
                    StudentName = enrollment.FullName,
                    Email = enrollment.UserID, // Hoặc query thêm bảng User để lấy email chính xác nếu UserID là Guid
                    CourseName = enrollment.Course.Title,
                    TotalChapters = totalChapters,
                    CompletedChapters = completedChapters,
                    LastActiveDate = lastActivity == DateTime.MinValue ? (DateTime?)null : lastActivity
                });
            }

            return Ok(result);
        }
    }
}