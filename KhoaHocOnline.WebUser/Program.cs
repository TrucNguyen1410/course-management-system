using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization; // Quan trọng
using Blazored.LocalStorage; // Quan trọng
using KhoaHocOnline.WebUser;
using KhoaHocOnline.WebUser.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Cấu hình API (Đảm bảo port 5284 đúng với máy bạn)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5284") });

// Đăng ký các dịch vụ xác thực
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

// Đăng ký Service nghiệp vụ
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CourseService>();

// THÊM DÒNG NÀY ĐỂ APP BIẾT AUTH SERVICE LÀ GÌ
builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();