namespace KhoaHocOnline.WebAdmin.Models.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // Dùng khi tạo/sửa
        
        // Danh sách quyền
        public IList<string> Roles { get; set; } = new List<string>();
        
        // Quyền được chọn (để gửi lên API)
        public string SelectedRole { get; set; } = "User"; 
    }
}