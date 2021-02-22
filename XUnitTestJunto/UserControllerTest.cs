namespace XUnitTestJunto
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Junto.Controllers;
    using Junto.Core.Service;
    using Junto.Domain.Model;
    using Junto.Domain.Service;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    public class UserControllerTest
    {
        private UserController _controller;
        private IUserService _service;

        public UserControllerTest()
        {
            _controller = new UserController(new UserService(new UserRepositoryFake()));
        }

        [Fact]
        public void Get()
        {
            // Act
            var okResult = _controller.Get().Result as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<User>>(okResult.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public void GetById()
        {
            // Act
            var okResult = _controller.GetById(1).Result as OkObjectResult;
            // Assert
            var item = Assert.IsType<User>(okResult.Value);
        }

        [Fact]
        public void Post()
        {
            var idUser = 88;
            // Act
            var okResult = _controller.Post(new User { Id = idUser }).Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult);

            var user = okResult.Value as User;

            Assert.NotEqual(idUser, user.Id);
        }

        [Fact]
        public void Put()
        {
            var idUser = 1;
            // Act
            var okResult = _controller.Put(new User { Id = idUser, Login = "PUT", Password = "PUT" }).Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult);

            var user = okResult.Value as User;

            Assert.Equal("PUT", user.Login);
            Assert.Equal("PUT", user.Password);
        }

        [Fact]
        public void Delete()
        {
            var okResult = _controller.Delete(1).Result as OkResult;
            Assert.IsType<OkResult>(okResult);

            // Act
            var okResult2 = _controller.GetById(1).Result as OkObjectResult;
            // Assert
            Assert.Null(okResult2.Value);
        }

        [Fact]
        public void PostToken()
        {
            var okResult = _controller.PostToken(new User { Id = 1, Login = "Junto", Password = "JuntoPassword" }).Result as OkObjectResult;
            Assert.IsType<OkObjectResult>(okResult);
            Assert.IsType<string>(okResult.Value);
        }
    }
}
