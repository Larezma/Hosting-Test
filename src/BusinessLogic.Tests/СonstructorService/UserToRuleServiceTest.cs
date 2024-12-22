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
    public class UserToRuleServiceTest
    {
        private readonly UserToRuleService service;
        private readonly Mock<IUserToRuleRepository> UserToRuleRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectUserToRule()
        {
            return new List<object[]>
            {
                new object[] { new UserToRule() { UserId = 0, RoleId = 0} },
                new object[] { new UserToRule() { UserId = -1, RoleId = -1} },
            };
        }

        public static IEnumerable<object[]> CreateCorrectUserToRule()
        {
            return new List<object[]>
            {
                new object[] { new UserToRule() { UserId = 1, RoleId = 1} },
                new object[] { new UserToRule() { UserId = 2, RoleId = 2} },
                new object[] { new UserToRule() { UserId = 3, RoleId = 3} },
            };
        }
        public static IEnumerable<object[]> GetIncorrectUserToRule()
        {
            return new List<object[]>
            {
                new object[] { new UserToRule() {Id =0, UserId = 0, RoleId = 0} },
                new object[] { new UserToRule() {Id =-1, UserId = -1, RoleId = -1} },
            };
        }

        public static IEnumerable<object[]> GetCorrectUserToRule()
        {
            return new List<object[]>
            {
                new object[] { new UserToRule() {Id =1, UserId = 1, RoleId = 1} },
                new object[] { new UserToRule() {Id =2, UserId = 2, RoleId = 2} },
                new object[] { new UserToRule() {Id =3, UserId = 3, RoleId = 3} },
            };
        }


        public UserToRuleServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            UserToRuleRepositoryMoq = new Mock<IUserToRuleRepository>();

            repositoryWrapperMoq.Setup(x => x.UserToRule).Returns(UserToRuleRepositoryMoq.Object);

            service = new UserToRuleService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            UserToRuleRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectUserToRule))]
        public async Task GetById_correct(UserToRule UserToRule)
        {
            UserToRuleRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserToRule, bool>>>())).ReturnsAsync(new List<UserToRule> { UserToRule });

            // Act
            var result = await service.GetById(UserToRule.Id);

            // Assert
            Assert.Equal(UserToRule.Id, result.Id);
            UserToRuleRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<UserToRule, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectUserToRule))]
        public async Task GetByid_incorrect(UserToRule UserToRule)
        {
            UserToRuleRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserToRule, bool>>>())).ReturnsAsync(new List<UserToRule> { UserToRule });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(UserToRule.Id));

            // Assert
            UserToRuleRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<UserToRule, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(CreateCorrectUserToRule))]

        public async Task CreateAsyncNewUserToRuleShouldNotCreateNewUserToRule_correct(UserToRule UserToRule)
        {
            var newUserToRule = UserToRule;

            await service.Create(newUserToRule);
            UserToRuleRepositoryMoq.Verify(x => x.Create(It.IsAny<UserToRule>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectUserToRule))]

        public async Task CreateAsyncNewUserToRuleShouldNotCreateNewUserToRule_incorrect(UserToRule UserToRule)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(UserToRule));
            UserToRuleRepositoryMoq.Verify(x => x.Delete(It.IsAny<UserToRule>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectUserToRule))]

        public async Task UpdateAsyncOldUserToRule_correct(UserToRule UserToRule)
        {
            var newUserToRule = UserToRule;

            await service.Update(newUserToRule);
            UserToRuleRepositoryMoq.Verify(x => x.Update(It.IsAny<UserToRule>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectUserToRule))]

        public async Task UpdateAsyncOldUserToRule_incorrect(UserToRule UserToRule)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(UserToRule));
            UserToRuleRepositoryMoq.Verify(x => x.Update(It.IsAny<UserToRule>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectUserToRule))]

        public async Task DeleteAsyncOldUserToRule_correct(UserToRule UserToRule)
        {
            UserToRuleRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserToRule, bool>>>())).ReturnsAsync(new List<UserToRule> { UserToRule });

            await service.Delete(UserToRule.Id);

            var result = await service.GetById(UserToRule.Id);
            UserToRuleRepositoryMoq.Verify(x => x.Delete(It.IsAny<UserToRule>()), Times.Once);
            Assert.Equal(UserToRule.Id, result.Id);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectUserToRule))]

        public async Task DeleteAsyncOldUserToRule_incorrect(UserToRule UserToRule)
        {
            UserToRuleRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserToRule, bool>>>())).ReturnsAsync(new List<UserToRule> { UserToRule });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(UserToRule.Id));
            UserToRuleRepositoryMoq.Verify(x => x.Delete(It.IsAny<UserToRule>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

    }
}
