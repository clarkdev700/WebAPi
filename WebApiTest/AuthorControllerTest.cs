using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using WebAPi.Controllers;
using WebAPi.Dtos;
using WebAPi.Entities;
using WebAPi.Services;

namespace WebApiTest
{
    [TestClass]
    public class AuthorControllerTest
    {
        [TestMethod]
        public void GetAuthor_ReturnNotFound_GivenMissingID()
        {
            var authorId = 1;
            var includeBook = false;
            var mockRepo = new Mock<IRepository<Author>>();
            mockRepo.Setup(repo => repo.GetEntity(authorId, includeBook)).Returns((Author)null);

            var mockMapper = new Mock<IMapper>();

            // Act
            var controller = new AuthorController(mockRepo.Object, mockMapper.Object);
            var result = (NotFoundResult) controller.Get(authorId, includeBook);
            // Assert
           
            Assert.AreEqual(404, result.StatusCode);
        }
        
        [TestMethod]
        public void GetAuthor_ReturnAuthor_GivenCorrectId()
        {
            var authorDto = new AuthorDto()
            {
                Id = 1,
                Name = "James",
                LastName = "Allen",
                Books = new List<BookDto>()
            };

            var author = new Author()
            {
                Id = 1,
                Name = "James",
                LastName = "Allen",
                Books = new List<Book>()
            };

            var authorId = 1;
            var includeBook = false;
            var mockRepo = new Mock<IRepository<Author>>();
            mockRepo.Setup(repo => repo.GetEntity(authorId, includeBook)).Returns(author);

            var mockMapper = new Mock<IMapper>();

            // Act
            var controller = new AuthorController(mockRepo.Object, mockMapper.Object);
            var result = (OkObjectResult) controller.Get(authorId, includeBook);
            var resultContent = result.Value as AuthorDto;
            // Assert
            //Assert.IsInstanceOfType(result.Value, typeof(AuthorDto));
            //Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(authorDto.LastName, resultContent.LastName);
            Assert.AreEqual(authorDto.Name, resultContent.LastName);
            Assert.AreEqual(authorDto.Books.Count, resultContent.Books.Count);
        }
        
        [TestMethod]
        public void CreateAuthor_ReturnBadRequest_ForInvalidAuthorDto()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void CreateAuthor_Return201_ForvalidAuthorDto()
        {
            Assert.Inconclusive();
        }
    }
}
