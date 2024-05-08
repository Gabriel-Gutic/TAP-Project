namespace BlazorClient.Contracts
{
    public interface IViewService
    {
        public Task CreateView(Guid videoId);
    }
}
