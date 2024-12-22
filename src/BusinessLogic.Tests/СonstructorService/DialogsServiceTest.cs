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
    public class DialogServiceTest
    {
        private readonly DialogsService service;
        private readonly Mock<IDialogsRepository> DialogRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectDialog()
        {
            return new List<object[]>
            {
                new object[] { new Dialog() { TextDialogs = ""} },
            };
        }

        public static IEnumerable<object[]> CreateCorrectDialog()
        {
            return new List<object[]>
            {
                new object[] { new Dialog() { TextDialogs = "sd"} },
            };
        }
        public static IEnumerable<object[]> GetIncorrectDialog()
        {
            return new List<object[]>
            {
                new object[] { new Dialog() {DialogsId = 0, TextDialogs = ""} },
                new object[] { new Dialog() {DialogsId = -1, TextDialogs = ""} },
            };
        }

        public static IEnumerable<object[]> GetCorrectDialog()
        {
            return new List<object[]>
            {
                new object[] { new Dialog() {DialogsId = 1, TextDialogs = "sd"} },
                new object[] { new Dialog() {DialogsId = 2, TextDialogs = "ds"} },
                new object[] { new Dialog() {DialogsId = 3, TextDialogs = "gr"} },
            };
        }


        public DialogServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            DialogRepositoryMoq = new Mock<IDialogsRepository>();

            repositoryWrapperMoq.Setup(x => x.Dialogs).Returns(DialogRepositoryMoq.Object);

            service = new DialogsService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            DialogRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectDialog))]
        public async Task GetById_correct(Dialog Dialog)
        {
            DialogRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Dialog, bool>>>())).ReturnsAsync(new List<Dialog> { Dialog });

            // Act
            var result = await service.GetById(Dialog.DialogsId);

            // Assert
            Assert.Equal(Dialog.DialogsId, result.DialogsId);
            DialogRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Dialog, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectDialog))]
        public async Task GetByid_incorrect(Dialog Dialog)
        {
            DialogRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Dialog, bool>>>())).ReturnsAsync(new List<Dialog> { Dialog });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(Dialog.DialogsId));

            // Assert
            DialogRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Dialog, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(CreateCorrectDialog))]

        public async Task CreateAsyncNewDialogShouldNotCreateNewDialog_correct(Dialog Dialog)
        {
            var newDialog = Dialog;

            await service.Create(newDialog);
            DialogRepositoryMoq.Verify(x => x.Create(It.IsAny<Dialog>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectDialog))]

        public async Task CreateAsyncNewDialogShouldNotCreateNewDialog_incorrect(Dialog Dialog)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(Dialog));
            DialogRepositoryMoq.Verify(x => x.Delete(It.IsAny<Dialog>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectDialog))]

        public async Task UpdateAsyncOldDialog_correct(Dialog Dialog)
        {
            var newDialog = Dialog;

            await service.Update(newDialog);
            DialogRepositoryMoq.Verify(x => x.Update(It.IsAny<Dialog>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectDialog))]

        public async Task UpdateAsyncOldDialog_incorrect(Dialog Dialog)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(Dialog));
            DialogRepositoryMoq.Verify(x => x.Update(It.IsAny<Dialog>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectDialog))]

        public async Task DeleteAsyncOldDialog_correct(Dialog Dialog)
        {
            DialogRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Dialog, bool>>>())).ReturnsAsync(new List<Dialog> { Dialog });

            await service.Delete(Dialog.DialogsId);

            var result = await service.GetById(Dialog.DialogsId);
            DialogRepositoryMoq.Verify(x => x.Delete(It.IsAny<Dialog>()), Times.Once);
            Assert.Equal(Dialog.DialogsId, result.DialogsId);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectDialog))]

        public async Task DeleteAsyncOldDialog_incorrect(Dialog Dialog)
        {
            DialogRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Dialog, bool>>>())).ReturnsAsync(new List<Dialog> { Dialog });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(Dialog.DialogsId));
            DialogRepositoryMoq.Verify(x => x.Delete(It.IsAny<Dialog>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }
    }
}
