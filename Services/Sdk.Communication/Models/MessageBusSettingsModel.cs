namespace Sdk.Communication.Models
{
    public class MessageBusSettingsModel
    {
        public string Host { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }
    }
}