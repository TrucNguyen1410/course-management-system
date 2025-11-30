using System.Net.Http.Json;
using KhoaHocOnline.WebUser.Models.DTOs;

namespace KhoaHocOnline.WebUser.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryDto>> GetCategories()
        {
            // Gọi API công khai, không cần token
            var result = await _httpClient.GetFromJsonAsync<List<CategoryDto>>("api/categories");
            return result ?? new List<CategoryDto>();
        }
    }
}