namespace BlazorClient.MultipartAdapter
{
    public interface IMultipartAdapter
    {
        public MultipartFormDataContent Adapt(object data);
    }
}
