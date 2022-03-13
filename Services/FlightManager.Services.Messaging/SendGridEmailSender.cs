namespace FlightManager.Services.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    using SendGrid;
    using SendGrid.Helpers.Mail;
    using static FlightManager.Common.GlobalConstants;

    /// <summary>
    /// This class creates an email and sends it to the desired person/organization.
    /// </summary>
    public class SendGridEmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string toMail, string subject, string messageBody)
        {
            var apiKey = Email.ApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(Email.SystemEmail, SystemName);
            var to = new EmailAddress(toMail);

            var msg = MailHelper.CreateSingleEmail(
                from,
                to,
                subject,
                messageBody,
                messageBody);

            await client.SendEmailAsync(msg);
        }
    }
}
