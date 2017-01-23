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
    public class TokenControllerTest
    {
        private TokenController _Sut;
        private Mock<ITokenRepository> _TokenRepositoryMock;

        [TestInitialize]
        public void Init()
        {
            _TokenRepositoryMock = new Mock<ITokenRepository>();

            var token = new Token() { AuthToken = "ABC", UserName = "bcd", Roles = "ABC,ABC", CreatedOn = new DateTime(2016, 12, 1) };

            _TokenRepositoryMock.Setup(x => x.GetTokenAsync(It.IsAny<String>())).Returns(Task.FromResult(token));
            _TokenRepositoryMock.Setup(x => x.SaveTokenAsync(It.IsAny<User>())).Returns(Task.FromResult(0));
            _TokenRepositoryMock.Setup(x => x.DeleteTokenAsync(It.IsAny<String>())).Returns(Task.FromResult(0));

            DependencyService.DependencyContainer.RegisterInstance<ITokenRepository>(_TokenRepositoryMock.Object);
        }

        [TestMethod]
        public void TokenController_Get_HappyPath()
        {
            // Arrange
            _Sut = new TokenController();

            // Act
            var actual = _Sut.Get("123").Result;

            // Assert
            Assert.AreEqual("ABC", actual.Token);
            Assert.AreEqual("bcd", actual.UserName);
            Assert.AreEqual("ABC,ABC", actual.Roles);

            _TokenRepositoryMock.Verify(x => x.GetTokenAsync("123"), Times.Exactly(1));
            _TokenRepositoryMock.Verify(x => x.DeleteTokenAsync("123"), Times.Exactly(0));
        }

        [TestMethod]
        public void TokenController_Delete_HappyPath()
        {
            // Arrange
            _Sut = new TokenController();

            // Act
            Task.Run(() => { _Sut.Delete("123"); }).Wait();

            // Assert
            _TokenRepositoryMock.Verify(x => x.GetTokenAsync("123"), Times.Exactly(0));
            _TokenRepositoryMock.Verify(x => x.DeleteTokenAsync("123"), Times.Exactly(1));
        }
    }
}
