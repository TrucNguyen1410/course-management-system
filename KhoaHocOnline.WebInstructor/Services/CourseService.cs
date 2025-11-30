using System.Net.Http.Json;
using System.Net.Http.Headers;
using KhoaHocOnline.WebAdmin.Models.DTOs;
using Blazored.LocalStorage;

namespace KhoaHocOnline.WebAdmin.Services
{
    public class CourseService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public CourseService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        // Hàm phụ: Gắn Token vào Header
        private async Task AddAuthHeader()
        {
            var token = await _localStorage.GetItemAsStringAsync("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        // 1. Lấy danh sách (Sửa tên hàm từ GetAllCourses -> GetCourses để khớp với lỗi của bạn)
        public async Task<List<CourseDto>> GetCourses()
        {
            await AddAuthHeader();
            var result = await _httpClient.GetFromJsonAsync<List<CourseDto>>("api/courses");
            return result ?? new List<CourseDto>();
        }

        // 2. Lấy chi tiết
        public async Task<CourseDto?> GetCourseById(int id)
        {
            await AddAuthHeader();
            try {
                return await _httpClient.GetFromJsonAsync<CourseDto>($"api/courses/{id}");
            } catch { return null; }
        }

        // 3. Thêm mới (Hàm còn thiếu gây lỗi CreateCourse)
        public async Task<bool> CreateCourse(CourseDto course)
        {
            await AddAuthHeader();
            var response = await _httpClient.PostAsJsonAsync("api/courses", course);
            return response.IsSuccessStatusCode;
        }

        // 4. Cập nhật (Hàm còn thiếu gây lỗi UpdateCourse)
        public async Task<bool> UpdateCourse(int id, CourseDto course)
        {
            await AddAuthHeader();
            var response = await _httpClient.PutAsJsonAsync($"api/courses/{id}", course);
            return response.IsSuccessStatusCode;
        }

        // 5. Xóa (Hàm còn thiếu gây lỗi DeleteCourse)
        public async Task<bool> DeleteCourse(int id)
        {
            await AddAuthHeader();
            var response = await _httpClient.DeleteAsync($"api/courses/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}