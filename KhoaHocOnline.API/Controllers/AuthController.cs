using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KhoaHocOnline.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace KhoaHocOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        // 1. API ĐĂNG NHẬP
        [HttpPost("login")]
        [AllowAnonymous] // Cho phép gọi khi chưa đăng nhập
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            // Tìm user theo email
            var user = await _userManager.FindByEmailAsync(model.Email);
            
            // Kiểm tra mật khẩu
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                // Lấy danh sách quyền (Admin/User)
                var userRoles = await _userManager.GetRolesAsync(user);

                // Tạo các Claim (thông tin in lên vé)
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName ?? ""),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", user.Id) // Lưu ID để dùng khi đăng ký khóa học
                };

                // Thêm Role vào Claim
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                // Tạo chữ ký số (Signature)
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

                // Tạo Token
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddHours(3), // Hết hạn sau 3 tiếng
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized(new { message = "Tài khoản hoặc mật khẩu không đúng" });
        }

        // 2. API ĐĂNG KÝ
        [HttpPost("register")]
        [AllowAnonymous] // Cho phép gọi khi chưa đăng nhập
        public async Task<IActionResult> Register([FromBody] RegisterApiDto model)
        {
            // Kiểm tra xem email đã tồn tại chưa
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
                return BadRequest("Email này đã được sử dụng. Vui lòng chọn email khác.");

            // Tạo user mới
            var newUser = new ApplicationUser
            {
                UserName = model.Email, // Lấy email làm tên đăng nhập luôn
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                EmailConfirmed = true // Tạm thời xác thực luôn để không phải gửi mail
            };

            // Lưu vào Database
            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                // Mặc định user mới đăng ký sẽ có quyền "User" (Học viên)
                await _userManager.AddToRoleAsync(newUser, "User");
                
                return Ok(new { message = "Đăng ký thành công! Bạn có thể đăng nhập ngay." });
            }

            // Nếu lỗi (ví dụ mật khẩu yếu) -> trả về lỗi chi tiết
            return BadRequest(result.Errors);
        }
    }

    // --- CÁC CLASS DTO DÙNG ĐỂ NHẬN DỮ LIỆU ---

    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterApiDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}