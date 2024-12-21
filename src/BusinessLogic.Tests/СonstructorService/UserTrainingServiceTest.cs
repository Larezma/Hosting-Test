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
    public class UserTrainingServiceTest
    {
        private readonly UserTrainingService service;
        private readonly Mock<IUserTrainingRepository> UserTrainingRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectUserTraining()
        {
            return new List<object[]>
            {
                new object[] { new UserTraining() { TrainingId = 0,UserId = 0,TrainerId = 0, DayOfWeek = "", Duration = "" } },
                new object[] { new UserTraining() { TrainingId = -1, UserId = -1,TrainerId = -1, DayOfWeek = "", Duration = "" } },
            };
        }

        public static IEnumerable<object[]> CreateCorrectUserTraining()
        {
            return new List<object[]>
            {
                new object[] { new UserTraining() { TrainingId = 1, UserId = 1,TrainerId = 1, DayOfWeek = "sd", Duration = "ddffd" } },
                new object[] { new UserTraining() { TrainingId = 2, UserId = 2,TrainerId = 2, DayOfWeek = "ds", Duration = "fdfd" } },
                new object[] { new UserTraining() { TrainingId = 3, UserId = 3,TrainerId = 3, DayOfWeek = "ffd", Duration = "asas" } },
            };
        }
        public static IEnumerable<object[]> GetIncorrectUserTraining()
        {
            return new List<object[]>
            {
                new object[] { new UserTraining() {Id = 0, TrainingId = 0, UserId = 0,TrainerId = 0, DayOfWeek = "", Duration = "" } },
                new object[] { new UserTraining() {Id = -1, TrainingId = -1, UserId = -1,TrainerId = -1, DayOfWeek = "", Duration = "" } },
            };
        }

        public static IEnumerable<object[]> GetCorrectUserTraining()
        {
            return new List<object[]>
            {
                new object[] { new UserTraining() {Id = 1, TrainingId = 1, UserId = 1,TrainerId = 1, DayOfWeek = "sd", Duration = "ddffd" } },
                new object[] { new UserTraining() {Id = 2, TrainingId = 2, UserId = 2,TrainerId = 2, DayOfWeek = "ds", Duration = "fdfd" } },
                new object[] { new UserTraining() {Id = 3, TrainingId = 3, UserId = 3,TrainerId = 3, DayOfWeek = "ffd", Duration = "asas" } },
            };
        }


        public UserTrainingServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            UserTrainingRepositoryMoq = new Mock<IUserTrainingRepository>();

            repositoryWrapperMoq.Setup(x => x.UserTraining).Returns(UserTrainingRepositoryMoq.Object);

            service = new UserTrainingService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            UserTrainingRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectUserTraining))]
        public async Task GetById_correct(UserTraining UserTraining)
        {
            UserTrainingRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserTraining, bool>>>())).ReturnsAsync(new List<UserTraining> { UserTraining });

            // Act
            var result = await service.GetById(UserTraining.Id);

            // Assert
            Assert.Equal(UserTraining.Id, result.Id);
            UserTrainingRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<UserTraining, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectUserTraining))]
        public async Task GetByid_incorrect(UserTraining UserTraining)
        {
            UserTrainingRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserTraining, bool>>>())).ReturnsAsync(new List<UserTraining> { UserTraining });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(UserTraining.Id));

            // Assert
            UserTrainingRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<UserTraining, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(CreateCorrectUserTraining))]

        public async Task CreateAsyncNewUserTrainingShouldNotCreateNewUserTraining_correct(UserTraining UserTraining)
        {
            var newUserTraining = UserTraining;

            await service.Create(newUserTraining);
            UserTrainingRepositoryMoq.Verify(x => x.Create(It.IsAny<UserTraining>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectUserTraining))]

        public async Task CreateAsyncNewUserTrainingShouldNotCreateNewUserTraining_incorrect(UserTraining UserTraining)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(UserTraining));
            UserTrainingRepositoryMoq.Verify(x => x.Delete(It.IsAny<UserTraining>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectUserTraining))]

        public async Task UpdateAsyncOldUserTraining_correct(UserTraining UserTraining)
        {
            var newUserTraining = UserTraining;

            await service.Update(newUserTraining);
            UserTrainingRepositoryMoq.Verify(x => x.Update(It.IsAny<UserTraining>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectUserTraining))]

        public async Task UpdateAsyncOldUserTraining_incorrect(UserTraining UserTraining)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(UserTraining));
            UserTrainingRepositoryMoq.Verify(x => x.Update(It.IsAny<UserTraining>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectUserTraining))]

        public async Task DeleteAsyncOldUserTraining_correct(UserTraining UserTraining)
        {
            UserTrainingRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserTraining, bool>>>())).ReturnsAsync(new List<UserTraining> { UserTraining });

            await service.Delete(UserTraining.Id);

            var result = await service.GetById(UserTraining.Id);
            UserTrainingRepositoryMoq.Verify(x => x.Delete(It.IsAny<UserTraining>()), Times.Once);
            Assert.Equal(UserTraining.Id, result.Id);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectUserTraining))]

        public async Task DeleteAsyncOldUserTraining_incorrect(UserTraining UserTraining)
        {
            UserTrainingRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserTraining, bool>>>())).ReturnsAsync(new List<UserTraining> { UserTraining });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(UserTraining.Id));
            UserTrainingRepositoryMoq.Verify(x => x.Delete(It.IsAny<UserTraining>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

    }
}