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
    public class TrainersSchedulesScheduleServiceTest
    {
        private readonly TrainersScheduleService service;
        private readonly Mock<ITrainersScheduleRepository> TrainersScheduleRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectTrainersSchedule()
        {
            return new List<object[]>
            {
                new object[] { new TrainersSchedule() { ScheduleId = 0, TrainerId = 0, TypeOfTraining = "", Date = new DateTime(2015, 7, 20, 18, 30, 25), Time = new DateTime(2015, 7, 20, 18, 30, 25) } },
                new object[] { new TrainersSchedule() { ScheduleId = -1, TrainerId = -1, TypeOfTraining = "", Date = new DateTime(2015, 7, 20, 18, 30, 25), Time = new DateTime(2015, 7, 20, 18, 30, 25) } },
            };
        }

        public static IEnumerable<object[]> CreateCorrectTrainersSchedule()
        {
            return new List<object[]>
            {
                new object[] { new TrainersSchedule() { ScheduleId = 1, TrainerId = 1, TypeOfTraining = "ds", Date = new DateTime(2015, 7, 20, 18, 30, 25), Time = new DateTime(2015, 7, 20, 18, 30, 25) } },
                new object[] { new TrainersSchedule() { ScheduleId = 2, TrainerId = 2, TypeOfTraining = "sdds", Date = new DateTime(2015, 7, 20, 18, 30, 25), Time = new DateTime(2015, 7, 20, 18, 30, 25) } },
                new object[] { new TrainersSchedule() { ScheduleId = 3, TrainerId = 3, TypeOfTraining = "sd", Date = new DateTime(2015, 7, 20, 18, 30, 25), Time = new DateTime(2015, 7, 20, 18, 30, 25) } },
            };
        }
        public static IEnumerable<object[]> GetIncorrectTrainersSchedule()
        {
            return new List<object[]>
            {
                new object[] { new TrainersSchedule() {Id= 0, ScheduleId = 0, TrainerId = 0, TypeOfTraining = "", Date = new DateTime(2015, 7, 20, 18, 30, 25), Time = new DateTime(2015, 7, 20, 18, 30, 25) } },
                new object[] { new TrainersSchedule() {Id=-1, ScheduleId = -1, TrainerId = -1, TypeOfTraining = "", Date = new DateTime(2015, 7, 20, 18, 30, 25), Time = new DateTime(2015, 7, 20, 18, 30, 25) } },
            };
        }

        public static IEnumerable<object[]> GetCorrectTrainersSchedule()
        {
            return new List<object[]>
            {
                new object[] { new TrainersSchedule() {Id = 1, ScheduleId = 1, TrainerId = 1, TypeOfTraining = "ds", Date = new DateTime(2015, 7, 20, 18, 30, 25), Time = new DateTime(2015, 7, 20, 18, 30, 25) } },
                new object[] { new TrainersSchedule() {Id = 2, ScheduleId = 2, TrainerId = 2, TypeOfTraining = "sdds", Date = new DateTime(2015, 7, 20, 18, 30, 25), Time = new DateTime(2015, 7, 20, 18, 30, 25) } },
                new object[] { new TrainersSchedule() {Id = 3, ScheduleId = 3, TrainerId = 3, TypeOfTraining = "sd", Date = new DateTime(2015, 7, 20, 18, 30, 25), Time = new DateTime(2015, 7, 20, 18, 30, 25) } },
            };
        }


        public TrainersSchedulesScheduleServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            TrainersScheduleRepositoryMoq = new Mock<ITrainersScheduleRepository>();

            repositoryWrapperMoq.Setup(x => x.TrainersSchedule).Returns(TrainersScheduleRepositoryMoq.Object);

            service = new TrainersScheduleService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            TrainersScheduleRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectTrainersSchedule))]
        public async Task GetById_correct(TrainersSchedule TrainersSchedule)
        {
            TrainersScheduleRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<TrainersSchedule, bool>>>())).ReturnsAsync(new List<TrainersSchedule> { TrainersSchedule });

            // Act
            var result = await service.GetById(TrainersSchedule.Id);

            // Assert
            Assert.Equal(TrainersSchedule.Id, result.Id);
            TrainersScheduleRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<TrainersSchedule, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectTrainersSchedule))]
        public async Task GetByid_incorrect(TrainersSchedule TrainersSchedule)
        {
            TrainersScheduleRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<TrainersSchedule, bool>>>())).ReturnsAsync(new List<TrainersSchedule> { TrainersSchedule });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(TrainersSchedule.Id));

            // Assert
            TrainersScheduleRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<TrainersSchedule, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(CreateCorrectTrainersSchedule))]

        public async Task CreateAsyncNewTrainersScheduleShouldNotCreateNewTrainersSchedule_correct(TrainersSchedule TrainersSchedule)
        {
            var newTrainersSchedule = TrainersSchedule;

            await service.Create(newTrainersSchedule);
            TrainersScheduleRepositoryMoq.Verify(x => x.Create(It.IsAny<TrainersSchedule>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectTrainersSchedule))]

        public async Task CreateAsyncNewTrainersScheduleShouldNotCreateNewTrainersSchedule_incorrect(TrainersSchedule TrainersSchedule)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(TrainersSchedule));
            TrainersScheduleRepositoryMoq.Verify(x => x.Delete(It.IsAny<TrainersSchedule>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectTrainersSchedule))]

        public async Task UpdateAsyncOldTrainersSchedule_correct(TrainersSchedule TrainersSchedule)
        {
            var newTrainersSchedule = TrainersSchedule;

            await service.Update(newTrainersSchedule);
            TrainersScheduleRepositoryMoq.Verify(x => x.Update(It.IsAny<TrainersSchedule>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectTrainersSchedule))]

        public async Task UpdateAsyncOldTrainersSchedule_incorrect(TrainersSchedule TrainersSchedule)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(TrainersSchedule));
            TrainersScheduleRepositoryMoq.Verify(x => x.Update(It.IsAny<TrainersSchedule>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectTrainersSchedule))]

        public async Task DeleteAsyncOldTrainersSchedule_correct(TrainersSchedule TrainersSchedule)
        {
            TrainersScheduleRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<TrainersSchedule, bool>>>())).ReturnsAsync(new List<TrainersSchedule> { TrainersSchedule });

            await service.Delete(TrainersSchedule.Id);

            var result = await service.GetById(TrainersSchedule.Id);
            TrainersScheduleRepositoryMoq.Verify(x => x.Delete(It.IsAny<TrainersSchedule>()), Times.Once);
            Assert.Equal(TrainersSchedule.Id, result.Id);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectTrainersSchedule))]

        public async Task DeleteAsyncOldTrainersSchedule_incorrect(TrainersSchedule TrainersSchedule)
        {
            TrainersScheduleRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<TrainersSchedule, bool>>>())).ReturnsAsync(new List<TrainersSchedule> { TrainersSchedule });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(TrainersSchedule.Id));
            TrainersScheduleRepositoryMoq.Verify(x => x.Delete(It.IsAny<TrainersSchedule>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

    }
}
