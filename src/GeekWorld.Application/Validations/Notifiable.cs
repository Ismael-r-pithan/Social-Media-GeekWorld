
using System.Text.Json.Serialization;


namespace GeekWorld.Application.Validations
{
    public class Notifiable
    {
        [JsonIgnore]
        private List<Notification> _notifications = new();

        [JsonIgnore]
        public IReadOnlyCollection<Notification> Notifications => _notifications;

        public void AddNotification(Notification notification) => _notifications.Add(notification);

        public void AddNotifications(IEnumerable<Notification> notifications) => _notifications.AddRange(notifications);

        public bool IsValid() => !_notifications.Any();
    }
}
