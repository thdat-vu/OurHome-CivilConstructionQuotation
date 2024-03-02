
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;

namespace SWP391.CHCQS.Utility.Helpers
{
	public class EmailSender : IEmailSender
	{
		public string SendGridSecret { get; set; }
		private readonly IConfiguration _configuration;

		public EmailSender(IConfiguration _config)
		{
			SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");
			_configuration = _config;
		}

		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			//var emailToSend = new MimeMessage();
			//emailToSend.From.Add(MailboxAddress.Parse("hello@dotnetmastery.com"));
			//emailToSend.To.Add(MailboxAddress.Parse(email));
			//emailToSend.Subject = subject;
			//emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html){ Text = htmlMessage};

			////send email
			//using (var emailClient = new SmtpClient())
			//{
			//    emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
			//    emailClient.Authenticate("dotnetmastery@gmail.com", "DotNet123$");
			//    emailClient.Send(emailToSend);
			//    emailClient.Disconnect(true);
			//}

			//return Task.CompletedTask;

			var client = new SendGridClient(SendGridSecret);
			var from = new EmailAddress("hello@dotnetmastery.com", "Bulky Book");
			var to = new EmailAddress(email);
			var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
			return client.SendEmailAsync(msg);

		}
		/*
		public static Task SendInfoToEmail(string toEmail, string subject, string htmlMessage)
		{

			string fromMail = "ourhomeswp391@gmail.com";
			string fromPassword = "kusvqhpurbksspvb";

			MailMessage mess = new MailMessage();
			mess.From = new MailAddress(fromMail);
			mess.Subject = subject;
			mess.To.Add(new MailAddress(toEmail));
			mess.Body = htmlMessage;

			mess.IsBodyHtml = true;

			var smtp = new SmtpClient("smtp.gmail.com")
			{
				Port = 587,
				Credentials = new NetworkCredential(fromMail, fromPassword),
				EnableSsl = true
			};
			smtp.Send(mess);
			return Task.CompletedTask;
		}
		*/
		/* HOW TO USE BODY BUILDER TO RENDER HTML
				var builder = new BodyBuilder();
		 Set the html version of the message text
			builder.HtmlBody = string.Format(@"<p>Hey Alice,<br>
		<p>What are you up to this weekend? Monica is throwing one of her parties on
		Saturday and I was hoping you could make it.<br>
		<p>Will you be my +1?<br>
		<p>-- Joey<br>
		<center><img src=""cid:{0}""></center>", image.ContentId);
		We may also want to attach a calendar event for Monica's party...
		builder.Attachments.Add(@"C:\Users\Joey\Documents\party.ics");

		 Now we just need to set the message body and we're done
		 message.Body = builder.ToMessageBody();
		*/
		public  Task SendInfoToEmail(string toEmail, string customerName, string quoteId)
		{
			var fromMail = _configuration["MailSettings:Mail"];
			var fromPassword = _configuration["MailSettings:FromKeyPassword"];
            MailMessage mess = new MailMessage();
			mess.From = new MailAddress(fromMail);
			mess.Subject = _configuration["MailSettings:Subject"];
            mess.To.Add(new MailAddress(toEmail));

			string url = $"{_configuration["Environment:LocalDomain"]}{_configuration["MailSettings:ActionLinkNoPara"]}?quoteId={quoteId}";
			string mailContent = $@"
<p>Welcome {customerName} !</p>
<p>Thank you for reaching out to us regarding the quotation request. Your inquiry has been received and is currently under review by our team.</p>
<p>We will endeavor to provide you with a response as promptly as possible, ensuring thorough consideration of your requirements.</p>
<p>Once again, we extend our gratitude for choosing our services. Wishing you a splendid day ahead!</p>
    
<p>For further information, please visit <a href='{url}'>here</a>.</p>
    
<p>Yours sincerely,</p>
<p>Our Home Architecture</p>
";
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = String.Format(mailContent); 
			mess.Body = bodyBuilder.HtmlBody;

			mess.IsBodyHtml = true;

			var smtp = new SmtpClient("smtp.gmail.com")
			{
				Port = 587,
				Credentials = new NetworkCredential(fromMail, fromPassword),
				EnableSsl = true
			};
			smtp.Send(mess);
			return Task.CompletedTask;
		}

	}
}
