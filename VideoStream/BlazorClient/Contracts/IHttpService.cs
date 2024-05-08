namespace BlazorClient.Contracts
{
    public interface IHttpService
    {
        public string GetAPI();
        public Task<T?> Get<T>(string uri, string? key = null, object? value = null);
        public Task<T?> Post<T>(string uri, object content);
        public Task<T?> Delete<T>(string uri, Guid id);
    }
}
