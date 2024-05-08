using BlazorClient.Contracts;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using BlazorClient.Dto;

namespace BlazorClient.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public HttpService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<T?> Get<T>(string uri, string? key = null, object? value = null)
        {
            string _uri = uri;
            if (!string.IsNullOrEmpty(key) && value != null)
            {
                _uri += "?" + key + "=" + value;
            }
            var request = new HttpRequestMessage(HttpMethod.Get, _uri);
            
            return await SendRequest<T>(request);
        }

        public string GetAPI()
        {
            return _httpClient.BaseAddress.ToString();
        }

        public async Task<T?> Post<T>(string uri, object content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);

            // set request body
            request.Content = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");

            return await SendRequest<T>(request);
        }

        public async Task<T?> Delete<T>(string uri, Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{uri}?id={{{id}}}");

            // set request body
            return await SendRequest<T>(request);
        }

        private async Task<T?> SendRequest<T>(HttpRequestMessage request)
        {
            // add authorization header
            TokenDto? token = await _localStorageService.GetItem<TokenDto>("JWTToken");
            if (token != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
            }

            // send request
            try
            {
                using var response = await _httpClient.SendAsync(request);
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch
            {
                return default;
            }
        }
    }
}
