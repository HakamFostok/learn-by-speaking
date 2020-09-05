using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LearnBySpeaking.Domain.Core
{
    public class SendEmailService : ISendEmailService
    {
        private const string fromAddress = "destek@biteg.net";

        private readonly SmtpClient _smtpClient;

        public SendEmailService()
        {
            _smtpClient = new SmtpClient
            {
                Port = 587,
                Host = "mail.biteg.net",
                Credentials = new NetworkCredential("destek@biteg.net", "destek2018")
            };
        }

        public async Task SendEmailAsync(string subject, string body, List<string> receivers, List<string> ccReceivers = null, List<MailAttachment> attachments = null)
        {
            if (receivers == null)
                throw new ArgumentNullException(nameof(receivers));

            if (receivers.Count == 0)
                throw new ArgumentException("Must be at least one receiver", nameof(receivers));

            if (string.IsNullOrEmpty(body))
                throw new ArgumentNullException(nameof(body));

            string to = string.Join(",", receivers);

            using MailMessage message = new MailMessage(fromAddress, to, subject, body)
            {
                IsBodyHtml = true,
            };

            if (ccReceivers != null)
                foreach (string cc in ccReceivers)
                    message.CC.Add(cc);

            if (attachments != null)
                foreach (MailAttachment attachment in attachments)
                    message.Attachments.Add(new Attachment(attachment.Content, attachment.Name, attachment.MediaType));

            await _smtpClient.SendMailAsync(message);
        }

        public void Dispose()
        {
            _smtpClient?.Dispose();
        }
    }
}
