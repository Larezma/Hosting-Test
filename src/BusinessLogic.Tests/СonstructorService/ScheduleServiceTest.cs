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
    public class ScheduleServiceTest
    {
        private readonly ScheduleService service;
        private readonly Mock<IScheduleRepository> scheduleRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectSchedule()
        {
            return new List<object[]>
            {
                new object[] { new Schedule() { TrainingId=0,TrainerId = 0,TrainingType="", DayOfWeek="sds",StartTime= new DateTime(2015, 7, 20, 18, 30, 25),EndTime= new DateTime(2015, 7, 20, 18, 30, 25) } },
                new object[] { new Schedule() { TrainingId=-1,TrainerId = 0,TrainingType="", DayOfWeek="sds",StartTime= new DateTime(2015, 7, 20, 18, 30, 25),EndTime= new DateTime(2015, 7, 20, 18, 30, 25) } },
                new object[] { new Schedule() { TrainingId=0,TrainerId = -1,TrainingType="dss", DayOfWeek="",StartTime= new DateTime(2015, 7, 20, 18, 30, 25),EndTime= new DateTime(2015, 7, 20, 18, 30, 25) } },
            };
        }

        public static IEnumerable<object[]> CreateCorrectSchedule()
        {
            return new List<object[]>
            {
                new object[] { new Schedule() { TrainingId=4,TrainerId = 1,TrainingType="dss", DayOfWeek="sdds",StartTime= new DateTime(2015, 7, 20, 18, 30, 25),EndTime= new DateTime(2015, 7, 20, 18, 30, 25) } },
                new object[] { new Schedule() { TrainingId=5,TrainerId = 2,TrainingType="dss", DayOfWeek="dsds",StartTime= new DateTime(2015, 7, 20, 18, 30, 25),EndTime= new DateTime(2015, 7, 20, 18, 30, 25) } },
                new object[] { new Schedule() { TrainingId=6,TrainerId = 3,TrainingType="dss", DayOfWeek="dssd",StartTime= new DateTime(2015, 7, 20, 18, 30, 25),EndTime= new DateTime(2015, 7, 20, 18, 30, 25) } },
            };
        }
        public static IEnumerable<object[]> GetIncorrectSchedule()
        {
            return new List<object[]>
            {
                new object[] { new Schedule() { ScheduleId=0, TrainingId=4,TrainerId = 1,TrainingType="", DayOfWeek="sdds",StartTime= new DateTime(2015, 7, 20, 18, 30, 25),EndTime= new DateTime(2015, 7, 20, 18, 30, 25) } },
                new object[] { new Schedule() { ScheduleId=-1, TrainingId=4,TrainerId = 1,TrainingType="dss", DayOfWeek="",StartTime= new DateTime(2015, 7, 20, 18, 30, 25),EndTime= new DateTime(2015, 7, 20, 18, 30, 25) } },
            };
        }

        public static IEnumerable<object[]> GetCorrectSchedule()
        {
            return new List<object[]>
            {
                new object[] { new Schedule() { ScheduleId=1, TrainingId=4,TrainerId = 1,TrainingType="dss", DayOfWeek="sdds",StartTime= new DateTime(2015, 7, 20, 18, 30, 25),EndTime= new DateTime(2015, 7, 20, 18, 30, 25) } },
                new object[] { new Schedule() { ScheduleId=2, TrainingId=4,TrainerId = 1,TrainingType="dss", DayOfWeek="sdds",StartTime= new DateTime(2015, 7, 20, 18, 30, 25),EndTime= new DateTime(2015, 7, 20, 18, 30, 25) } },
            };
        }


        public ScheduleServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            scheduleRepositoryMoq = new Mock<IScheduleRepository>();

            repositoryWrapperMoq.Setup(x => x.Schedule).Returns(scheduleRepositoryMoq.Object);

            service = new ScheduleService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            scheduleRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectSchedule))]
        public async Task GetById_correct(Schedule Schedule)
        {
            scheduleRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Schedule, bool>>>())).ReturnsAsync(new List<Schedule> { Schedule });

            // Act
            var result = await service.GetById(Schedule.ScheduleId);

            // Assert
            Assert.Equal(Schedule.ScheduleId, result.ScheduleId);
            scheduleRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Schedule, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectSchedule))]
        public async Task GetByid_incorrect(Schedule Schedule)
        {
            scheduleRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Schedule, bool>>>())).ReturnsAsync(new List<Schedule> { Schedule });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(Schedule.ScheduleId));

            // Assert
            scheduleRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Schedule, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);

        }

        [Theory]
        [MemberData(nameof(CreateCorrectSchedule))]

        public async Task CreateAsyncNewScheduleShouldNotCreateNewSchedule_correct(Schedule Schedule)
        {
            var newSchedule = Schedule;

            await service.Create(newSchedule);
            scheduleRepositoryMoq.Verify(x => x.Create(It.IsAny<Schedule>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectSchedule))]

        public async Task CreateAsyncNewScheduleShouldNotCreateNewSchedule_incorrect(Schedule Schedule)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(Schedule));
            scheduleRepositoryMoq.Verify(x => x.Delete(It.IsAny<Schedule>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectSchedule))]

        public async Task UpdateAsyncOldSchedule_correct(Schedule Schedule)
        {
            var newSchedule = Schedule;

            await service.Update(newSchedule);
            scheduleRepositoryMoq.Verify(x => x.Update(It.IsAny<Schedule>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectSchedule))]

        public async Task UpdateAsyncOldSchedule_incorrect(Schedule Schedule)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(Schedule));
            scheduleRepositoryMoq.Verify(x => x.Update(It.IsAny<Schedule>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectSchedule))]

        public async Task DeleteAsyncOldSchedule_correct(Schedule Schedule)
        {
            scheduleRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Schedule, bool>>>())).ReturnsAsync(new List<Schedule> { Schedule });

            await service.Delete(Schedule.ScheduleId);

            var result = await service.GetById(Schedule.ScheduleId);
            scheduleRepositoryMoq.Verify(x => x.Delete(It.IsAny<Schedule>()), Times.Once);
            Assert.Equal(Schedule.ScheduleId, result.ScheduleId);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectSchedule))]

        public async Task DeleteAsyncOldSchedule_incorrect(Schedule Schedule)
        {
            scheduleRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Schedule, bool>>>())).ReturnsAsync(new List<Schedule> { Schedule });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(Schedule.ScheduleId));
            scheduleRepositoryMoq.Verify(x => x.Delete(It.IsAny<Schedule>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

    }
}
