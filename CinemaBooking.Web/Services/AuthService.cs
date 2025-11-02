using CinemaBooking.Shared.DTOs;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace CinemaBooking.Web.Services
{
    public class AuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public string? AuthToken { get; private set; }
        public bool IsLoggedIn => !string.IsNullOrEmpty(AuthToken);

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> LoginAsync(LoginDto loginModel)
        {
            var http = _httpClientFactory.CreateClient("MyApi");
            var response = await http.PostAsJsonAsync("api/account/login", loginModel);

            if (!response.IsSuccessStatusCode)
            {
                AuthToken = null;
                return false;
            }

            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (loginResponse?.Token == null)
            {
                AuthToken = null;
                return false;
            }

            AuthToken = loginResponse.Token;
            return true;
        }

        public void Logout()
        {
            AuthToken = null;
        }

        public void SetAuthHeader(HttpClient client)
        {
            if (IsLoggedIn)
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AuthToken);
            }
        }

        private class LoginResponse
        {
            [JsonPropertyName("token")]
            public string? Token { get; set; }
        }
    }
}