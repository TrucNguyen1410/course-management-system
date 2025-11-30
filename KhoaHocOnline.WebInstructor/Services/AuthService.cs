using System.Net.Http.Json;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
// Dùng DTO của Admin (do bạn copy Models sang nhưng namespace vẫn là WebAdmin)
using KhoaHocOnline.WebAdmin.Models.DTOs; 

// QUAN TRỌNG: Đổi namespace này thành WebInstructor
namespace KhoaHocOnline.WebInstructor.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _auth;

        public AuthService(HttpClient http, ILocalStorageService local, AuthenticationStateProvider auth) 
        {
            _httpClient = http; 
            _localStorage = local; 
            _auth = auth;
        }

        public async Task<AuthResponseDto> Login(LoginDto model)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/login", model);
            
            if (result.IsSuccessStatusCode) 
            {
                var content = await result.Content.ReadFromJsonAsync<AuthResponseDto>();
                
                if(content?.Token != null) 
                {
                    // Kiểm tra quyền Instructor
                    // Vì đang ở cùng namespace WebInstructor.Services nên nó sẽ tự tìm thấy CustomAuthStateProvider
                    var claims = CustomAuthStateProvider.ParseClaimsFromJwt(content.Token);
                    
                    var isInstructor = claims.Any(c => 
                        c.Type.Contains("role", StringComparison.OrdinalIgnoreCase) && 
                        c.Value == "Instructor");

                    if (!isInstructor)
                    {
                        return new AuthResponseDto 
                        { 
                            IsSuccess = false, 
                            ErrorMessage = "Tài khoản này không có quyền truy cập Cổng Giảng Viên!" 
                        };
                    }

                    await _localStorage.SetItemAsync("authToken", content.Token);
                    ((CustomAuthStateProvider)_auth).NotifyUserLogin(content.Token);
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", content.Token);
                    
                    return new AuthResponseDto { IsSuccess = true, Token = content.Token };
                }
            }
            
            return new AuthResponseDto { IsSuccess = false, ErrorMessage = "Email hoặc mật khẩu không đúng." };
        }

        public async Task Logout() 
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((CustomAuthStateProvider)_auth).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}