using ApplicationLayer.Service;
using Contracts.DataLayer;
using Contracts.InfrastructureLayer;
using DomainLayer.DTO.Authentication;
using DomainLayer.Entity;
using DomainLayer.Enums;
using DomainLayer.Errors;
using DomainLayer.Errors.AuthenticationErrors;
using Moq;

namespace Tests.ApplicationLayerTests.AccountServiceTests
{
    [TestFixture]
    [Category("AccountServiceTest")]
    public class LoginTests
    {
        private  Mock<IJwtService> _jwtService;
        private  Mock<IUserRepository> _userRepo;
        private  Mock<ICrypterService> _crypterService;
        private  Mock<IEmailService> _emailService;
        private  Mock<ICaptchaVerificationService> _captchaService;

        private AccountService _accountService;



        private readonly User UnverifiedUser = new() { Email = "foundunverified@g.c", FirstName = "Maaz", LastName = "Khan", Password = "00000000", Id = 1, Role = Role.Student };
        private readonly User VerifiedUser = new() { Email = "foundverified@g.c", FirstName = "Maaz", LastName = "Khan", Password = "11111111", Id = 2, Role = Role.Moderator };

        [SetUp]
        public void Setup()
        {
            _jwtService = new Mock<IJwtService>();
            _userRepo = new Mock<IUserRepository>();
            _crypterService = new Mock<ICrypterService>();
            _emailService = new Mock<IEmailService>();
            _captchaService = new Mock<ICaptchaVerificationService>();

            // Setting up Captha Service
            _captchaService.Setup(ser => ser.VerifyTokenAsync("Invalid")).ReturnsAsync(false);
            _captchaService.Setup(ser => ser.VerifyTokenAsync("Valid")).ReturnsAsync(true);

            // Setting up User Repo
            _userRepo.Setup(ser => ser.GetByEmailAsync("notfound@g.c")).ReturnsAsync((User?)null);
            _userRepo.Setup(ser => ser.GetByEmailAsync("foundunverified@g.c")).ReturnsAsync(UnverifiedUser);
            _userRepo.Setup(ser => ser.GetByEmailAsync("foundverified@g.c")).ReturnsAsync(VerifiedUser);

            _accountService = new AccountService(_jwtService.Object, _userRepo.Object, _crypterService.Object, _emailService.Object, _captchaService.Object);


        }

        [Test]
        public async Task InvalidCaptcha_ReturnsFailure()
        {
            // Arrange
            string expectedErrorCode = AccountErrorHelper.InvalidCaptchaCode;
            LogInRequest request = new LogInRequest() { CaptchaToken = "Invalid", Email = "a@g.c", Password = "11111111" };

            //Act
            var result = await _accountService.Login(request);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ServiceError, Is.Not.Null);
                Assert.That(result.ServiceError?.ErrorCode, Is.EqualTo(expectedErrorCode));
            });
        }

        [Test]
        public async Task NotFound_ReturnsFailure()
        {
            // Arrange
            string expectedErrorCode = AccountErrorHelper.InvalidCredentialsCode;
            LogInRequest request = new LogInRequest() { CaptchaToken = "Valid", Email = "notfound@g.c", Password = "00000000" };

            //Act
            var result = await _accountService.Login(request);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ServiceError, Is.Not.Null);
                Assert.That(result.ServiceError?.ErrorCode, Is.EqualTo(expectedErrorCode));
            });
        }

        [TestCase("Valid", "foundunverified@g.c", "00000000", true)]
        [TestCase("Valid", "foundunverified@g.c", "12345678", true)]
        [TestCase("Valid", "foundunverified@g.c", "00000000", false)]
        [TestCase("Valid", "foundunverified@g.c", "12345678", false)]
        public async Task UnverifiedValidUser_ValidAndInvalidPassword_ReturnsFailure(string captchaToken, string email, string password, bool emailServiceSuccess = true)
        {
            // Arrange
            string expectedErrorCode = emailServiceSuccess ? AccountErrorHelper.AccountNotActivatedCode : CommonErrorHelper.ServerErrorCode;

            var request = new LogInRequest
            {
                CaptchaToken = captchaToken,
                Email = email,
                Password = password
            };

            var unverifiedUser = UnverifiedUser;

            _jwtService.Setup(j => j.GenerateResetPasswordToken(unverifiedUser.Id)).Returns("dummy-token");
            if(emailServiceSuccess)
            {
                _emailService.Setup(e => e.SendPasswordResetEmail(unverifiedUser.FirstName, unverifiedUser.Email, "dummy-token"))
                         .Returns(Task.CompletedTask);
            } else
            {
                _emailService.Setup(e => e.SendPasswordResetEmail(unverifiedUser.FirstName, unverifiedUser.Email, "dummy-token"))
                         .ThrowsAsync(new Exception());
            }


            // Act
            var result = await _accountService.Login(request);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ServiceError, Is.Not.Null);
                Assert.That(result.ServiceError?.ErrorCode, Is.EqualTo(expectedErrorCode));
            });

            //Clean up
            _emailService.Reset();
            _jwtService.Reset();

        }

        [TestCase("Valid", "foundverified@g.c", "00000000")]
        public async Task VerifiedValidUser_ReturnsToken(string captchaToken, string email, string password)
        {
            // Arrange

            var expectedToken = "dummy-token";

            var request = new LogInRequest
            {
                CaptchaToken = captchaToken,
                Email = email,
                Password = password
            };

            var verifiedUser = VerifiedUser;

            _jwtService.Setup(j => j.GenerateAccessToken(verifiedUser.Id, verifiedUser.FirstName, verifiedUser.Email, verifiedUser.Role)).Returns(expectedToken);
            _crypterService.Setup(ser => ser.CompareHash(password, verifiedUser.Password)).Returns(true);

            // Act
            var result = await _accountService.Login(request);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.ServiceError, Is.Null);
                Assert.That(result.Value, Is.TypeOf<LogInResponse>());
                Assert.That(result.Value?.Token, Is.EqualTo(expectedToken));
            });

            //Clean up
            _crypterService.Reset();
            _jwtService.Reset();

        }

        [TestCase("Valid", "foundverified@g.c", "12345678")]
        public async Task VerifiedValidUser_InvalidPassword_ReturnsFailure(string captchaToken, string email, string password)
        {
            // Arrange

            var expectedErrorCode = AccountErrorHelper.InvalidCredentialsCode;

            var request = new LogInRequest
            {
                CaptchaToken = captchaToken,
                Email = email,
                Password = password
            };

            var verifiedUser = VerifiedUser;

            _crypterService.Setup(ser => ser.CompareHash(password, verifiedUser.Password)).Returns(false);
            
            // Act
            var result = await _accountService.Login(request);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ServiceError, Is.Not.Null);
                Assert.That(result.ServiceError?.ErrorCode, Is.EqualTo(expectedErrorCode));
                Assert.That(_jwtService.Invocations, Is.Empty);
            });

            //Clean up
            _crypterService.Reset();
            _jwtService.Reset();

        }

        [TearDown] public void CLeanUp() { }
    }
}
