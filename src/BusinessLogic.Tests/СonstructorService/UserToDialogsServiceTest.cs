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
    public class UserToDialogsServiceTest
    {
        private readonly UserToDialogsService service;
        private readonly Mock<IUserToDialogRepository> UserToDialogRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectUserToDialog()
        {
            return new List<object[]>
            {
                new object[] { new UserToDialog() {DialogId = 0, UserId = 0 } },
                new object[] { new UserToDialog() {DialogId = -1, UserId = -1 } },
            };
        }

        public static IEnumerable<object[]> CreateCorrectUserToDialog()
        {
            return new List<object[]>
            {
                new object[] { new UserToDialog() {DialogId = 1, UserId = 1 } },
                new object[] { new UserToDialog() {DialogId = 2, UserId = 2 } },
                new object[] { new UserToDialog() {DialogId = 3, UserId = 3 } },
            };
        }
        public static IEnumerable<object[]> GetIncorrectUserToDialog()
        {
            return new List<object[]>
            {
                new object[] { new UserToDialog() {Id = 0, DialogId = 0, UserId = 0 } },
                new object[] { new UserToDialog() {Id =-1, DialogId = -1, UserId = -1 } },
            };
        }

        public static IEnumerable<object[]> GetCorrectUserToDialog()
        {
            return new List<object[]>
            {
                new object[] { new UserToDialog() {Id = 1, DialogId = 1, UserId = 1 } },
                new object[] { new UserToDialog() {Id = 2, DialogId = 2, UserId = 2 } },
                new object[] { new UserToDialog() {Id = 3, DialogId = 3, UserId = 3 } },
            };
        }


        public UserToDialogsServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            UserToDialogRepositoryMoq = new Mock<IUserToDialogRepository>();

            repositoryWrapperMoq.Setup(x => x.UserToDialog).Returns(UserToDialogRepositoryMoq.Object);

            service = new UserToDialogsService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            UserToDialogRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectUserToDialog))]
        public async Task GetById_correct(UserToDialog UserToDialog)
        {
            UserToDialogRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserToDialog, bool>>>())).ReturnsAsync(new List<UserToDialog> { UserToDialog });

            // Act
            var result = await service.GetById(UserToDialog.UserId);

            // Assert
            Assert.Equal(UserToDialog.UserId, result.UserId);
            UserToDialogRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<UserToDialog, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectUserToDialog))]
        public async Task GetByid_incorrect(UserToDialog UserToDialog)
        {
            UserToDialogRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserToDialog, bool>>>())).ReturnsAsync(new List<UserToDialog> { UserToDialog });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(UserToDialog.UserId));

            // Assert
            UserToDialogRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<UserToDialog, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(CreateCorrectUserToDialog))]

        public async Task CreateAsyncNewUserToDialogShouldNotCreateNewUserToDialog_correct(UserToDialog UserToDialog)
        {
            var newUserToDialog = UserToDialog;

            await service.Create(newUserToDialog);
            UserToDialogRepositoryMoq.Verify(x => x.Create(It.IsAny<UserToDialog>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectUserToDialog))]

        public async Task CreateAsyncNewUserToDialogShouldNotCreateNewUserToDialog_incorrect(UserToDialog UserToDialog)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(UserToDialog));
            UserToDialogRepositoryMoq.Verify(x => x.Delete(It.IsAny<UserToDialog>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectUserToDialog))]

        public async Task UpdateAsyncOldUserToDialog_correct(UserToDialog UserToDialog)
        {
            var newUserToDialog = UserToDialog;

            await service.Update(newUserToDialog);
            UserToDialogRepositoryMoq.Verify(x => x.Update(It.IsAny<UserToDialog>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectUserToDialog))]

        public async Task UpdateAsyncOldUserToDialog_incorrect(UserToDialog UserToDialog)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(UserToDialog));
            UserToDialogRepositoryMoq.Verify(x => x.Update(It.IsAny<UserToDialog>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectUserToDialog))]

        public async Task DeleteAsyncOldUserToDialog_correct(UserToDialog UserToDialog)
        {
            UserToDialogRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserToDialog, bool>>>())).ReturnsAsync(new List<UserToDialog> { UserToDialog });

            await service.Delete(UserToDialog.UserId);

            var result = await service.GetById(UserToDialog.UserId);
            UserToDialogRepositoryMoq.Verify(x => x.Delete(It.IsAny<UserToDialog>()), Times.Once);
            Assert.Equal(UserToDialog.UserId, result.UserId);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectUserToDialog))]

        public async Task DeleteAsyncOldUserToDialog_incorrect(UserToDialog UserToDialog)
        {
            UserToDialogRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<UserToDialog, bool>>>())).ReturnsAsync(new List<UserToDialog> { UserToDialog });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(UserToDialog.UserId));
            UserToDialogRepositoryMoq.Verify(x => x.Delete(It.IsAny<UserToDialog>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }
    }
}