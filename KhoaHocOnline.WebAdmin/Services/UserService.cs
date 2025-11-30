using System.Net.Http.Json;
using System.Net.Http.Headers;
using KhoaHocOnline.WebAdmin.Models.DTOs;
using Blazored.LocalStorage;

namespace KhoaHocOnline.WebAdmin.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public UserService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        private async Task AddAuthHeader()
        {
            var token = await _localStorage.GetItemAsStringAsync("authToken");
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<List<UserDto>> GetUsers()
        {
            await AddAuthHeader();
            var result = await _httpClient.GetFromJsonAsync<List<UserDto>>("api/users");
            return result ?? new List<UserDto>();
        }

        public async Task<UserDto?> GetUserById(string id)
        {
            await AddAuthHeader();
            try {
                return await _httpClient.GetFromJsonAsync<UserDto>($"api/users/{id}");
            } catch { return null; }
        }

        public async Task<bool> CreateUser(UserDto user)
        {
            await AddAuthHeader();
            // Map DTO sang object API cần
            var payload = new { 
                user.Email, 
                user.Password, 
                user.PhoneNumber, 
                Role = user.SelectedRole 
            };
            var response = await _httpClient.PostAsJsonAsync("api/users", payload);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUser(string id, UserDto user)
        {
            await AddAuthHeader();
            var payload = new { 
                user.Email, 
                user.Password, // Nếu rỗng API sẽ bỏ qua
                user.PhoneNumber, 
                Role = user.SelectedRole 
            };
            var response = await _httpClient.PutAsJsonAsync($"api/users/{id}", payload);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUser(string id)
        {
            await AddAuthHeader();
            var response = await _httpClient.DeleteAsync($"api/users/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}