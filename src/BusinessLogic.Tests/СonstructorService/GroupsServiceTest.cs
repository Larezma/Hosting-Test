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
    public class GroupsServiceTest
    {
        private readonly GroupsService service;
        private readonly Mock<IGroupsRepository> GroupRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectGroup()
        {
            return new List<object[]>
            {
                new object[] { new Group() {OwnerGroups=0,GroupsName=""} },
                new object[] { new Group() {OwnerGroups=-1,GroupsName=""} },
            };
        }

        public static IEnumerable<object[]> CreateCorrectGroup()
        {
            return new List<object[]>
            {
                new object[] { new Group() {OwnerGroups=1,GroupsName="sd"} },
                new object[] { new Group() {OwnerGroups=2,GroupsName="ds"} },
                new object[] { new Group() {OwnerGroups=3,GroupsName="ds"} },
            };
        }
        public static IEnumerable<object[]> GetIncorrectGroup()
        {
            return new List<object[]>
            {
                new object[] { new Group() {GroupsId=0, OwnerGroups=0,GroupsName=""} },
                new object[] { new Group() {GroupsId=-1, OwnerGroups=1,GroupsName=""} },
            };
        }

        public static IEnumerable<object[]> GetCorrectGroup()
        {
            return new List<object[]>
            {
                new object[] { new Group() {GroupsId=1, OwnerGroups=1,GroupsName="dsds"} },
                new object[] { new Group() {GroupsId=2, OwnerGroups=2,GroupsName="dsd"} },
                new object[] { new Group() {GroupsId=3, OwnerGroups=3,GroupsName="sdds"} },
            };
        }


        public GroupsServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            GroupRepositoryMoq = new Mock<IGroupsRepository>();

            repositoryWrapperMoq.Setup(x => x.Group).Returns(GroupRepositoryMoq.Object);

            service = new GroupsService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            GroupRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectGroup))]
        public async Task GetById_correct(Group Group)
        {
            GroupRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Group, bool>>>())).ReturnsAsync(new List<Group> { Group });

            // Act
            var result = await service.GetById(Group.GroupsId);

            // Assert
            Assert.Equal(Group.GroupsId, result.GroupsId);
            GroupRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Group, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectGroup))]
        public async Task GetByid_incorrect(Group Group)
        {
            GroupRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Group, bool>>>())).ReturnsAsync(new List<Group> { Group });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(Group.GroupsId));

            // Assert
            GroupRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Group, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(CreateCorrectGroup))]

        public async Task CreateAsyncNewGroupShouldNotCreateNewGroup_correct(Group Group)
        {
            var newGroup = Group;

            await service.Create(newGroup);
            GroupRepositoryMoq.Verify(x => x.Create(It.IsAny<Group>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectGroup))]

        public async Task CreateAsyncNewGroupShouldNotCreateNewGroup_incorrect(Group Group)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(Group));
            GroupRepositoryMoq.Verify(x => x.Delete(It.IsAny<Group>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectGroup))]

        public async Task UpdateAsyncOldGroup_correct(Group Group)
        {
            var newGroup = Group;

            await service.Update(newGroup);
            GroupRepositoryMoq.Verify(x => x.Update(It.IsAny<Group>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectGroup))]

        public async Task UpdateAsyncOldGroup_incorrect(Group Group)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(Group));
            GroupRepositoryMoq.Verify(x => x.Update(It.IsAny<Group>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectGroup))]

        public async Task DeleteAsyncOldGroup_correct(Group Group)
        {
            GroupRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Group, bool>>>())).ReturnsAsync(new List<Group> { Group });

            await service.Delete(Group.GroupsId);

            var result = await service.GetById(Group.GroupsId);
            GroupRepositoryMoq.Verify(x => x.Delete(It.IsAny<Group>()), Times.Once);
            Assert.Equal(Group.GroupsId, result.GroupsId);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectGroup))]

        public async Task DeleteAsyncOldGroup_incorrect(Group Group)
        {
            GroupRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Group, bool>>>())).ReturnsAsync(new List<Group> { Group });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(Group.GroupsId));
            GroupRepositoryMoq.Verify(x => x.Delete(It.IsAny<Group>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

    }
}
