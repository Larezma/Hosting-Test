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
    public class TrainingServiceTest
    {
        private readonly TrainingService service;
        private readonly Mock<ITrainingRepository> trainingRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectTraining()
        {
            return new List<object[]>
            {
                new object[] { new Training() { DurationMinutes = "100m",CaloriesBurned=200,Notes="",TrainingType=""} },
                new object[] { new Training() { DurationMinutes = "",CaloriesBurned=200,Notes="",TrainingType="sddsdssd"} },
                new object[] { new Training() { DurationMinutes = "",CaloriesBurned=300,Notes="",TrainingType=""} },
            };
        }

        public static IEnumerable<object[]> CreateCorrectTraining()
        {
            return new List<object[]>
            {
                new object[] { new Training() { DurationMinutes = "100m",CaloriesBurned=200.0m,Notes="sddss",TrainingType="Лайтова"} },
                new object[] { new Training() { DurationMinutes = "200m",CaloriesBurned=300.22m,Notes="ss",TrainingType= "Ну вроде норм" } },
                new object[] { new Training() { DurationMinutes = "100m",CaloriesBurned=401.0m,Notes="s",TrainingType="скоро меня прорвет!"} },
            };
        }
        public static IEnumerable<object[]> GetIncorrectTraining()
        {
            return new List<object[]>
            {
                new object[] { new Training() {TrainingId=0,DurationMinutes = "100m",CaloriesBurned=200,Notes="",TrainingType=""} },
                new object[] { new Training() {TrainingId=-1, DurationMinutes = "",CaloriesBurned=200,Notes="",TrainingType=""} },
            };
        }

        public static IEnumerable<object[]> GetCorrectTraining()
        {
            return new List<object[]>
            {
                new object[] { new Training() {TrainingId=1,DurationMinutes = "100m",CaloriesBurned=200,Notes="",TrainingType="32113232"} },
                new object[] { new Training() {TrainingId=2, DurationMinutes = "23212331322",CaloriesBurned=200,Notes="",TrainingType="2313232132"} },
            };
        }

        public TrainingServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            trainingRepositoryMoq = new Mock<ITrainingRepository>();

            repositoryWrapperMoq.Setup(x => x.Training).Returns(trainingRepositoryMoq.Object);

            service = new TrainingService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            trainingRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectTraining))]
        public async Task GetById_correct(Training Training)
        {
            trainingRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Training, bool>>>())).ReturnsAsync(new List<Training> { Training });

            // Act
            var result = await service.GetById(Training.TrainingId);

            // Assert
            Assert.Equal(Training.TrainingId, result.TrainingId);
            trainingRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Training, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectTraining))]
        public async Task GetByid_incorrect(Training Training)
        {
            trainingRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Training, bool>>>())).ReturnsAsync(new List<Training> { Training });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(Training.TrainingId));

            // Assert
            trainingRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Training, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);

        }

        [Theory]
        [MemberData(nameof(CreateCorrectTraining))]

        public async Task CreateAsyncNewTrainingShouldNotCreateNewTraining_correct(Training Training)
        {
            var newTraining = Training;

            await service.Create(newTraining);
            trainingRepositoryMoq.Verify(x => x.Create(It.IsAny<Training>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectTraining))]

        public async Task CreateAsyncNewTrainingShouldNotCreateNewTraining_incorrect(Training Training)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(Training));
            trainingRepositoryMoq.Verify(x => x.Delete(It.IsAny<Training>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectTraining))]

        public async Task UpdateAsyncOldTraining_correct(Training Training)
        {
            var newTraining = Training;

            await service.Update(newTraining);
            trainingRepositoryMoq.Verify(x => x.Update(It.IsAny<Training>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectTraining))]

        public async Task UpdateAsyncOldTraining_incorrect(Training Training)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(Training));
            trainingRepositoryMoq.Verify(x => x.Update(It.IsAny<Training>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectTraining))]

        public async Task DeleteAsyncOldTraining_correct(Training Training)
        {
            trainingRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Training, bool>>>())).ReturnsAsync(new List<Training> { Training });

            await service.Delete(Training.TrainingId);

            var result = await service.GetById(Training.TrainingId);
            trainingRepositoryMoq.Verify(x => x.Delete(It.IsAny<Training>()), Times.Once);
            Assert.Equal(Training.TrainingId, result.TrainingId);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectTraining))]

        public async Task DeleteAsyncOldTraining_incorrect(Training Training)
        {
            trainingRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Training, bool>>>())).ReturnsAsync(new List<Training> { Training });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(Training.TrainingId));
            trainingRepositoryMoq.Verify(x => x.Delete(It.IsAny<Training>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

    }
}