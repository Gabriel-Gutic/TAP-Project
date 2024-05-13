using BlazorClient.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.Net.Http.Headers;
using BlazorClient.Dto;
using BlazorClient.Data;

namespace BlazorClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpService _httpService;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;


        public AuthService(HttpClient httpClient, IHttpService httpService, NavigationManager navigationManager, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task Register(RegisterData register)
        {
            string? errorMessage = null;

            try
            {
                var usernameCheck = await _httpService.Get<bool?>("api/User/IsUsernameValid", "username", register.Username);
                var emailCheck = await _httpService.Get<bool?>("api/User/IsEmailValid", "email", register.Email);
                if (usernameCheck == null || emailCheck == null)
                {
                    throw new Exception("An error occured. Try again.");
                }
                
                if (usernameCheck == false)
                {
                    throw new Exception("Username already used!");
                }

                if (emailCheck == false)
                {
                    throw new Exception("Email already used!");
                }

                var content = new MultipartFormDataContent();
                content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
                if (register.ImageData != null)
                {
                    var file = new StreamContent(register.ImageData.Stream);
                    file.Headers.ContentType = register.ImageData.MediaType;
                    content.Add(file, "Image", register.ImageData.Name);
                }
                
                content.Add(new StringContent(register.Username), "Username");
                content.Add(new StringContent(register.Email), "Email");
                content.Add(new StringContent(register.Password), "Password");

                var response = await _httpService.Post<string>("api/User/Insert", content);
                if (response != null)
                {
                    var token = await _httpService.Post<TokenDto>("api/Auth/Token", new AuthDto(register.Username, register.Password));

                    if (token != null)
                    {
                        await _localStorageService.SetItem("JWTToken", token);

                        _navigationManager.NavigateTo("/", true);
                    }
                    else
                    {
                        errorMessage = "Account successfully created. Please login to your new account.";
                        _navigationManager.NavigateTo($"/login/{errorMessage}");
                    }
                }
                else
                {
                    throw new Exception("An error occured. Try again.");
                }
            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
            }

            _navigationManager.NavigateTo($"/register/{errorMessage}");
        }

        public async Task Login(AuthDto user)
        {
            string? errorMessage = null;

            try
            {
                var token = await _httpService.Post<TokenDto>("api/Auth/Token", user);

                if (token != null)
                {
                    await _localStorageService.SetItem("JWTToken", token);

                    _navigationManager.NavigateTo("/", true);
                }
                else
                {
                    errorMessage = "Authentication failed!";
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
