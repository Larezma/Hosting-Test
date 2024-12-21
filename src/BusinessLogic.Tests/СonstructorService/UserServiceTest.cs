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
    public class UserServiceTest
    {
        private readonly UserService service;
        private readonly Mock<IUserRepository> userRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectUsers()
        {
            return new List<object[]>
            {
                new object[] { new User() { UserName = "", Email = "", Password = "", RoleUser = 0, UserImg = "", PhoneNumber = "", AboutMe = "", } },
                new object[] { new User() { UserName = "sddssd", Email = "", Password = "", RoleUser = 0, UserImg = "", PhoneNumber = "", AboutMe = "", } },
                new object[] { new User() { UserName = "sdsdsd", Email = "sdsdsd", Password = "", RoleUser = 0, UserImg = "", PhoneNumber = "", AboutMe = "", } },
            };
        }

        public static IEnumerable<object[]> CreateCorrectUsers()
        {
            return new List<object[]>
            {
                new object[] { new User() {UserName = "xxc",Email = "dssd",Password = "sdsd",RoleUser = 0,UserImg = "",PhoneNumber = "dssd", AboutMe = "",} },
                new object[] { new User() {UserName = "sddssd",Email = "ssdds",Password = "sdsds",RoleUser = 0,UserImg = "sdsdsd",PhoneNumber = "sdsd",AboutMe = "",} },
                new object[] { new User() {UserName = "sdsdsd",Email = "sdsdsd",Password = "dss",RoleUser = 0,UserImg = "",PhoneNumber = "sdsdsd",AboutMe = "11",} },
            };
        }
        public static IEnumerable<object[]> GetIncorrectUsers()
        {
            return new List<object[]>
            {
                new object[] { new User() {UserId = 0,UserName = "",Email = "",Password = "",RoleUser = 0,UserImg = "", CreateAt = DateTime.Now, UpdateAt = DateTime.Today, PhoneNumber = "", AboutMe = "",} },
                new object[] { new User() {UserId = -1,UserName = "sdsdsd",Email = "sdsdsd",Password = "",RoleUser = 0,UserImg = "", CreateAt = DateTime.Now, UpdateAt = DateTime.Today, PhoneNumber = "",AboutMe = "",} },
            };
        }
        public static IEnumerable<object[]> GetCorrectUsers()
        {
            return new List<object[]>
            {
                new object[] { new User() { UserId = 1, UserName = "xxc",Email = "dssd",Password = "sdsd",RoleUser = 0,UserImg = "", CreateAt = DateTime.Now,UpdateAt = DateTime.Today, PhoneNumber = "dssd", AboutMe = "",} },
                new object[] { new User() { UserId = 2, UserName = "sddssd",Email = "ssdds",Password = "sdsds",RoleUser = 0,UserImg = "sdsdsd",CreateAt = DateTime.Now, UpdateAt = DateTime.Today, PhoneNumber = "sdsd",AboutMe = "",} },
                new object[] { new User() { UserId = 3, UserName = "sdsdsd",Email = "sdsdsd",Password = "dss",RoleUser = 0,UserImg = "", CreateAt = DateTime.Now, UpdateAt = DateTime.Today, PhoneNumber = "sdsdsd",AboutMe = "11",} },
            };
        }

        public UserServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IUserRepository>();

            repositoryWrapperMoq.Setup(x => x.User).Returns(userRepositoryMoq.Object);

            service = new UserService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            userRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectUsers))]
        public async Task GetById_correct(User user)
        {
            userRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new List<User> { user });

            // Act
            var result = await service.GetById(user.UserId);

            // Assert
            Assert.Equal(user.UserId, result.UserId);
            userRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectUsers))]
        public async Task GetByid_incorrect(User user)
        {
            userRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new List<User> { user });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException> (() =>  service.GetById(user.UserId));

            // Assert
            userRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<User, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(CreateCorrectUsers))]

        public async Task CreateAsyncNewUserShouldNotCreateNewUser_correct(User user)
        {
            var newUser = user;

            await service.Create(newUser);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectUsers))]

        public async Task CreateAsyncNewUserShouldNotCreateNewUser_incorrect(User user)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(user));
            userRepositoryMoq.Verify(x => x.Delete(It.IsAny<User>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectUsers))]

        public async Task UpdateAsyncOldUser_correct(User user)
        {
            var newUser = user;

            await service.Update(newUser);
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectUsers))]

        public async Task UpdateAsyncOldUser_incorrect(User user)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(user));
            userRepositoryMoq.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectUsers))]

        public async Task DeleteAsyncOldUser_correct(User user)
        {
            userRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new List<User> { user });

            await service.Delete(user.UserId);

            var result = await service.GetById(user.UserId);
            userRepositoryMoq.Verify(x => x.Delete(It.IsAny<User>()), Times.Once);
            Assert.Equal(user.UserId, result.UserId);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectUsers))]

        public async Task DeleteAsyncOldUser_incorrect(User user)
        {
            userRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new List<User> { user });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(user.UserId));
            userRepositoryMoq.Verify(x => x.Delete(It.IsAny<User>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }
    }
}
