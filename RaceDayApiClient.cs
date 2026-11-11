using System.Text;
using System.Text.Json;
using RaceDay.Mvc.Models;

namespace RaceDay.Mvc.Services
{
    public class RaceDayApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public RaceDayApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            var baseUrl = configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5150/";
            _httpClient.BaseAddress = new Uri(baseUrl.TrimEnd('/') + "/");

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        // ==================== AUTHENTICATION ====================

        public async Task<AuthResponseDto?> LoginAsync(LoginViewModel model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/auth/login", content);

            if (!response.IsSuccessStatusCode) return null;

            var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<AuthResponseDto>(stream, _jsonOptions);
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/auth/register", content);
            return response.IsSuccessStatusCode;
        }

        // ==================== EVENTS ====================

        public async Task<List<RaceEventDto>> GetEventsAsync()
        {
            var response = await _httpClient.GetAsync("api/events");
            if (!response.IsSuccessStatusCode) return new List<RaceEventDto>();

            var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<RaceEventDto>>(stream, _jsonOptions) ?? new();
        }

        // ==================== ENROLMENTS ====================

        public async Task<bool> EnrolParticipantAsync(int eventId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsync($"api/enrolments?eventId={eventId}", null);
            return response.IsSuccessStatusCode;
        }

        // ==================== RESULTS ====================

        public async Task<string> GetResultsAsync(int eventId)
        {
            var response = await _httpClient.GetAsync($"api/results/event/{eventId}");
            if (!response.IsSuccessStatusCode) return string.Empty;

            return await response.Content.ReadAsStringAsync();
        }
    }
}