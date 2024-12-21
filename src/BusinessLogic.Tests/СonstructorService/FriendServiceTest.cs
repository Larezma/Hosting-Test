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

namespace BusinessLogic.Tests.СonstructorService
{
    public class FriendServiceTest
    {
        private readonly FriendService service;
        private readonly Mock<IFriendRepository> FriendRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectFriend()
        {
            return new List<object[]>
            {
                new object[] { new Friend() { UserId1 = 0,UserId2 = 0} },
                new object[] { new Friend() { UserId1 = -1,UserId2 = -1} },
            };
        }

        public static IEnumerable<object[]> CreateCorrectFriend()
        {
            return new List<object[]>
            {
                new object[] { new Friend() { UserId1 = 1,UserId2 = 1} },
                new object[] { new Friend() { UserId1 = 2,UserId2 = 2} },
                new object[] { new Friend() { UserId1 = 3,UserId2 = 3} },
            };
        }
        public static IEnumerable<object[]> GetIncorrectFriend()
        {
            return new List<object[]>
            {
                new object[] { new Friend() {FriendId = 0, UserId1 = 0,UserId2 = 0} },
                new object[] { new Friend() {FriendId = -1, UserId1 = -1,UserId2 = -1} },
            };
        }

        public static IEnumerable<object[]> GetCorrectFriend()
        {
            return new List<object[]>
            {
                new object[] { new Friend() {FriendId = 1, UserId1 = 1,UserId2 = 2} },
                new object[] { new Friend() {FriendId = 2, UserId1 = 2,UserId2 = 3} },
                new object[] { new Friend() {FriendId = 3, UserId1 = 3,UserId2 = 1} },
            };
        }


        public FriendServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            FriendRepositoryMoq = new Mock<IFriendRepository>();

            repositoryWrapperMoq.Setup(x => x.Friend).Returns(FriendRepositoryMoq.Object);

            service = new FriendService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            FriendRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectFriend))]
        public async Task GetById_correct(Friend Friend)
        {
            FriendRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Friend, bool>>>())).ReturnsAsync(new List<Friend> { Friend });

            // Act
            var result = await service.GetById(Friend.FriendId);

            // Assert
            Assert.Equal(Friend.FriendId, result.FriendId);
            FriendRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Friend, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectFriend))]
        public async Task GetByid_incorrect(Friend Friend)
        {
            FriendRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Friend, bool>>>())).ReturnsAsync(new List<Friend> { Friend });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(Friend.FriendId));

            // Assert
            FriendRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Friend, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(CreateCorrectFriend))]

        public async Task CreateAsyncNewFriendShouldNotCreateNewFriend_correct(Friend Friend)
        {
            var newFriend = Friend;

            await service.Create(newFriend);
            FriendRepositoryMoq.Verify(x => x.Create(It.IsAny<Friend>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectFriend))]

        public async Task CreateAsyncNewFriendShouldNotCreateNewFriend_incorrect(Friend Friend)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(Friend));
            FriendRepositoryMoq.Verify(x => x.Delete(It.IsAny<Friend>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectFriend))]

        public async Task UpdateAsyncOldFriend_correct(Friend Friend)
        {
            var newFriend = Friend;

            await service.Update(newFriend);
            FriendRepositoryMoq.Verify(x => x.Update(It.IsAny<Friend>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectFriend))]

        public async Task UpdateAsyncOldFriend_incorrect(Friend Friend)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(Friend));
            FriendRepositoryMoq.Verify(x => x.Update(It.IsAny<Friend>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectFriend))]

        public async Task DeleteAsyncOldFriend_correct(Friend Friend)
        {
            FriendRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Friend, bool>>>())).ReturnsAsync(new List<Friend> { Friend });

            await service.Delete(Friend.FriendId);

            var result = await service.GetById(Friend.FriendId);
            FriendRepositoryMoq.Verify(x => x.Delete(It.IsAny<Friend>()), Times.Once);
            Assert.Equal(Friend.FriendId, result.FriendId);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectFriend))]

        public async Task DeleteAsyncOldFriend_incorrect(Friend Friend)
        {
            FriendRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Friend, bool>>>())).ReturnsAsync(new List<Friend> { Friend });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(Friend.FriendId));
            FriendRepositoryMoq.Verify(x => x.Delete(It.IsAny<Friend>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

    }
}
