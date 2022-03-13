namespace FlightManager.Services.Messaging
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// This class calls the email sender method that has the contents of the email.
    /// </summary>
    public interface IEmailSender
    {
        Task SendEmailAsync(
            string toMail,
            string subject,
            string messageBody);
    }
}
