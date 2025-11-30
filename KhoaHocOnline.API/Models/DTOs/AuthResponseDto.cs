namespace KhoaHocOnline.API.Models.DTOs
{
    public class AuthResponseDto
    {
        public string Message { get; set; } = string.Empty; // <-- SỬA LỖI
        public string Token { get; set; } = string.Empty; // <-- SỬA LỖI
        public string FullName { get; set; } = string.Empty; // <-- SỬA LỖI
        public string Email { get; set; } = string.Empty; // <-- SỬA LỖI
        public IList<string> Roles { get; set; } = new List<string>(); // <-- SỬA LỖI
    }
}