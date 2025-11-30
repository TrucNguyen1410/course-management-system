using System.Net.Http.Json;
using System.Net.Http.Headers;
using KhoaHocOnline.WebAdmin.Models.DTOs;
using Blazored.LocalStorage;

namespace KhoaHocOnline.WebAdmin.Services
{
    public class EnrollmentService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public EnrollmentService(HttpClient httpClient, ILocalStorageService localStorage)
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

        public async Task<List<EnrollmentDto>> GetEnrollments()
        {
            await AddAuthHeader();
            var result = await _httpClient.GetFromJsonAsync<List<EnrollmentDto>>("api/enrollments");
            return result ?? new List<EnrollmentDto>();
        }

        public async Task<bool> ConfirmPayment(int id)
        {
            await AddAuthHeader();
            var response = await _httpClient.PutAsync($"api/enrollments/confirm/{id}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteEnrollment(int id)
        {
            await AddAuthHeader();
            var response = await _httpClient.DeleteAsync($"api/enrollments/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<ReportDto?> GetReportStats()
        {
            try
            {
                await AddAuthHeader();
                return await _httpClient.GetFromJsonAsync<ReportDto>("api/reports/dashboard");
            }
            catch { return new ReportDto(); }
        }

        // --- HÀM MỚI: LẤY DANH SÁCH TIẾN ĐỘ HỌC TẬP ---
        public async Task<List<StudentProgressDto>> GetStudentProgress()
        {
            try
            {
                await AddAuthHeader();
                var result = await _httpClient.GetFromJsonAsync<List<StudentProgressDto>>("api/reports/student-progress");
                return result ?? new List<StudentProgressDto>();
            }
            catch { return new List<StudentProgressDto>(); }
        }
    }
}