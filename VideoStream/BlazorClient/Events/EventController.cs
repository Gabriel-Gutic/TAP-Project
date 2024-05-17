using BlazorClient.Contracts;

namespace BlazorClient.Events
{
    public class EventController : IEventController
    {
        private Dictionary<string, Func<object, Task>> _events;

        public EventController(IEventSetup eventSetup)
        {
            _events = new Dictionary<string, Func<object, Task>>();

            eventSetup.Setup(this);
        }

        public void AddEvent(string name, Func<object, Task> action)
        {
            if (_events.ContainsKey(name))
            {
                throw new Exception($"Event with name '{name}' already exists");
            }

            _events[name] = action;
        }

        public void RemoveEvent(string name)
        {
            _checkEventExistence(name);

            _events.Remove(name);
        }

        public async Task Invoke(string name, object? args = null)
        {
            _checkEventExistence(name);

            await _events[name](args);
        }
        
        private void _checkEventExistence(string name)
        {
            if (!_events.ContainsKey(name))
            {
                throw new Exception($"Event with name '{name}' doesn't exist");
            }
        }
    }
}
