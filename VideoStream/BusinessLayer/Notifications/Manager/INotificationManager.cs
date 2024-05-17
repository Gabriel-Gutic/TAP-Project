using BusinessLayer.Notifications.Types;


namespace BusinessLayer.Notifications.Manager
{
    // Singleton
    public interface INotificationManager
    {
        public void Send(BaseNotification notification);
    }
}
