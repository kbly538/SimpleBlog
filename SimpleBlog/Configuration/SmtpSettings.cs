using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Configuration
{
	public class SmtpSettings
	{
		public string Server { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string From { get; set; }
	}
}
