using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.AspNetCore.Identity.UI.Services;
using log4net;

namespace BDFirst.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private static readonly ILog log = LogManager.GetLogger(typeof(EmailSender));


        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                           ILogger<EmailSender> logger,
                           IConfiguration config)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
            _config = config;
        }

        public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var key = _config["ConnectionStrings:ServiceApiKey"];
            //            if (string.IsNullOrEmpty(Options.SendGridKey))
            if (string.IsNullOrEmpty(key))

            {
                throw new Exception("Null SendGridKey");
            }
            await Execute(key, subject, message, toEmail);
        }

        public async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            try
            {
                var client = new SendGridClient(apiKey);
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("Joe@contoso.com", "Password Recovery"),
                    Subject = subject,
                    PlainTextContent = message,
                    HtmlContent = message
                };
                msg.AddTo(new EmailAddress(toEmail));

                // Disable click tracking.
                // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
                msg.SetClickTracking(false, false);
                var response = await client.SendEmailAsync(msg);
                _logger.LogInformation(response.IsSuccessStatusCode
                                       ? $"Email to {toEmail} queued successfully!"
                                       : $"Failure Email to {toEmail}");
            }
            catch (Exception ex) {
                log.Error(ex.InnerException != null ? string.Format("Inner Exception: {0} --- Exception: {1}", ex.InnerException.Message, ex.Message) : ex.Message, ex);

            }
        }
    }
}
