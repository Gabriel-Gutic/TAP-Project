using BlazorClient.Contracts;
using Microsoft.Extensions.Logging;

namespace BlazorClient.Events
{
    public class EventSetup : IEventSetup
    {
        private readonly IServiceProvider _serviceProvider;

        public EventSetup(IServiceProvider serviceProvider) 
        { 
            _serviceProvider = serviceProvider;
        }

        public void Setup(IEventController controller)
        {
            SetupSubscribeEvent(controller);
            SetupVideoUploadEvent(controller);
        }

        private void SetupSubscribeEvent(IEventController controller)
        {
            controller.AddEvent("Subscribe", async (object args) =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var httpService = scope.ServiceProvider.GetRequiredService<IHttpService>();
                    await httpService.Post<string>("api/Notification/Subscriber", args);
                }
            });
        }

        private void SetupVideoUploadEvent(IEventController controller)
        {
            controller.AddEvent("VideoUpload", async (object args) =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var httpService = scope.ServiceProvider.GetRequiredService<IHttpService>();
                    await httpService.Post<string>("api/Notification/VideoUpload", args);
                }
            });
        }
    }
}
