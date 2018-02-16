namespace Arkitektum.Orden.Services
{
    public class AppSettings
    {
        public EmailSettings EmailSettings { get; set; }
    }

    public class EmailSettings
    {
        public string FromAddress { get; set; }
        public string SendgridApiKey { get; set; }
    }
}