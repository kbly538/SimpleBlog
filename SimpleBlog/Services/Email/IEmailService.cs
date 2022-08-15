using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Services.Email
{
	public interface IEmailService
	{
		Task SendEmail(string to, string subject, string message);
	}
}
