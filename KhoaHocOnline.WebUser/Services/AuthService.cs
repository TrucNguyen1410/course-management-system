using System.Net.Http.Json;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using KhoaHocOnline.WebUser.Models.DTOs;

namespace KhoaHocOnline.WebUser.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<AuthResponseDto> Login(LoginDto loginModel)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/login", loginModel);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadFromJsonAsync<AuthResponseDto>();
                if (content?.Token != null)
                {
                    await _localStorage.SetItemAsync("authToken", content.Token);
                    ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogin(content.Token);
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", content.Token);
                    return new AuthResponseDto { IsSuccess = true, Token = content.Token };
                }
            }
            return new AuthResponseDto { IsSuccess = false, ErrorMessage = "Sai tài khoản hoặc mật khẩu" };
        }

        public async Task<AuthResponseDto> Register(RegisterDto registerModel)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/register", registerModel);
            if (result.IsSuccessStatusCode)
            {
                return new AuthResponseDto { IsSuccess = true };
            }
            // Đọc lỗi từ API trả về
            var errorContent = await result.Content.ReadAsStringAsync();
            return new AuthResponseDto { IsSuccess = false, ErrorMessage = errorContent };
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}