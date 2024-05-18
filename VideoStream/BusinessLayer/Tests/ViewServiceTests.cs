using BusinessLayer.Logger;
using BusinessLayer.Services;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Moq;
using NUnit.Framework;
using System.Linq.Expressions;

namespace BusinessLayer.Tests
{
    [TestFixture]
    public class ViewServiceTests
    {
        [Test]
        public void CountMethod_ShouldCallRepository_Once()
        {
            // arrange
            var mockRepository = new Mock<IRepository<View>>();
            var mockLogger = new Mock<IAppLogger>();
            var testViewService = new ViewService(mockRepository.Object, mockLogger.Object);

            // act
            testViewService.GetAll();

            // assert
            mockRepository.Verify(method => method.GetAll(), Times.Once);
        }

        [Test]
        public void CountMethod_ShouldReturn_NumberOfViews()
        {
            int count = 10;

            // arrange
            var mockRepository = new Mock<IRepository<View>>();

            mockRepository.Setup(s => s.Count(It.IsAny<Expression<Func<View, bool>>>())).Returns(count);

            var mockLogger = new Mock<IAppLogger>();
            var testViewService = new ViewService(mockRepository.Object, mockLogger.Object);

            // act
            var viewCount = testViewService.Count(Guid.NewGuid());

            // assert
            Assert.That(count, Is.EqualTo(viewCount));
        }
    }
}
