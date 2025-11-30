using System.Net.Http.Json;
using System.Net.Http.Headers;
using KhoaHocOnline.WebUser.Models.DTOs;
using Blazored.LocalStorage;

namespace KhoaHocOnline.WebUser.Services
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

        private async Task AddAuthHeader()
        {
            var token = await _localStorage.GetItemAsStringAsync("authToken");
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<List<CourseDto>> GetCourses()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CourseDto>>("api/courses");
            return result ?? new List<CourseDto>();
        }

        public async Task<CourseDto?> GetCourseById(int id)
        {
            try { return await _httpClient.GetFromJsonAsync<CourseDto>($"api/courses/{id}"); } 
            catch { return null; }
        }

        public async Task<bool> RegisterCourse(RegisterCourseDto request)
        {
            await AddAuthHeader();
            var response = await _httpClient.PostAsJsonAsync("api/enrollments", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<VoucherDto?> CheckVoucher(string code)
        {
            try { var safeCode = Uri.EscapeDataString(code); return await _httpClient.GetFromJsonAsync<VoucherDto>($"api/vouchers/check?code={safeCode}"); } catch { return null; }
        }

        public async Task<EnrollmentSuccessDto?> GetLatestEnrollment(string userId, int courseId)
        {
            try { await AddAuthHeader(); return await _httpClient.GetFromJsonAsync<EnrollmentSuccessDto>($"api/enrollments/latest/{userId}/{courseId}"); } catch { return null; }
        }

        public async Task<List<MyCourseDto>> GetMyCourses(string userId)
        {
            try
            {
                await AddAuthHeader();
                var result = await _httpClient.GetFromJsonAsync<List<MyCourseDto>>($"api/enrollments/user/{userId}");
                return result ?? new List<MyCourseDto>();
            }
            catch { return new List<MyCourseDto>(); }
        }

        // --- 2 HÀM MỚI VỀ TIẾN ĐỘ ---
        public async Task<List<int>> GetCompletedChapters(string userId, int courseId)
        {
            try 
            {
                await AddAuthHeader();
                return await _httpClient.GetFromJsonAsync<List<int>>($"api/progress/{userId}/{courseId}") ?? new List<int>();
            }
            catch { return new List<int>(); }
        }

        public async Task MarkChapterComplete(string userId, int chapterId, int courseId)
        {
            await AddAuthHeader();
            var payload = new { UserID = userId, ChapterID = chapterId, CourseID = courseId, IsCompleted = true };
            await _httpClient.PostAsJsonAsync("api/progress/complete", payload);
        }
    }
}