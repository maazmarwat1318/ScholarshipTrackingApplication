
using System.Text.Json;
using System.Text.Json.Serialization;
using Contracts.InfrastructureLayer;
using InfrastructureLayer.Options;
using Microsoft.Extensions.Options;

namespace InfrastructureLayer.Service
{

    public class CaptchaVerificationService : ICaptchaVerificationService
    {
        private readonly CaptchaOptions _captchaOptions;
        private readonly HttpClient _httpClient;

        public CaptchaVerificationService(IOptions<CaptchaOptions> options, HttpClient httpClient)
        {
            _captchaOptions = options.Value;
            _httpClient = httpClient;
        }

        public async Task<bool> VerifyTokenAsync(string token)
        {
            var secretKey = _captchaOptions.ServerKey;
            var response = await _httpClient.PostAsync(
                $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={token}",
                null);

            if (!response.IsSuccessStatusCode) return false;

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ReCaptchaResponse>(content);

            return result?.Success ?? false;
        }

        private class ReCaptchaResponse
        {
            [JsonPropertyName("success")]
            public bool Success { get; set; }
        }
    }

}
