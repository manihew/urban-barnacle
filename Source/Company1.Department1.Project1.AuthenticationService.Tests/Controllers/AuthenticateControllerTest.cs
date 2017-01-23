using Company1.Department1.Project1.AuthenticationService.Controllers;
using Company1.Department1.Project1.AuthenticationService.Interfaces;
using Company1.Department1.Project1.AuthenticationService.Models;
using Company1.Department1.Project1.AuthenticationService.Services;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Company1.Department1.Project1.AuthenticationService.Tests.Controllers
{
    [TestClass]
    public class AuthenticateControllerTest
    {
        private AuthenticateController _Sut;
        private Mock<IUserRepository> _UserRepositoryMock;
        private Mock<ILdapRepository> _LdapRepositoryMock;
        private Mock<ITokenRepository> _TokenRepositoryMock;

        [TestInitialize]
        public void Init()
        {
            _UserRepositoryMock = new Mock<IUserRepository>();
            _LdapRepositoryMock = new Mock<ILdapRepository>();
            _TokenRepositoryMock = new Mock<ITokenRepository>();

            _UserRepositoryMock.Setup(x => x.SaveAuthenticationAsync(It.IsAny<User>())).Returns(Task.FromResult(0));
            _LdapRepositoryMock.Setup(x => x.ValidateUser(It.IsAny<String>(), It.IsAny<String>())).Returns(true);
            _TokenRepositoryMock.Setup(x => x.SaveTokenAsync(It.IsAny<User>())).Returns(Task.FromResult(0));

            DependencyService.DependencyContainer.RegisterInstance<IUserRepository>(_UserRepositoryMock.Object);
            DependencyService.DependencyContainer.RegisterInstance<ITokenRepository>(_TokenRepositoryMock.Object);
            DependencyService.DependencyContainer.RegisterInstance<ILdapRepository>(_LdapRepositoryMock.Object);
        }

        [TestMethod]
        public void AuthenticateController_Post_HappyPath()
        {
            // Arrange
            _Sut = new AuthenticateController();

            // Act
            var user = new User() { UserName = "abc", Password = "bcd", Token = "ad" };
            var actual = _Sut.Post(user).Result;

            // Assert
            Assert.AreEqual("ad", actual.Token);
            Assert.AreEqual(true, actual.IsAuthenticated);
            _LdapRepositoryMock.Verify(x => x.ValidateUser("abc", "bcd"), Times.Exactly(1));
            _UserRepositoryMock.Verify(x => x.SaveAuthenticationAsync(user), Times.Exactly(1));
            _TokenRepositoryMock.Verify(x => x.SaveTokenAsync(user), Times.Exactly(1));
        }

        [TestMethod]
        public void AuthenticateController_Post_FailedAuthentication()
        {
            // Arrange
            _LdapRepositoryMock.Setup(x => x.ValidateUser(It.IsAny<String>(), It.IsAny<String>())).Returns(false);
            _Sut = new AuthenticateController();

            // Act
            var user = new User() { UserName = "abc", Password = "bcd", Token = "ad" };
            var actual = _Sut.Post(user).Result;

            // Assert
            Assert.AreEqual("ad", actual.Token);
            Assert.AreEqual(false, actual.IsAuthenticated);
            _LdapRepositoryMock.Verify(x => x.ValidateUser("abc", "bcd"), Times.Exactly(1));
            _UserRepositoryMock.Verify(x => x.SaveAuthenticationAsync(user), Times.Exactly(0));
            _TokenRepositoryMock.Verify(x => x.SaveTokenAsync(user), Times.Exactly(0));
        }
    }
}
