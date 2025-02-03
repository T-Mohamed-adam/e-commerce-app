namespace TagerProject.ServiceContracts
{
    public interface IEmailService
    {
        public Task SendEmail(string receptor, string subject, string body);
    }
}
