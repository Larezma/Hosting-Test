using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using BusinessLogic.Services;
using Domain.Models;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Wrapper;
using Moq;
using Xunit.Sdk;
using System.Numerics;

namespace BusinessLogic.Tests.СonstructorService
{
    public class PublicationServiceTest
    {
        private readonly PublicationService service;
        private readonly Mock<IPublicationRepository> PublicationRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectPublication()
        {
            return new List<object[]>
            {
                new object[] { new Publication() { PublicationsId=0,PublicationText="",PublicationsImage="" } },
                new object[] { new Publication() { PublicationsId=-1,PublicationText="",PublicationsImage="sds" } },
                new object[] { new Publication() { PublicationsId=int.MaxValue,PublicationText="",PublicationsImage="" } },
            };
        }

        public static IEnumerable<object[]> CreateCorrectPublication()
        {
            return new List<object[]>
            {
                new object[] { new Publication() { PublicationsId=1,PublicationText="sdds",PublicationsImage= "sdds" } },
                new object[] { new Publication() { PublicationsId=2,PublicationText="1233",PublicationsImage= "1233" } },
                new object[] { new Publication() { PublicationsId=3,PublicationText="!!!!",PublicationsImage="" } },
            };
        }
        public static IEnumerable<object[]> GetIncorrectPublication()
        {
            return new List<object[]>
            {
                new object[] { new Publication() { PublicationsId=0, UsersId=0,PublicationText="",PublicationsImage="" } },
                new object[] { new Publication() { PublicationsId=-1, UsersId=1,PublicationText="",PublicationsImage="" } },
            };
        }

        public static IEnumerable<object[]> GetCorrectPublication()
        {
            return new List<object[]>
            {
                new object[] { new Publication() { PublicationsId=1, UsersId =1,PublicationText="ssdsd",PublicationsImage="" } },
                new object[] { new Publication() { PublicationsId=2, UsersId =2,PublicationText="dssd",PublicationsImage="sddssd" } },
            };
        }


        public PublicationServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            PublicationRepositoryMoq = new Mock<IPublicationRepository>();

            repositoryWrapperMoq.Setup(x => x.Publication).Returns(PublicationRepositoryMoq.Object);

            service = new PublicationService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            PublicationRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectPublication))]
        public async Task GetById_correct(Publication Publication)
        {
            PublicationRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Publication, bool>>>())).ReturnsAsync(new List<Publication> { Publication });

            // Act
            var result = await service.GetById(Publication.PublicationsId);

            // Assert
            Assert.Equal(Publication.PublicationsId, result.PublicationsId);
            PublicationRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Publication, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectPublication))]
        public async Task GetByid_incorrect(Publication Publication)
        {
            PublicationRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Publication, bool>>>())).ReturnsAsync(new List<Publication> { Publication });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(Publication.PublicationsId));

            // Assert
            PublicationRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Publication, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);

        }

        [Theory]
        [MemberData(nameof(CreateCorrectPublication))]

        public async Task CreateAsyncNewPublicationShouldNotCreateNewPublication_correct(Publication Publication)
        {
            var newPublication = Publication;

            await service.Create(newPublication);
            PublicationRepositoryMoq.Verify(x => x.Create(It.IsAny<Publication>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectPublication))]

        public async Task CreateAsyncNewPublicationShouldNotCreateNewPublication_incorrect(Publication Publication)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(Publication));
            PublicationRepositoryMoq.Verify(x => x.Delete(It.IsAny<Publication>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectPublication))]

        public async Task UpdateAsyncOldPublication_correct(Publication Publication)
        {
            var newPublication = Publication;

            await service.Update(newPublication);
            PublicationRepositoryMoq.Verify(x => x.Update(It.IsAny<Publication>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectPublication))]

        public async Task UpdateAsyncOldPublication_incorrect(Publication Publication)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(Publication));
            PublicationRepositoryMoq.Verify(x => x.Update(It.IsAny<Publication>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectPublication))]

        public async Task DeleteAsyncOldPublication_correct(Publication Publication)
        {
            PublicationRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Publication, bool>>>())).ReturnsAsync(new List<Publication> { Publication });

            await service.Delete(Publication.PublicationsId);

            var result = await service.GetById(Publication.PublicationsId);
            PublicationRepositoryMoq.Verify(x => x.Delete(It.IsAny<Publication>()), Times.Once);
            Assert.Equal(Publication.PublicationsId, result.PublicationsId);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectPublication))]

        public async Task DeleteAsyncOldPublication_incorrect(Publication Publication)
        {
            PublicationRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Publication, bool>>>())).ReturnsAsync(new List<Publication> { Publication });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(Publication.PublicationsId));
            PublicationRepositoryMoq.Verify(x => x.Delete(It.IsAny<Publication>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

    }
}
