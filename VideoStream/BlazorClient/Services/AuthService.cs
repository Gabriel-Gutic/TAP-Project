using BlazorClient.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.Net.Http.Headers;
using BlazorClient.Dto;
using BlazorClient.Data;
using System.Text;
using System.Net.Http.Json;
using BlazorClient.MultipartAdapter;

namespace BlazorClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpService _httpService;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;
        private readonly IMultipartAdapter _multipartAdapter;


        public AuthService(IHttpService httpService, NavigationManager navigationManager, ILocalStorageService localStorageService)
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;

            _multipartAdapter = new RegisterAdapter();
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

                var success = await _httpService.PostMultipart("api/User/Insert", _multipartAdapter.Adapt(register));
                if (success)
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
