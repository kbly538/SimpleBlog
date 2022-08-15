using Microsoft.Extensions.Options;
using SimpleBlog.Configuration;
using SimpleBlog.Services.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Services
{
	public class EmailServiceImpl : IEmailService
	{
		private readonly SmtpSettings _smtpSettings;
		private readonly SmtpClient _client;
		public EmailServiceImpl(IOptions<SmtpSettings> options)
		{
			_smtpSettings = options.Value;
			_client = new SmtpClient(_smtpSettings.Server)
			{
				Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),

			};
		}
		public Task SendEmail(string to, string subject, string message)
		{

			string x = _smtpSettings.From;

			MailMessage mailMessage = new MailMessage(
				
				from: _smtpSettings.From,
				to: to, 
				subject: subject, 
				body: message);



			return _client.SendMailAsync(mailMessage);
		}
	}
}
