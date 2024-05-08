using BlazorClient.Contracts;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System;
using BlazorClient.Dto;

namespace BlazorClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;


        public AuthService(HttpClient httpClient, NavigationManager navigationManager, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task Login(AuthDto user)
        {
            string? errorMessage = null;

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Auth/Token", user);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadFromJsonAsync<TokenDto>();

                    await _localStorageService.SetItem("JWTToken", token);

                    _navigationManager.NavigateTo("/", true);
                }
                else
                {
                    errorMessage = response.ToString();
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Error: " + ex.Message;
            }

            _navigationManager.NavigateTo($"/login/{errorMessage}");
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItem("JWTToken");

            _navigationManager.NavigateTo("/", true);
        }
    }
}
