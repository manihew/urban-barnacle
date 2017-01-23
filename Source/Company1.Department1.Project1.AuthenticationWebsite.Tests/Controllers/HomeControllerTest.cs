using Company1.Department1.Project1.AuthenticationWebsite.Controllers;
using Company1.Department1.Project1.AuthenticationWebsite.Models;
using Company1.Department1.Project1.Services.Authentication.Interfaces;
using Company1.Department1.Project1.Services.Authentication.Models;
using Company1.Department1.Project1.Services.Dependency;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Company1.Department1.Project1.AuthenticationWebsite.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController _Sut;
        private Mock<IAuthenticationService> _AuthenticationServiceMock;

        [TestInitialize]
        public void Init()
        {
            _AuthenticationServiceMock = new Mock<IAuthenticationService>();

            var result = new AuthenticationResult() { IsAuthenticated = true, Token = "abc" };

            _AuthenticationServiceMock.Setup(x => x.AuthenticateAsync(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>()))
                                        .Returns(Task.FromResult(result));

            DependencyService.DependencyContainer.RegisterInstance<IAuthenticationService>(_AuthenticationServiceMock.Object);
        }

        [TestMethod]
        public void HomeController_Get_Index_HappyPath()
        {
            // Arrange
            _Sut = new HomeController();

            // Act
            var actual = _Sut.Index("returnurl", "auth");

            // Assert
            Assert.AreEqual(typeof(ViewResult), actual.GetType());
            Assert.AreEqual("returnurl", _Sut.ViewBag.ReturnUrl);
            Assert.AreEqual("auth", _Sut.ViewBag.Auth);
        }

        [TestMethod]
        public void HomeController_Post_Index_HappyPath()
        {
            // Arrange
            Config.AppId = "id";
            Config.ApiKey = "key";
            Config.AuthenticationServiceUrl = "authurl";
            var user = new LogonViewModel() { UserName ="user", Password = "pwd"};
            _Sut = new HomeController();

            // Act
            var actual = _Sut.Index(user, "returnurl", "auth").Result;

            // Assert
            Assert.AreEqual(typeof(RedirectResult), actual.GetType());
            Assert.AreEqual("returnurl", ((RedirectResult)actual).Url);
            _AuthenticationServiceMock.Verify(x => x.AuthenticateAsync("id", "key", "authurl", "user", "pwd", "auth"), Times.Exactly(1));
        }

        [TestMethod]
        public void HomeController_Post_Index_FailedAuthentication()
        {
            // Arrange
            var result = new AuthenticationResult() { IsAuthenticated = false, Token = "abc" };

            _AuthenticationServiceMock.Setup(x => x.AuthenticateAsync(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>()))
                                        .Returns(Task.FromResult(result));

            Config.AppId = "id";
            Config.ApiKey = "key";
            Config.AuthenticationServiceUrl = "authurl";
            var user = new LogonViewModel() { UserName = "user", Password = "pwd" };
            _Sut = new HomeController();

            // Act
            var actual = _Sut.Index(user, "returnurl", "auth").Result;

            // Assert
            Assert.AreEqual(typeof(ViewResult), actual.GetType());
            _AuthenticationServiceMock.Verify(x => x.AuthenticateAsync("id", "key", "authurl", "user", "pwd", "auth"), Times.Exactly(1));
        }
    }
}
