using ApplicationLayer.Service;
using Contracts.DataLayer;
using Contracts.InfrastructureLayer;
using DomainLayer.DTO.Common;
using Moq;

namespace Tests.ApplicationLayerTests.AccountServiceTests
{
    [TestFixture]
    [Category("AccountServiceTest")]
    public class DeleteUserTests
    {
        private Mock<IJwtService> _jwtService;
        private Mock<IUserRepository> _userRepo;
        private Mock<ICrypterService> _crypterService;
        private Mock<IEmailService> _emailService;
        private Mock<ICaptchaVerificationService> _captchaService;

        private AccountService _accountService;



      
        [SetUp]
        public void Setup()
        {
            _jwtService = new Mock<IJwtService>();
            _userRepo = new Mock<IUserRepository>();
            _crypterService = new Mock<ICrypterService>();
            _emailService = new Mock<IEmailService>();
            _captchaService = new Mock<ICaptchaVerificationService>();

            // Setting up User Repo
            _userRepo.Setup(ser => ser.DeleteUser(It.IsAny<int>())).Returns(Task.CompletedTask);

            _accountService = new AccountService(_jwtService.Object, _userRepo.Object, _crypterService.Object, _emailService.Object, _captchaService.Object);



        }

        [Test]
        public async Task DeleteUserTest()
        {
            // Arrange
            int id = 1;

            // Act
            var result = await _accountService.DeleteUser(id);

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
