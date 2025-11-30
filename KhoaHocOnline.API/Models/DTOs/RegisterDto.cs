using System.ComponentModel.DataAnnotations;
namespace KhoaHocOnline.API.Models.DTOs
{
    public class RegisterDto
    {
        [Required] [EmailAddress] public string Email { get; set; } = string.Empty; // <-- SỬA LỖI
        [Required] public string FullName { get; set; } = string.Empty; // <-- SỬA LỖI
        [Required] public string Password { get; set; } = string.Empty; // <-- SỬA LỖI
    }
}