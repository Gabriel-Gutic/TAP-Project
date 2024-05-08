
using BlazorClient.Dto;

namespace BlazorClient.Contracts
{
    public interface IAuthService
    {
        public Task Login(AuthDto user);
        public Task Logout();
    }
}
