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
    public class AchievementServiceTest
    {
        private readonly AchievementsService service;
        private readonly Mock<IAchievementsRepository> AchievementRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectAchievement()
        {
            return new List<object[]>
            {
                new object[] { new Achievement() {AchievementsText = "", AchievementsType= 0 } },
                new object[] { new Achievement() {AchievementsText = "", AchievementsType= -1 } },
            };
        }

        public static IEnumerable<object[]> CreateCorrectAchievement()
        {
            return new List<object[]>
            {
                new object[] { new Achievement() {AchievementsText = "sd", AchievementsType= 1 } },
                new object[] { new Achievement() {AchievementsText = "sd", AchievementsType= 2 } },
                new object[] { new Achievement() {AchievementsText = "ds", AchievementsType= 3 } },
            };
        }
        public static IEnumerable<object[]> GetIncorrectAchievement()
        {
            return new List<object[]>
            {
                new object[] { new Achievement() {AchievementsId=0, AchievementsText = "", AchievementsType= 0 } },
                new object[] { new Achievement() {AchievementsId=-1, AchievementsText = "", AchievementsType= -1 } },
            };
        }

        public static IEnumerable<object[]> GetCorrectAchievement()
        {
            return new List<object[]>
            {
                new object[] { new Achievement() {AchievementsId=1, AchievementsText = "sds", AchievementsType= 1 } },
                new object[] { new Achievement() {AchievementsId=2, AchievementsText = "sd", AchievementsType= 2 } },
                new object[] { new Achievement() {AchievementsId = 3, AchievementsText = "sdss", AchievementsType= 3 } },
            };
        }

        public AchievementServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            AchievementRepositoryMoq = new Mock<IAchievementsRepository>();

            repositoryWrapperMoq.Setup(x => x.Achievements).Returns(AchievementRepositoryMoq.Object);

            service = new AchievementsService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            AchievementRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectAchievement))]
        public async Task GetById_correct(Achievement Achievement)
        {
            AchievementRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Achievement, bool>>>())).ReturnsAsync(new List<Achievement> { Achievement });

            // Act
            var result = await service.GetById(Achievement.AchievementsId);

            // Assert
            Assert.Equal(Achievement.AchievementsId, result.AchievementsId);
            AchievementRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Achievement, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectAchievement))]
        public async Task GetByid_incorrect(Achievement Achievement)
        {
            AchievementRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Achievement, bool>>>())).ReturnsAsync(new List<Achievement> { Achievement });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(Achievement.AchievementsId));

            // Assert
            AchievementRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Achievement, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(CreateCorrectAchievement))]

        public async Task CreateAsyncNewAchievementShouldNotCreateNewAchievement_correct(Achievement Achievement)
        {
            var newAchievement = Achievement;

            await service.Create(newAchievement);
            AchievementRepositoryMoq.Verify(x => x.Create(It.IsAny<Achievement>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectAchievement))]

        public async Task CreateAsyncNewAchievementShouldNotCreateNewAchievement_incorrect(Achievement Achievement)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(Achievement));
            AchievementRepositoryMoq.Verify(x => x.Delete(It.IsAny<Achievement>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectAchievement))]

        public async Task UpdateAsyncOldAchievement_correct(Achievement Achievement)
        {
            var newAchievement = Achievement;

            await service.Update(newAchievement);
            AchievementRepositoryMoq.Verify(x => x.Update(It.IsAny<Achievement>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectAchievement))]

        public async Task UpdateAsyncOldAchievement_incorrect(Achievement Achievement)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(Achievement));
            AchievementRepositoryMoq.Verify(x => x.Update(It.IsAny<Achievement>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectAchievement))]

        public async Task DeleteAsyncOldAchievement_correct(Achievement Achievement)
        {
            AchievementRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Achievement, bool>>>())).ReturnsAsync(new List<Achievement> { Achievement });

            await service.Delete(Achievement.AchievementsId);

            var result = await service.GetById(Achievement.AchievementsId);
            AchievementRepositoryMoq.Verify(x => x.Delete(It.IsAny<Achievement>()), Times.Once);
            Assert.Equal(Achievement.AchievementsId, result.AchievementsId);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectAchievement))]

        public async Task DeleteAsyncOldAchievement_incorrect(Achievement Achievement)
        {
            AchievementRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Achievement, bool>>>())).ReturnsAsync(new List<Achievement> { Achievement });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(Achievement.AchievementsId));
            AchievementRepositoryMoq.Verify(x => x.Delete(It.IsAny<Achievement>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

    }
}
