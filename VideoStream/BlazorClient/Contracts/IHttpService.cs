namespace BlazorClient.Contracts
{
    public interface IHttpService
    {
        public string GetAPI();
        public Task<T?> Get<T>(string uri, string? key = null, object? value = null);
        public Task<T?> Get<T>(string uri, string key1, object value1, string key2, object value2);
        public Task<T?> Post<T>(string uri, object content);
        public Task<T?> Patch<T>(string uri, object content);
        public Task<T?> Delete<T>(string uri, Guid id);
        public Task<T?> DeleteContent<T>(string uri, object content);
        public Task<bool> PostMultipart(string uri, MultipartFormDataContent content);
    }
}
