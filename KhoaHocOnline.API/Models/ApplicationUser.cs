using Microsoft.AspNetCore.Identity;

namespace KhoaHocOnline.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}