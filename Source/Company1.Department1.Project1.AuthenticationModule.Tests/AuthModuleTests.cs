using Company1.Department1.Project1.AuthenticationModule.Modules;
using Company1.Department1.Project1.Services.Authentication.Interfaces;
using Company1.Department1.Project1.Services.Authentication.Models;
using Company1.Department1.Project1.Services.Dependency;
using Company1.Department1.Project1.Services.Helper.Interfaces;
using Company1.Department1.Project1.Services.Helper.Models;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using System.Web;

namespace Company1.Department1.Project1.AuthenticationModule.Tests
{
    [TestClass]
    public class AuthModuleTests
    {
        private AuthModule _Sut;
        private Mock<IAuthenticationService> _AuthenticationServiceMock;
        private Mock<IHelperService> _HelperServiceMock;
        private Mock<IAuthenticationConfig> _AuthenticationConfigMock;
        private TokenResult _TokenResult;

        [TestInitialize]
        public void Init()
        {
            _AuthenticationServiceMock = new Mock<IAuthenticationService>();
            _HelperServiceMock = new Mock<IHelperService>();
            _AuthenticationConfigMock = new Mock<IAuthenticationConfig>();

            var config = _AuthenticationConfigMock.Object;

            _AuthenticationConfigMock.Setup(x => x.AppId).Returns("id");
            _AuthenticationConfigMock.Setup(x => x.ApiKey).Returns("key");
            _AuthenticationConfigMock.Setup(x => x.LoginUrl).Returns("loginurl");
            _AuthenticationConfigMock.Setup(x => x.HomePageUrl).Returns("homepageurl");
            _AuthenticationConfigMock.Setup(x => x.RestApiUrl).Returns("restapiurl");
            _AuthenticationConfigMock.Setup(x => x.LogoutAction).Returns("logout");
            _AuthenticationConfigMock.Setup(x => x.GetTokenUrl(It.IsAny<String>())).Returns("tokenurl");

            _TokenResult = new TokenResult() { UserName = "abc", Token = "bcd", Roles = "admin" };

            _AuthenticationServiceMock.Setup(x => x.UpdateExpiredTokenAsync(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>())).Returns(Task.FromResult(0));
            _AuthenticationServiceMock.Setup(x => x.GetTokenInfoAsync(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>())).Returns(Task.FromResult(_TokenResult));

            _HelperServiceMock.Setup(x => x.GetCurrentToken(It.IsAny<HttpContext>(), It.IsAny<IAuthenticationConfig>())).Returns("bcd");
            _HelperServiceMock.Setup(x => x.IsLogoutRequest(It.IsAny<HttpContext>(), It.IsAny<IAuthenticationConfig>())).Returns(false);

            _HelperServiceMock.Setup(x => x.ProcessLogoutRequest(It.IsAny<HttpContext>(), It.IsAny<IAuthenticationConfig>())).Verifiable();
            _HelperServiceMock.Setup(x => x.ProcessToken(It.IsAny<HttpContext>(), It.IsAny<IAuthenticationConfig>(), It.IsAny<TokenResult>())).Verifiable();
            _HelperServiceMock.Setup(x => x.RedirectForAuthentication(It.IsAny<HttpContext>(), It.IsAny<IAuthenticationConfig>())).Verifiable();

            DependencyService.DependencyContainer.RegisterInstance<IAuthenticationService>(_AuthenticationServiceMock.Object);
            DependencyService.DependencyContainer.RegisterInstance<IHelperService>(_HelperServiceMock.Object);
            DependencyService.DependencyContainer.RegisterInstance<IAuthenticationConfig>(_AuthenticationConfigMock.Object);
        }

        [TestMethod]
        public void AuthModule_AuthenticateAsync_WithNoToken()
        {
            // Arrange
            _HelperServiceMock.Setup(x => x.GetCurrentToken(It.IsAny<HttpContext>(), It.IsAny<IAuthenticationConfig>())).Returns((String)null);
            _Sut = new AuthModule();

            // Act
            _Sut.AuthenticateAsync(new HttpApplication(), EventArgs.Empty).Wait();

            // Assert
            var config = DependencyService.DependencyContainer.Resolve<IAuthenticationConfig>();
            _HelperServiceMock.Verify(x => x.RedirectForAuthentication(null, config), Times.Exactly(1));

        }

        [TestMethod]
        public void AuthModule_AuthenticateAsync_WithTokenNotLogoutRequest()
        {
            // Arrange
            _Sut = new AuthModule();

            // Act
            _Sut.AuthenticateAsync(new HttpApplication(), EventArgs.Empty).Wait();

            // Assert
            _AuthenticationServiceMock.Verify(x => x.GetTokenInfoAsync("id", "key", "tokenurl"), Times.Exactly(1));
            var config = DependencyService.DependencyContainer.Resolve<IAuthenticationConfig>();
            _HelperServiceMock.Verify(x => x.ProcessToken(null, config, _TokenResult), Times.Exactly(1));
            
        }

        [TestMethod]
        public void AuthModule_AuthenticateAsync_WithTokenLogoutRequest()
        {
            // Arrange
            _HelperServiceMock.Setup(x => x.IsLogoutRequest(It.IsAny<HttpContext>(), It.IsAny<IAuthenticationConfig>())).Returns(true);
            _Sut = new AuthModule();

            // Act
            _Sut.AuthenticateAsync(new HttpApplication(), EventArgs.Empty).Wait();

            // Assert
            _AuthenticationServiceMock.Verify(x => x.UpdateExpiredTokenAsync("id", "key", "tokenurl"), Times.Exactly(1));
            var config = DependencyService.DependencyContainer.Resolve<IAuthenticationConfig>();
            _HelperServiceMock.Verify(x => x.ProcessLogoutRequest(null, config), Times.Exactly(1));

        }
    }
}
