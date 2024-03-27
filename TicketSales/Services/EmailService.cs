using System;
using SendGrid;
using SendGrid.Helpers.Mail;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace TicketSales.Services
{
    public class EmailService
    {

        public static async void SendEmail(string senderEmail, string recipientEmail, string subject, string body)
        {
            var apiKey = ""; // "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";
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress(senderEmail);
            var to = new EmailAddress(recipientEmail);

            var plainTextContent = body;
            var htmlContent = ""; // "<strong>Hello</strong>, this is a <em>test email</em> from SendGrid!";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            try
            {
                // Send email
                var response = await client.SendEmailAsync(msg);
                Console.WriteLine($"Status code: {response.StatusCode}");
                Console.WriteLine($"Response body: {await response.Body.ReadAsStringAsync()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }

        }
    }

}
