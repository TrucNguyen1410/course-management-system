using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using KhoaHocOnline.WebInstructor;

// --- QUAN TRỌNG: KHAI BÁO CẢ 2 DÒNG NÀY ĐỂ NHẬN DIỆN HẾT CÁC FILE ---
using KhoaHocOnline.WebInstructor.Services; // Cho AuthService, CustomAuthStateProvider...
using KhoaHocOnline.WebAdmin.Services;      // Cho các file copy từ Admin sang (CategoryService...)
// ---------------------------------------------------------------------

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 1. Cấu hình API (5284)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5284") });

// 2. Cấu hình Auth
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

// 3. Đăng ký Services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<VoucherService>();
builder.Services.AddScoped<EnrollmentService>();
builder.Services.AddScoped<ChapterService>();

await builder.Build().RunAsync();