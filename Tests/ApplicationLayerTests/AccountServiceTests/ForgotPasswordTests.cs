using ApplicationLayer.Service;
using Contracts.DataLayer;
using Contracts.InfrastructureLayer;
using DomainLayer.DTO.Authentication;
using DomainLayer.DTO.Common;
using DomainLayer.Entity;
using DomainLayer.Enums;
using DomainLayer.Errors.AuthenticationErrors;
using Moq;

namespace Tests.ApplicationLayerTests.AccountServiceTests
{
    [TestFixture]
    [Category("AccountServiceTest")]
    public class ForgotPasswordTests
    {
        private Mock<IJwtService> _jwtService;
        private Mock<IUserRepository> _userRepo;
        private Mock<ICrypterService> _crypterService;
        private Mock<IEmailService> _emailService;
        private Mock<ICaptchaVerificationService> _captchaService;

        private AccountService _accountService;



        private User UnverifiedUser = new User() { Email = "foundunverified@g.c", FirstName = "Maaz", LastName = "Khan", Password = "00000000", Id = 1, Role = Role.Student };
        private User VerifiedUser = new User() { Email = "foundverified@g.c", FirstName = "Maaz", LastName = "Khan", Password = "11111111", Id = 2, Role = Role.Moderator };

        [SetUp]
        public void Setup()
        {
            _jwtService = new Mock<IJwtService>();
            _userRepo = new Mock<IUserRepository>();
            _crypterService = new Mock<ICrypterService>();
            _emailService = new Mock<IEmailService>();
            _captchaService = new Mock<ICaptchaVerificationService>();

            // Setting up User Repo
            _userRepo.Setup(ser => ser.GetByEmailAsync("notfound@g.c")).ReturnsAsync((User?)null);
            _userRepo.Setup(ser => ser.GetByEmailAsync("foundunverified@g.c")).ReturnsAsync(UnverifiedUser);
            _userRepo.Setup(ser => ser.GetByEmailAsync("foundverified@g.c")).ReturnsAsync(VerifiedUser);

            _accountService = new AccountService(_jwtService.Object, _userRepo.Object, _crypterService.Object, _emailService.Object, _captchaService.Object) ;


        }

        [Test]
        public async Task UserNotFound_ReturnsSuccess()
        {
            // Arrange
            var request = new ForgotPasswordRequest { Email = "notfound@g.c" };
            _userRepo.Setup(repo => repo.GetByEmailAsync(request.Email))
                     .ReturnsAsync((User?)null);

            // Act
            var result = await _accountService.ForgotPassword(request);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.ServiceError, Is.Null);
                Assert.That(result.Value, Is.TypeOf<MessageResponse>());
            });
        }

        [Test]
        public async Task EmailSendingFails_ReturnsFailure()
        {
            // Arrange
            var request = new ForgotPasswordRequest { Email = "foundverified@g.c" };

            _jwtService.Setup(jwt => jwt.GenerateResetPasswordToken(2))
                       .Returns("mock-token");

            _emailService.Setup(email => email.SendPasswordResetEmail(VerifiedUser.FirstName, VerifiedUser.Email, "mock-token"))
                         .ThrowsAsync(new Exception("SMTP failed"));

            // Act
            var result = await _accountService.ForgotPassword(request);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ServiceError?.ErrorCode, Is.EqualTo(AccountErrorHelper.ResetPasswordEmailSentFailureCode));
            });
        }

        [Test]
        public async Task ValidUser_EmailSentSuccessfully_ReturnsSuccess()
        {
            // Arrange
            var user = VerifiedUser;
            var request = new ForgotPasswordRequest { Email = user.Email };

            _userRepo.Setup(repo => repo.GetByEmailAsync(user.Email))
                     .ReturnsAsync(user);

            _jwtService.Setup(jwt => jwt.GenerateResetPasswordToken(user.Id))
                       .Returns("valid-token");

            _emailService.Setup(email => email.SendPasswordResetEmail(user.FirstName, user.Email, "valid-token"))
                         .Returns(Task.CompletedTask);

            // Act
            var result = await _accountService.ForgotPassword(request);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.ServiceError, Is.Null);
                Assert.That(result.Value, Is.TypeOf<MessageResponse>());
            });
        }

        [TearDown] public void CLeanUp() { }
    }
}
