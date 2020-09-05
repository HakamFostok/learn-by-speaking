using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LearnBySpeaking.Domain.Core
{
    public interface ISendEmailService : IDisposable
    {
        Task SendEmailAsync(string subject, string body, List<string> receivers, List<string> ccReceivers = null, List<MailAttachment> attachments = null);

        Task SendEmailAsync(string subject, string body, string receiver, List<string> ccReceivers = null, List<MailAttachment> attachments = null) =>
            SendEmailAsync(subject, body, new List<string> { receiver }, ccReceivers, attachments);
    }

    public class MailAttachment
    {
        public Stream Content { get; set; }
        public string Name { get; set; }
        public string MediaType { get; set; }
    }
}