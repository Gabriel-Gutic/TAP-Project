
using BlazorClient.Data;
using BlazorClient.Dto;

namespace BlazorClient.Contracts
{
    public interface IAuthService
    {
        public Task Register(RegisterData registerDto);
        public Task Login(AuthDto user);
        public Task Logout();
    }
}
