
using System.Text;
using Contracts.InfrastructureLayer;
using InfrastructureLayer.Options;
using Microsoft.Extensions.Options;
using System.Net;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace InfrastructureLayer.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailServiceOptions _emailServiceOptions;
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public EmailService(IOptions<EmailServiceOptions> emailServiceOptions, HttpClient httpClient, ILogger<EmailService> logger)
        {
            _emailServiceOptions = emailServiceOptions.Value;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task SendPasswordResetEmail(string username, string useremail, string resetToken)
        {
            try
            {
                var requestUrl = _emailServiceOptions.Host;
                var token = _emailServiceOptions.ApiToken;

                var emailData = new
                {
                    from = new { email = "hello@demomailtrap.co", name = "Scholarship Tracking Application" },
                    to = new[] { new { email = "m.maaz.khan.1318@gmail.com" } },
                    subject = "Reset Password",
                    text = "Click to reset your password!",
                    category = "Integration Test",
                    html = GetResetPasswordHTMLTemplate(username, resetToken)
                };

                string jsonData = JsonSerializer.Serialize(emailData);

                StringContent content = new(jsonData, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Post, requestUrl)
                {
                    Content = content
                };
                request.Headers.Add("Authorization", "Bearer " + token);

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error occured at ${nameof(EmailService)} in ${nameof(SendPasswordResetEmail)}");
                throw;
            }
        }

        private string GetResetPasswordHTMLTemplate(string username, string resetToken)
        {
            return
            $"""
            <!DOCTYPE html>
            <html>
            <head>
              <meta charset="UTF-8">
              <title>Reset Password</title>
            </head>
            <body style="font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;">

              <div style="max-width: 600px; margin: auto; background: #ffffff; padding: 20px; text-align: center; border-radius: 6px;">
                <h2 style="color: #333;">Reset Your Password</h2>
                <p style="color: #555;">Hi {username}, <br>Click the button below to reset your password:</p>

                <a href="{_emailServiceOptions.ResetPasswordUrl}?token={resetToken}"
                   style="display: inline-block; cursor: pointer; padding: 12px 20px; background-color: #007bff; color: #ffffff; text-decoration: none; border-radius: 4px; margin-top: 20px;">
                  Reset Password
                </a>
              </div>

            </body>
            </html>
            
            """;
        }
    }
}
