using BlazorClient.Contracts;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using BlazorClient.Dto;
using BlazorClient.Pages.AuthPages;
using Microsoft.AspNetCore.Components;

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

        public async Task<T?> Get<T>(string uri, string key1, object value1, string key2, object value2)
        {
            string _uri = uri;
            if (string.IsNullOrEmpty(key1) || value1 == null || string.IsNullOrEmpty(key2) || value2 == null)
            {
                throw new ArgumentException("Invalid argument");
            }
            _uri += "?" + key1 + "=" + value1 + "&" + key2 + "=" + value2;
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

        public async Task<T?> Patch<T>(string uri, object content)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, uri);

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

        public async Task<T?> DeleteContent<T>(string uri, object content)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);

            // set request body
            request.Content = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");

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
            HttpResponseMessage? response = null;
            try
            {
                response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode) 
                {
                    return await response.Content.ReadFromJsonAsync<T>();
                }
                return default;
            }
            catch
            {
                return default;
            }
        }

        public async Task<bool> PostMultipart(string uri, MultipartFormDataContent content)
        {
            HttpResponseMessage? response = null;
            try
            {
                response = await _httpClient.PostAsync(uri, content);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
