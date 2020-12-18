namespace Sdk.Auth.Models
{
    public class Auth0SettingsModel
    {
        public string Domain { get; set; }
        public string Audience { get; set; }
        public string[] Policies { get; set; }
    }
}