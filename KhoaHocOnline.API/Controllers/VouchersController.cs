using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KhoaHocOnline.API.Data;
using KhoaHocOnline.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace KhoaHocOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VouchersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VouchersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Lấy danh sách (Admin)
        [HttpGet]
        public async Task<IActionResult> GetVouchers()
        {
            return Ok(await _context.Vouchers.OrderByDescending(v => v.ExpiryDate).ToListAsync());
        }

        // 2. Lấy chi tiết 1 voucher
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVoucher(int id)
        {
            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher == null) return NotFound();
            return Ok(voucher);
        }

        // 3. Tạo mới
        [HttpPost]
        public async Task<IActionResult> CreateVoucher([FromBody] Voucher voucher)
        {
            if (await _context.Vouchers.AnyAsync(v => v.Code == voucher.Code))
            {
                return BadRequest("Mã voucher này đã tồn tại!");
            }

            _context.Vouchers.Add(voucher);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Tạo voucher thành công" });
        }

        // 4. Cập nhật
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVoucher(int id, [FromBody] Voucher voucher)
        {
            if (id != voucher.VoucherID) return BadRequest();

            var existingVoucher = await _context.Vouchers.FindAsync(id);
            if (existingVoucher == null) return NotFound();

            existingVoucher.Code = voucher.Code;
            existingVoucher.DiscountValue = voucher.DiscountValue;
            existingVoucher.IsPercentage = voucher.IsPercentage;
            existingVoucher.ExpiryDate = voucher.ExpiryDate;
            existingVoucher.IsActive = voucher.IsActive;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Cập nhật thành công" });
        }

        // 5. Xóa
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoucher(int id)
        {
            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher == null) return NotFound();

            _context.Vouchers.Remove(voucher);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Xóa thành công" });
        }

        // --- 6. API KIỂM TRA VOUCHER (CHO USER) ---
        // Đã sửa lại để dùng Query String (?code=...) thay vì Route Param
        [HttpGet("check")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckVoucher([FromQuery] string code)
        {
            if (string.IsNullOrEmpty(code)) return BadRequest(new { message = "Vui lòng nhập mã." });

            // Tìm mã trong database (So sánh chính xác)
            var voucher = await _context.Vouchers
                .FirstOrDefaultAsync(v => v.Code == code);

            if (voucher == null)
                return NotFound(new { message = "Mã giảm giá không tồn tại." });

            if (!voucher.IsActive)
                return BadRequest(new { message = "Mã này đang bị khóa." });

            if (voucher.ExpiryDate < DateTime.Now)
                return BadRequest(new { message = "Mã này đã hết hạn sử dụng." });

            // Trả về kết quả
            return Ok(new { 
                voucher.Code, 
                voucher.DiscountValue, 
                voucher.IsPercentage 
            });
        }
    }
}