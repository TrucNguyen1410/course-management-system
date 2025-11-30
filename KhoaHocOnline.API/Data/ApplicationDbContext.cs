using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KhoaHocOnline.API.Models;

namespace KhoaHocOnline.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        
        // --- MỚI THÊM ---
        public DbSet<UserProgress> UserProgresses { get; set; }
    }
}