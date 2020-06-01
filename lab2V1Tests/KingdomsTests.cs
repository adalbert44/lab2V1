using lab2V1.Controllers;
using lab2V1.Models;
using Xunit;
using Moq;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lab2V1.Tests
{

    public class KingdomsControllerTests
    {
        [Fact]
        public void Post()
        {
            var mockSet = new Mock<DbSet<Kingdom>>();

            var mockContext = new Mock<Lab2LibraryContext>();
            mockContext.Setup(m => m.Kingdoms).Returns(mockSet.Object);
            var e = mockContext.Object;
            var service = new KingdomsController(mockContext.Object);

            var k = new Kingdom();
            var t = service.PostKingdom(k);
            t.Wait();
            mockSet.Verify(m => m.Add(It.IsAny<Kingdom>()), Times.Once());
        }

        [Fact]
        public void Exists()
        {
            var kk = new List<Kingdom>()
            {
                new Kingdom() { Id = 1 }
            };

            var queryable = kk.AsQueryable();
            var mock = new Mock<DbSet<Kingdom>>();
            mock.As<IQueryable<Kingdom>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mock.As<IQueryable<Kingdom>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mock.As<IQueryable<Kingdom>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mock.As<IQueryable<Kingdom>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            var mockContext = new Mock<Lab2LibraryContext>();
            mockContext.Setup(m => m.Kingdoms).Returns(mock.Object);
            var e = mockContext.Object;
            var service = new KingdomsController(mockContext.Object);

            Assert.True(service.KingdomExists(1));
            Assert.False(service.KingdomExists(2)); 
        }
    }
}