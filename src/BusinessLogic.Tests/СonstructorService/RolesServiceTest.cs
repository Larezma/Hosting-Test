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
    public class RoleServiceTest
    {
        private readonly RolesService service;
        private readonly Mock<IRolesRepository> roleRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectRole()
        {
            return new List<object[]>
            {
                new object[] { new Role() {Role1 = ""} },
            };
        }

        public static IEnumerable<object[]> CreateCorrectRole()
        {
            return new List<object[]>
            {
               new object[] { new Role() { Role1 = "dssdsd"} },
            };
        }

        public static IEnumerable<object[]> GetIncorrectRole()
        {
            return new List<object[]>
            {
                new object[] { new Role() { Id= 0, Role1 = ""} },
                new object[] { new Role() { Id = -1, Role1 = "" } },
            };
        }

        public static IEnumerable<object[]> GetCorrectRole()
        {
            return new List<object[]>
            {
                new object[] { new Role() { Id= 1, Role1 = "sdds"} },
                new object[] { new Role() { Id = 3, Role1 = "dsds" } },
            };
        }


        public RoleServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            roleRepositoryMoq = new Mock<IRolesRepository>();

            repositoryWrapperMoq.Setup(x => x.Roles).Returns(roleRepositoryMoq.Object);

            service = new RolesService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            roleRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectRole))]
        public async Task GetById_correct(Role Role)
        {
            roleRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Role, bool>>>())).ReturnsAsync(new List<Role> { Role });

            // Act
            var result = await service.GetById(Role.Id);

            // Assert
            Assert.Equal(Role.Id, result.Id);
            roleRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Role, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectRole))]
        public async Task GetByid_incorrect(Role Role)
        {
            roleRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Role, bool>>>())).ReturnsAsync(new List<Role> { Role });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(Role.Id));

            // Assert
            roleRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Role, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);

        }

        [Theory]
        [MemberData(nameof(CreateCorrectRole))]

        public async Task CreateAsyncNewRoleShouldNotCreateNewRole_correct(Role Role)
        {
            var newRole = Role;

            await service.Create(newRole);
            roleRepositoryMoq.Verify(x => x.Create(It.IsAny<Role>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectRole))]

        public async Task CreateAsyncNewRoleShouldNotCreateNewRole_incorrect(Role Role)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(Role));
            roleRepositoryMoq.Verify(x => x.Delete(It.IsAny<Role>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectRole))]

        public async Task UpdateAsyncOldRole_correct(Role Role)
        {
            var newRole = Role;

            await service.Update(newRole);
            roleRepositoryMoq.Verify(x => x.Update(It.IsAny<Role>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectRole))]

        public async Task UpdateAsyncOldRole_incorrect(Role Role)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(Role));
            roleRepositoryMoq.Verify(x => x.Update(It.IsAny<Role>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectRole))]

        public async Task DeleteAsyncOldRole_correct(Role Role)
        {
            roleRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Role, bool>>>())).ReturnsAsync(new List<Role> { Role });

            await service.Delete(Role.Id);

            var result = await service.GetById(Role.Id);
            roleRepositoryMoq.Verify(x => x.Delete(It.IsAny<Role>()), Times.Once);
            Assert.Equal(Role.Id, result.Id);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectRole))]

        public async Task DeleteAsyncOldRole_incorrect(Role Role)
        {
            roleRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Role, bool>>>())).ReturnsAsync(new List<Role> { Role });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(Role.Id));
            roleRepositoryMoq.Verify(x => x.Delete(It.IsAny<Role>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

    }
}
