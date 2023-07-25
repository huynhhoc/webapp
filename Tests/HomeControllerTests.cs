
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using webapp.Controllers;
using webapp.Models;

namespace webapp.Tests
{
    [TestFixture]
    public class HomeControllerTests
    {
        private Mock<ILogger<HomeController>> _loggerMock;
        private Mock<AppDbContext> _contextMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<HomeController>>();
            _contextMock = new Mock<AppDbContext>();
        }

        [Test]
        public void Index_ReturnsViewWithListOfProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10 },
                new Product { Id = 2, Name = "Product 2", Price = 15 }
            };

            _contextMock.Setup(c => c.Products.ToList()).Returns(products);

            var controller = new HomeController(_loggerMock.Object, _contextMock.Object);

            // Act
            var result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(products);
            Assert.AreEqual(products, result.Model);
        }
    }
}

