using System.Security.Claims;
using ApplicationLayer.Service;
using Contracts.DataLayer;
using Contracts.InfrastructureLayer;
using DomainLayer.Common;
using DomainLayer.DTO.Authentication;
using DomainLayer.DTO.Common;
using DomainLayer.Errors.AuthenticationErrors;
using Moq;

namespace Tests.ApplicationLayerTests.AccountServiceTests
{
    [TestFixture]
    public class ResetPasswordTests
    {
        private Mock<IJwtService> _jwtService;
        private Mock<IUserRepository> _userRepo;
        private Mock<ICrypterService> _crypterService;
        private Mock<IEmailService> _emailService;
        private Mock<ICaptchaVerificationService> _captchaService;

        private AccountService _accountService;
        private ClaimsPrincipal ValidTokenResponse = new ClaimsPrincipal(new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, "1")]));
        private ClaimsPrincipal ValidNoIdTokenResponse = new ClaimsPrincipal(new ClaimsIdentity([]));

        [SetUp]
        public void Setup()
        {
            _jwtService = new Mock<IJwtService>();
            _userRepo = new Mock<IUserRepository>();
            _crypterService = new Mock<ICrypterService>();
            _emailService = new Mock<IEmailService>();
            _captchaService = new Mock<ICaptchaVerificationService>();


            // Setting up Jwt Service
            _jwtService.Setup(ser => ser.VerifyResetPasswordToken("Invalid")).Returns(Response<ClaimsPrincipal>.Failure(AccountErrorHelper.TokenInvalidError()));
            _jwtService.Setup(ser => ser.VerifyResetPasswordToken("Expired")).Returns(Response<ClaimsPrincipal>.Failure(AccountErrorHelper.TokenExpiredError()));
            _jwtService.Setup(ser => ser.VerifyResetPasswordToken("Valid")).Returns(Response<ClaimsPrincipal>.Success(ValidTokenResponse));
            _jwtService.Setup(ser => ser.VerifyResetPasswordToken("ValidNoId")).Returns(Response<ClaimsPrincipal>.Success(ValidNoIdTokenResponse));
            _jwtService.Setup(ser => ser.GetClaimValue<int?>(It.Is<ClaimsPrincipal>(principle => principle.HasClaim(ClaimTypes.NameIdentifier, "1")), ClaimTypes.NameIdentifier)).Returns(1);
            _jwtService.Setup(ser => ser.GetClaimValue<int?>(It.Is<ClaimsPrincipal>(principle => !principle.HasClaim(ClaimTypes.NameIdentifier, "1")), ClaimTypes.NameIdentifier)).Returns((int?)null);

            // Setting up User Repo
            _crypterService.Setup(ser => ser.EncryptString(It.IsAny<string>())).Returns("xyz");


            _accountService = new AccountService(_jwtService.Object, _userRepo.Object, _crypterService.Object, _emailService.Object, _captchaService.Object);
        }

        [Test]
        public async Task ValidResetPasswordTokenTest_ReturnsSuccess()
        {
            //Arrange
            var request = new ResetPasswordRequest() { Password = "maaz", Token = "Valid" };
            _userRepo.Setup(ser => ser.UpdateUserPasswordAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);

            //Act
            var result = await _accountService.ResetPassword(request);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.ServiceError, Is.Null);
                Assert.That(result.Value, Is.TypeOf<MessageResponse>());
                _crypterService.Verify(ser => ser.EncryptString(request.Password), Times.Once);
                _userRepo.Verify(ser => ser.UpdateUserPasswordAsync(1, "xyz"), Times.Once);
            });
        }

        [Test]
        public async Task ValidResetPasswordTokenTest_NoId_ReturnsFailure()
        {
            //Arrange
            var expectedErrorCode = AccountErrorHelper.TokenInvalidCode;
            var request = new ResetPasswordRequest() { Password = "maaz", Token = "ValidNoId" };

            //Act
            var result = await _accountService.ResetPassword(request);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ServiceError, Is.Not.Null);
                Assert.That(result.ServiceError?.ErrorCode, Is.EqualTo(expectedErrorCode));
            });
        }

        [Test]
        public async Task InvalidTokenTest_ReturnsFailure()
        {
            //Arrange
            var expectedErrorCode = AccountErrorHelper.TokenInvalidCode;
            var request = new ResetPasswordRequest() { Password = "maaz", Token = "Invalid" };

            //Act
            var result = await _accountService.ResetPassword(request);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ServiceError, Is.Not.Null);
                Assert.That(result.ServiceError?.ErrorCode, Is.EqualTo(expectedErrorCode));
            });
        }

        [Test]
        public async Task ExpiredTokenTest_ReturnsFailure()
        {
            //Arrange
            var expectedErrorCode = AccountErrorHelper.TokenExpiredCode;
            var request = new ResetPasswordRequest() { Password = "maaz", Token = "Expired" };

            //Act
            var result = await _accountService.ResetPassword(request);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ServiceError, Is.Not.Null);
                Assert.That(result.ServiceError?.ErrorCode, Is.EqualTo(expectedErrorCode));
            });
        }
    }
}
