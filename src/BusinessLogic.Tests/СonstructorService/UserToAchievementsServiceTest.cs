using BusinessLogic.Services;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Wrapper;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Xunit.Sdk;

namespace BusinessLogic.Tests.СonstructorService
{
    public class UserToAchievementsServiceTest
    {
        private readonly UserToAchievementsService service;
        private readonly Mock<IUserToAchievementsRepository> UserToAchievementRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectUserToAchievement()
        {
            return new List<object[]>
            {
                new object[] { new UserToAchievement() { UserId = 0, AchievementsId = 0 } },
                new object[] { new UserToAchievement() { UserId = -1, AchievementsId = -1 } },
            };
        }

        public static IEnumerable<object[]> CreateCorrectUserToAchievement()
        {
            return new List<object[]>
            {
                new object[] { new UserToAchievement() { UserId = 1, AchievementsId = 1 } },
                new object[] { new UserToAchievement() { UserId = 2, AchievementsId = 2 } },
                new object[] { new UserToAchievement() { UserId = 3, AchievementsId = 3 } },
            };
        }
        public static IEnumerable<object[]> GetIncorrectUserToAchievement()
        {
            return new List<object[]>
            {
                new object[] { new UserToAchievement() {Id = 0, UserId = 0, AchievementsId = 0 } },
                new object[] { new UserToAchievement() {Id = -1, UserId = -1, AchievementsId = -1 } },
            };
        }

        public static IEnumerable<object[]> GetCorrectUserToAchievement()
        {
            return new List<object[]>
            {
                new object[] { new UserToAchievement() {Id = 1, UserId = 1, AchievementsId = 1 } },
                new object[] { new UserToAchievement() {Id = 2, UserId = 2, AchievementsId = 2 } },
                new object[] { new UserToAchievement() {Id = 3, UserId = 3, AchievementsId = 3 } },
            };
        }


        public UserToAchievementsServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            UserToAchievementRepositoryMoq = new Mock<IUserToAchievementsRepository>();

            repositoryWrapperMoq.Setup(x => x.UserToAchievements).Returns(UserToAchievementRepositoryMoq.Object);

            service = new UserToAchievementsService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            UserToAchievementRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectUserToAchievement))]
        public async Task GetById_correct(UserToAchievement UserToAchievement)
        {
            UserToAchievementRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserToAchievement, bool>>>())).ReturnsAsync(new List<UserToAchievement> { UserToAchievement });

            // Act
            var result = await service.GetById(UserToAchievement.Id);

            // Assert
            Assert.Equal(UserToAchievement.Id, result.Id);
            UserToAchievementRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<UserToAchievement, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectUserToAchievement))]
        public async Task GetByid_incorrect(UserToAchievement UserToAchievement)
        {
            UserToAchievementRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserToAchievement, bool>>>())).ReturnsAsync(new List<UserToAchievement> { UserToAchievement });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(UserToAchievement.Id));

            // Assert
            UserToAchievementRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<UserToAchievement, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(CreateCorrectUserToAchievement))]

        public async Task CreateAsyncNewUserToAchievementShouldNotCreateNewUserToAchievement_correct(UserToAchievement UserToAchievement)
        {
            var newUserToAchievement = UserToAchievement;

            await service.Create(newUserToAchievement);
            UserToAchievementRepositoryMoq.Verify(x => x.Create(It.IsAny<UserToAchievement>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectUserToAchievement))]

        public async Task CreateAsyncNewUserToAchievementShouldNotCreateNewUserToAchievement_incorrect(UserToAchievement UserToAchievement)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(UserToAchievement));
            UserToAchievementRepositoryMoq.Verify(x => x.Delete(It.IsAny<UserToAchievement>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectUserToAchievement))]

        public async Task UpdateAsyncOldUserToAchievement_correct(UserToAchievement UserToAchievement)
        {
            var newUserToAchievement = UserToAchievement;

            await service.Update(newUserToAchievement);
            UserToAchievementRepositoryMoq.Verify(x => x.Update(It.IsAny<UserToAchievement>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectUserToAchievement))]

        public async Task UpdateAsyncOldUserToAchievement_incorrect(UserToAchievement UserToAchievement)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(UserToAchievement));
            UserToAchievementRepositoryMoq.Verify(x => x.Update(It.IsAny<UserToAchievement>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectUserToAchievement))]

        public async Task DeleteAsyncOldUserToAchievement_correct(UserToAchievement UserToAchievement)
        {
            UserToAchievementRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserToAchievement, bool>>>())).ReturnsAsync(new List<UserToAchievement> { UserToAchievement });

            await service.Delete(UserToAchievement.Id);

            var result = await service.GetById(UserToAchievement.Id);
            UserToAchievementRepositoryMoq.Verify(x => x.Delete(It.IsAny<UserToAchievement>()), Times.Once);
            Assert.Equal(UserToAchievement.Id, result.Id);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectUserToAchievement))]

        public async Task DeleteAsyncOldUserToAchievement_incorrect(UserToAchievement UserToAchievement)
        {
            UserToAchievementRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserToAchievement, bool>>>())).ReturnsAsync(new List<UserToAchievement> { UserToAchievement });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(UserToAchievement.Id));
            UserToAchievementRepositoryMoq.Verify(x => x.Delete(It.IsAny<UserToAchievement>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }
    }
}