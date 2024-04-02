using CodingAssessment.Domain.Interfaces;
using CodingAssessment.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CodingAssessment.Domain;

namespace CodingAssessment.Infrastructure.Repositories
{
    public class SmtpClientRepository: ISmtpClientRepository
    {
        private readonly IOptions<Settings> _options;
        private readonly string SmtpHost;
        private readonly int Port;
        public SmtpClientRepository(IOptions<Settings> options)
        {
            _options = options;
            SmtpHost = _options.Value.SmtpHost;
            Port = _options.Value.Port;
        }
        public async Task SendEmailAsync(string emailObject)
        {
            var fromAddress = new MailAddress(Constants.FromEmailAddress, Constants.FromEmailName);
            var toAddress = new MailAddress(Constants.ToEmailAddress,Constants.ToEmailName);
            const string fromPassword = Constants.FromEmailPassword;
            const string subject = Constants.Subject;

            var smtpClient = new SmtpClient
            {
                Host = SmtpHost,
                Port = Port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = emailObject
            };

            await smtpClient.SendMailAsync(message);
            Console.WriteLine("Email sent successfully.");
        }
    }
}
