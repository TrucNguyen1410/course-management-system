using System.Net.Http.Json;
using System.Net.Http.Headers;
using KhoaHocOnline.WebAdmin.Models.DTOs;
using Blazored.LocalStorage;

namespace KhoaHocOnline.WebAdmin.Services
{
    public class ChapterService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public ChapterService(HttpClient httpClient, ILocalStorageService localStorage)
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

        public async Task<List<ChapterDto>> GetChaptersByCourse(int courseId)
        {
            await AddAuthHeader();
            var result = await _httpClient.GetFromJsonAsync<List<ChapterDto>>($"api/chapters/by-course/{courseId}");
            return result ?? new List<ChapterDto>();
        }

        public async Task<bool> CreateChapter(ChapterDto chapter)
        {
            await AddAuthHeader();
            var response = await _httpClient.PostAsJsonAsync("api/chapters", chapter);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateChapter(int id, ChapterDto chapter)
        {
            await AddAuthHeader();
            var response = await _httpClient.PutAsJsonAsync($"api/chapters/{id}", chapter);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteChapter(int id)
        {
            await AddAuthHeader();
            var response = await _httpClient.DeleteAsync($"api/chapters/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}