using BusinessLogic.Services;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
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
    public class UserNutritionServiceTest
    {
        private readonly UserNutritionService service;
        private readonly Mock<IUserNutritionRepository> UserNutritionRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectUserNutrition()
        {
            return new List<object[]>
            {
                new object[] { new UserNutrition() { UserId = 0, NutritionId = 0, DateOfAdmission = new DateTime(2015, 7, 20, 18, 30, 25),AppointmentTime = new DateTime(2015, 7, 20, 18, 30, 25),NutritionType = "",  Report="" } },
                new object[] { new UserNutrition() { UserId = -1, NutritionId = -1, DateOfAdmission = new DateTime(2015, 7, 20, 18, 30, 25),AppointmentTime = new DateTime(2015, 7, 20, 18, 30, 25), NutritionType = "", Report ="" } },
            };
        }

        public static IEnumerable<object[]> CreateCorrectUserNutrition()
        {
            return new List<object[]>
            {
                new object[] { new UserNutrition() { UserId = 1, NutritionId = 1, DateOfAdmission = new DateTime(2015, 7, 20, 18, 30, 25),AppointmentTime = new DateTime(2015, 7, 20, 18, 30, 25), NutritionType = "ds", Report ="ds" } },
                new object[] { new UserNutrition() { UserId = 2, NutritionId = 2, DateOfAdmission = new DateTime(2015, 7, 20, 18, 30, 25),AppointmentTime = new DateTime(2015, 7, 20, 18, 30, 25), NutritionType = "ds", Report ="ds" } },
                new object[] { new UserNutrition() { UserId = 3, NutritionId = 3, DateOfAdmission = new DateTime(2015, 7, 20, 18, 30, 25),AppointmentTime = new DateTime(2015, 7, 20, 18, 30, 25), NutritionType = "ds", Report ="ds" } },
            };
        }
        public static IEnumerable<object[]> GetIncorrectUserNutrition()
        {
            return new List<object[]>
            {
                new object[] { new UserNutrition() {UserNutritionId = 0, UserId = 0, NutritionId = 0, DateOfAdmission = new DateTime(2015, 7, 20, 18, 30, 25),AppointmentTime = new DateTime(2015, 7, 20, 18, 30, 25), NutritionType = "", Report ="" } },
                new object[] { new UserNutrition() {UserNutritionId = -1, UserId = -1, NutritionId = -1, DateOfAdmission = new DateTime(2015, 7, 20, 18, 30, 25),AppointmentTime = new DateTime(2015, 7, 20, 18, 30, 25), NutritionType = "", Report ="" } },
            };
        }

        public static IEnumerable<object[]> GetCorrectUserNutrition()
        {
            return new List<object[]>
            {
                new object[] { new UserNutrition() {UserNutritionId = 1, UserId = 1, NutritionId = 1, DateOfAdmission = new DateTime(2015, 7, 20, 18, 30, 25),AppointmentTime = new DateTime(2015, 7, 20, 18, 30, 25), NutritionType = "ds", Report ="ds" } },
                new object[] { new UserNutrition() {UserNutritionId = 2, UserId = 2, NutritionId = 2, DateOfAdmission = new DateTime(2015, 7, 20, 18, 30, 25),AppointmentTime = new DateTime(2015, 7, 20, 18, 30, 25), NutritionType = "ds", Report ="ds" } },
                new object[] { new UserNutrition() {UserNutritionId = 3, UserId = 3, NutritionId = 3, DateOfAdmission = new DateTime(2015, 7, 20, 18, 30, 25),AppointmentTime = new DateTime(2015, 7, 20, 18, 30, 25), NutritionType = "sdsd", Report ="ds" } },
            };
        }


        public UserNutritionServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            UserNutritionRepositoryMoq = new Mock<IUserNutritionRepository>();

            repositoryWrapperMoq.Setup(x => x.UserNutrition).Returns(UserNutritionRepositoryMoq.Object);

            service = new UserNutritionService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            UserNutritionRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectUserNutrition))]
        public async Task GetById_correct(UserNutrition UserNutrition)
        {
            UserNutritionRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserNutrition, bool>>>())).ReturnsAsync(new List<UserNutrition> { UserNutrition });

            // Act
            var result = await service.GetById(UserNutrition.UserNutritionId);

            // Assert
            Assert.Equal(UserNutrition.UserNutritionId, result.UserNutritionId);
            UserNutritionRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<UserNutrition, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectUserNutrition))]
        public async Task GetByid_incorrect(UserNutrition UserNutrition)
        {
            UserNutritionRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserNutrition, bool>>>())).ReturnsAsync(new List<UserNutrition> { UserNutrition });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(UserNutrition.UserNutritionId));

            // Assert
            UserNutritionRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<UserNutrition, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(CreateCorrectUserNutrition))]

        public async Task CreateAsyncNewUserNutritionShouldNotCreateNewUserNutrition_correct(UserNutrition UserNutrition)
        {
            var newUserNutrition = UserNutrition;

            await service.Create(newUserNutrition);
            UserNutritionRepositoryMoq.Verify(x => x.Create(It.IsAny<UserNutrition>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectUserNutrition))]

        public async Task CreateAsyncNewUserNutritionShouldNotCreateNewUserNutrition_incorrect(UserNutrition UserNutrition)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(UserNutrition));
            UserNutritionRepositoryMoq.Verify(x => x.Delete(It.IsAny<UserNutrition>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectUserNutrition))]

        public async Task UpdateAsyncOldUserNutrition_correct(UserNutrition UserNutrition)
        {
            var newUserNutrition = UserNutrition;

            await service.Update(newUserNutrition);
            UserNutritionRepositoryMoq.Verify(x => x.Update(It.IsAny<UserNutrition>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectUserNutrition))]

        public async Task UpdateAsyncOldUserNutrition_incorrect(UserNutrition UserNutrition)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(UserNutrition));
            UserNutritionRepositoryMoq.Verify(x => x.Update(It.IsAny<UserNutrition>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectUserNutrition))]

        public async Task DeleteAsyncOldUserNutrition_correct(UserNutrition UserNutrition)
        {
            UserNutritionRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserNutrition, bool>>>())).ReturnsAsync(new List<UserNutrition> { UserNutrition });

            await service.Delete(UserNutrition.UserNutritionId);

            var result = await service.GetById(UserNutrition.UserNutritionId);
            UserNutritionRepositoryMoq.Verify(x => x.Delete(It.IsAny<UserNutrition>()), Times.Once);
            Assert.Equal(UserNutrition.UserNutritionId, result.UserNutritionId);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectUserNutrition))]

        public async Task DeleteAsyncOldUserNutrition_incorrect(UserNutrition UserNutrition)
        {
            UserNutritionRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserNutrition, bool>>>())).ReturnsAsync(new List<UserNutrition> { UserNutrition });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(UserNutrition.UserNutritionId));
            UserNutritionRepositoryMoq.Verify(x => x.Delete(It.IsAny<UserNutrition>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }
    }
}