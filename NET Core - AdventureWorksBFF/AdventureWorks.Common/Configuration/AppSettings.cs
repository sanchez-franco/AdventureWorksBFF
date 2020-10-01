namespace AdventureWorks.Common.Configuration
{
    public class AppSettings
    {
        public string AuthenticationUrl { get; set; }
        public string PersonUrl { get; set; }
        public string SecretKey { get; set; }
        public int MinutesToExpireToken { get; set; }
    }
}