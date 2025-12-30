using Application.Services;
using Application.Users.DTOs;
using Domain.Contracts;
using Domain.Entities;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Application
{
    public class UserServiceTests
    {
        [Fact]
        public async Task RegisterAsync_CreatesUser_WhenEmailNotExists()
        {
            // Arrange
            var repoMock = new Mock<IUserRepository>();
            repoMock.Setup(r => r.GetByEmailAsync("test@email.com")).ReturnsAsync((User)null);
            repoMock.Setup(r => r.CreateAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            var service = new UserService(repoMock.Object);
            var dto = new RegisterUserDto("test@email.com", "password");

            // Act
            var result = await service.RegisterAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Email, result.Email);
            Assert.Equal(dto.Email, result.Username);
            Assert.False(string.IsNullOrEmpty(result.PasswordHash));
        }

        [Fact]
        public async Task RegisterAsync_ThrowsException_WhenUserAlreadyExists()
        {
            // Arrange
            var existingUser = new User { Email = "test@email.com" };
            var repoMock = new Mock<IUserRepository>();
            repoMock.Setup(r => r.GetByEmailAsync("test@email.com")).ReturnsAsync(existingUser);
            var service = new UserService(repoMock.Object);
            var dto = new RegisterUserDto("test@email.com", "password");

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.RegisterAsync(dto));
        }

        [Fact]
        public async Task LoginAsync_ReturnsUser_WhenCredentialsAreValid()
        {
            // Arrange
            var password = "password";
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User { Email = "test@email.com", PasswordHash = hash };
            var repoMock = new Mock<IUserRepository>();
            repoMock.Setup(r => r.GetByEmailAsync("test@email.com")).ReturnsAsync(user);
            var service = new UserService(repoMock.Object);
            var dto = new LoginUserDto("test@email.com", password);

            // Act
            var result = await service.LoginAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task LoginAsync_ThrowsException_WhenUserNotFound()
        {
            // Arrange
            var repoMock = new Mock<IUserRepository>();
            repoMock.Setup(r => r.GetByEmailAsync("notfound@email.com")).ReturnsAsync((User)null);
            var service = new UserService(repoMock.Object);
            var dto = new LoginUserDto("notfound@email.com", "password");

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.LoginAsync(dto));
        }

        [Fact]
        public async Task LoginAsync_ThrowsException_WhenPasswordIsInvalid()
        {
            // Arrange
            var hash = BCrypt.Net.BCrypt.HashPassword("correctpassword");
            var user = new User { Email = "test@email.com", PasswordHash = hash };
            var repoMock = new Mock<IUserRepository>();
            repoMock.Setup(r => r.GetByEmailAsync("test@email.com")).ReturnsAsync(user);
            var service = new UserService(repoMock.Object);
            var dto = new LoginUserDto("test@email.com", "wrongpassword");

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.LoginAsync(dto));
        }
    }
}
