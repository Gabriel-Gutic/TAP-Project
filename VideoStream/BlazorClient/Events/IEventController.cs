namespace BlazorClient.Events
{
    public interface IEventController
    {
        public void AddEvent(string name, Func<object, Task> action);
        public void RemoveEvent(string name);
        public Task Invoke(string name, object? args = null);
    }
}
