using Moq;
using MyBookshelf.Application.Commands.CreateUser;
using MyBookshelf.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyBookshelf.Tests.Application.Commands
{
    public class CreateUserCommandHandlerTests
    {
        [Fact]
        public async Task EmailAlreadyExists_Executed_ReturnInvalidCommand()
        {
            // Arrange
            var createUserCommand = new CreateUser() { Email = "francisco@email.com", Nome = "Francisco", Senha = "senhatesteunitario" };            
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(ur => ur.EmailExists(createUserCommand.Email)).Returns(true);
            var createUserCommandHandler = new CreateUserHandler(userRepository.Object);

            // Act

            // Assert
        }
    }
}
