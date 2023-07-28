namespace GeekWorld.Application.Validations
{
    public class Errors
    {
        public IReadOnlyCollection<Notification> Notifications { get; set; }

        public Errors(IReadOnlyCollection<Notification> notifications)
            => Notifications = notifications;

        public Errors(Notification notification)
            => Notifications = new List<Notification> { notification };
    }
}
