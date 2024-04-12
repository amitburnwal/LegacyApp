using LegacyApp.Api.Controllers;
using LegacyApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LegacyApp.Api.Tests
{
    [TestClass]
    public class UsersControllerTests
    {
        [TestMethod]
        public async Task CreateUser_Returns_BadRequest_When_Model_Is_Invalid()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var clientRepositoryMock = new Mock<IClientRepository>();
            var creditServiceBLMock = new Mock<ICreditServiceBL>();
            var loggerMock = new Mock<ILogger<UsersController>>();

            var controller = new UsersController(creditServiceBLMock.Object, userServiceMock.Object, clientRepositoryMock.Object, loggerMock.Object);

            var invalidRequest = new UserDto(); // Invalid request without required fields

            // Act
            var result = await controller.CreateUser(invalidRequest, CancellationToken.None);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task CreateUser_Returns_OkResult_When_Request_Is_Valid()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.AddUser(It.IsAny<UserDto>())).ReturnsAsync(new User());

            var clientRepositoryMock = new Mock<IClientRepository>();
            clientRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Client());

            var creditServiceBLMock = new Mock<ICreditServiceBL>();
            creditServiceBLMock.Setup(x => x.GetCreditLimit(It.IsAny<int>(), It.IsAny<UserDto>())).ReturnsAsync(5000); // Assuming a credit limit of 5000
            creditServiceBLMock.Setup(x => x.CreditLimitNotSufficient(It.IsAny<UserDto>())).Returns(false); // Assuming credit limit is sufficient

            var loggerMock = new Mock<ILogger<UsersController>>();

            var controller = new UsersController(creditServiceBLMock.Object, userServiceMock.Object, clientRepositoryMock.Object, loggerMock.Object);

            var validRequest = new UserDto
            {
                Firstname = "John",
                Surname = "Doe",
                EmailAddress = "john@example.com",
                DateOfBirth = DateTime.Now.AddYears(-30),
                ClientId = 1
            };

            // Act
            var result = await controller.CreateUser(validRequest, CancellationToken.None);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}
