using System.Net.Http.Json;
using System.Net.Http.Headers;
using KhoaHocOnline.WebAdmin.Models.DTOs;
using Blazored.LocalStorage;

namespace KhoaHocOnline.WebAdmin.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public CategoryService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        // Hàm phụ: Lấy token và gắn vào đầu mỗi yêu cầu gửi đi
        private async Task AddAuthHeader()
        {
            var token = await _localStorage.GetItemAsStringAsync("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<List<CategoryDto>> GetCategories()
        {
            await AddAuthHeader(); // <--- Gắn Token vào trước khi gọi
            try 
            {
                var result = await _httpClient.GetFromJsonAsync<List<CategoryDto>>("api/categories");
                return result ?? new List<CategoryDto>();
            }
            catch
            {
                return new List<CategoryDto>();
            }
        }

        public async Task<bool> CreateCategory(CategoryDto category)
        {
            await AddAuthHeader(); // <--- Gắn Token vào trước khi gọi
            var response = await _httpClient.PostAsJsonAsync("api/categories", category);
            return response.IsSuccessStatusCode;
        }
    }
}