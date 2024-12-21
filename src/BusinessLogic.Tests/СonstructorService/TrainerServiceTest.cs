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
    public class TrainerServiceTest
    {
        private readonly TrainerService service;
        private readonly Mock<ITrainerRepository> trainerRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectTrainer()
        {
            return new List<object[]>
            {
                new object[] { new Trainer() { TrainerId = 1, FirstName = "sdsdsd", MiddleName="sdsds",LastName="dsdssd", Email = "", Password = "", PhoneNumber = ""} },
                new object[] { new Trainer() { TrainerId = 2, FirstName = "sdssdds", MiddleName = "", LastName = "dsdssd", Email = "sdsdsd", Password = "sdsds",PhoneNumber = "" } },
                new object[] { new Trainer() { TrainerId = 3, FirstName = "", MiddleName = "", LastName = "", Email = "", Password = "",  PhoneNumber = "" } },
            };
        }

        public static IEnumerable<object[]> CreateCorrectTrainer()
        {
            return new List<object[]>
            {
                new object[] { new Trainer() { TrainerId = 1, FirstName = "xddssd", MiddleName="sdsdd",LastName="sdsdsd", Email = "dsdssd", Password = "sdsdds", PhoneNumber = "sddssd"} },
                new object[] { new Trainer() { TrainerId = 2, FirstName = "xddssd", MiddleName="sdsdd",LastName="sdsdsd", Email = "dsdssd", Password = "sdsdds", PhoneNumber = "sddssd"} },
                new object[] { new Trainer() { TrainerId = 3, FirstName = "xddssd", MiddleName="sdsdd",LastName="sdsdsd", Email = "dsdssd", Password = "sdsdds", PhoneNumber = "sddssd"} },
            };
        }
        public static IEnumerable<object[]> GetIncorrectTrainer()
        {
            return new List<object[]>
            {
                new object[] { new Trainer() {TrainerId = 0, FirstName = "", MiddleName="sdsdd",LastName="sdsdsd", Email = "dsdssd", Password = "sdsdds", PhoneNumber = "sddssd"} },
                new object[] { new Trainer() {TrainerId = -1, FirstName = "xddssd", MiddleName="sdsdd",LastName="", Email = "dsdssd", Password = "sdsdds", PhoneNumber = ""} },
            };
        }

        public static IEnumerable<object[]> GetCorrectTrainer()
        {
            return new List<object[]>
            {
                new object[] { new Trainer() { TrainerId = 1, FirstName = "xddssd", MiddleName="sdsdd",LastName="sdsdsd", Email = "dsdssd", Password = "sdsdds", PhoneNumber = "sddssd"} },
                new object[] { new Trainer() { TrainerId = 2, FirstName = "ыввы", MiddleName="вывы",LastName="вы", Email = "ыв", Password = "sdsdds", PhoneNumber = "sddssd"} },
            };
        }

        public TrainerServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            trainerRepositoryMoq = new Mock<ITrainerRepository>();

            repositoryWrapperMoq.Setup(x => x.Trainer).Returns(trainerRepositoryMoq.Object);

            service = new TrainerService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            trainerRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectTrainer))]
        public async Task GetById_correct(Trainer Trainer)
        {
            trainerRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Trainer, bool>>>())).ReturnsAsync(new List<Trainer> { Trainer });

            // Act
            var result = await service.GetById(Trainer.TrainerId);

            // Assert
            Assert.Equal(Trainer.TrainerId, result.TrainerId);
            trainerRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Trainer, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectTrainer))]
        public async Task GetByid_incorrect(Trainer Trainer)
        {
            trainerRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Trainer, bool>>>())).ReturnsAsync(new List<Trainer> { Trainer });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(Trainer.TrainerId));

            // Assert
            trainerRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Trainer, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);

        }

        [Theory]
        [MemberData(nameof(CreateCorrectTrainer))]

        public async Task CreateAsyncNewTrainerShouldNotCreateNewTrainer_correct(Trainer Trainer)
        {
            var newTrainer = Trainer;

            await service.Create(newTrainer);
            trainerRepositoryMoq.Verify(x => x.Create(It.IsAny<Trainer>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectTrainer))]

        public async Task CreateAsyncNewTrainerShouldNotCreateNewTrainer_incorrect(Trainer Trainer)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(Trainer));
            trainerRepositoryMoq.Verify(x => x.Delete(It.IsAny<Trainer>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectTrainer))]

        public async Task UpdateAsyncOldTrainer_correct(Trainer Trainer)
        {
            var newTrainer = Trainer;

            await service.Update(newTrainer);
            trainerRepositoryMoq.Verify(x => x.Update(It.IsAny<Trainer>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectTrainer))]

        public async Task UpdateAsyncOldTrainer_incorrect(Trainer Trainer)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(Trainer));
            trainerRepositoryMoq.Verify(x => x.Update(It.IsAny<Trainer>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectTrainer))]

        public async Task DeleteAsyncOldTrainer_correct(Trainer Trainer)
        {
            trainerRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Trainer, bool>>>())).ReturnsAsync(new List<Trainer> { Trainer });

            await service.Delete(Trainer.TrainerId);

            var result = await service.GetById(Trainer.TrainerId);
            trainerRepositoryMoq.Verify(x => x.Delete(It.IsAny<Trainer>()), Times.Once);
            Assert.Equal(Trainer.TrainerId, result.TrainerId);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectTrainer))]

        public async Task DeleteAsyncOldTrainer_incorrect(Trainer Trainer)
        {
            trainerRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Trainer, bool>>>())).ReturnsAsync(new List<Trainer> { Trainer });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(Trainer.TrainerId));
            trainerRepositoryMoq.Verify(x => x.Delete(It.IsAny<Trainer>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

    }
}