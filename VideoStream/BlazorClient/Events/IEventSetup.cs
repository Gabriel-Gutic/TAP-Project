namespace BlazorClient.Events
{
    public interface IEventSetup
    {
        public void Setup(IEventController controller);
    }
}
