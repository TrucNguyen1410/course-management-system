using System.Net.Http.Json;
using System.Net.Http.Headers;
using KhoaHocOnline.WebAdmin.Models.DTOs;
using Blazored.LocalStorage;

namespace KhoaHocOnline.WebAdmin.Services
{
    public class VoucherService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public VoucherService(HttpClient httpClient, ILocalStorageService localStorage)
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

        public async Task<List<VoucherDto>> GetVouchers()
        {
            await AddAuthHeader();
            var result = await _httpClient.GetFromJsonAsync<List<VoucherDto>>("api/vouchers");
            return result ?? new List<VoucherDto>();
        }

        public async Task<bool> CreateVoucher(VoucherDto voucher)
        {
            await AddAuthHeader();
            var response = await _httpClient.PostAsJsonAsync("api/vouchers", voucher);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateVoucher(int id, VoucherDto voucher)
        {
            await AddAuthHeader();
            var response = await _httpClient.PutAsJsonAsync($"api/vouchers/{id}", voucher);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteVoucher(int id)
        {
            await AddAuthHeader();
            var response = await _httpClient.DeleteAsync($"api/vouchers/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}