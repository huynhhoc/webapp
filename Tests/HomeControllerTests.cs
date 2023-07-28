using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using webapp.Controllers;
using webapp.Models;
using webapp.Utils;

namespace webapp.Tests
{
    [TestFixture]
    public class HomeControllerTests
    {
        private Mock<ILogger<HomeController>> _loggerMock;
        private Mock<IAppDbContext> _contextMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<HomeController>>();
            _contextMock = new Mock<IAppDbContext>();
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

            // Convert the products list to an IQueryable for the DbSet mock setup
            IQueryable<Product> queryableProducts = products.AsQueryable();

            // Setup the DbSet mock to return the queryable products
            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(queryableProducts.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(queryableProducts.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(queryableProducts.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => queryableProducts.GetEnumerator());

            _contextMock.Setup(c => c.Products).Returns(mockSet.Object);

            var controller = new HomeController(_loggerMock.Object, _contextMock.Object);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            var model = result.Model as List<Product>;
            Assert.AreEqual(products.Count, model.Count);
        }
    }
}
