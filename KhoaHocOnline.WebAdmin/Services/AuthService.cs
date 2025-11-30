using System.Net.Http.Json;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using KhoaHocOnline.WebAdmin.Models.DTOs;
using System.Security.Claims;

namespace KhoaHocOnline.WebAdmin.Services
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
                    // --- KIỂM TRA QUYỀN TRƯỚC KHI CHO VÀO ---
                    var claims = CustomAuthStateProvider.ParseClaimsFromJwt(content.Token);
                    
                    // Chỉ cho Admin hoặc Instructor vào trang này
                    var isAuthorized = claims.Any(c => 
                        c.Type == ClaimTypes.Role && 
                        (c.Value == "Admin" || c.Value == "Instructor"));

                    if (!isAuthorized)
                    {
                        return new AuthResponseDto 
                        { 
                            IsSuccess = false, 
                            ErrorMessage = "Tài khoản này không có quyền truy cập trang Quản trị!" 
                        };
                    }

                    // Nếu có quyền -> Lưu Token
                    await _localStorage.SetItemAsync("authToken", content.Token);
                    ((CustomAuthStateProvider)_auth).NotifyUserLogin(content.Token);
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", content.Token);
                    
                    return new AuthResponseDto { IsSuccess = true, Token = content.Token };
                }
            }
            
            return new AuthResponseDto { IsSuccess = false, ErrorMessage = "Tài khoản hoặc mật khẩu không đúng." };
        }

        public async Task Logout() 
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((CustomAuthStateProvider)_auth).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}