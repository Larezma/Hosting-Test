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
    public class MessageUserServiceTest
    {
        private readonly MessageUsersService service;
        private readonly Mock<IMessageUsersRepository> MessageUserRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectMessageUser()
        {
            return new List<object[]>
            {
                new object[] { new MessageUser() {SenderId=0,ReceiverId=0,MessageContent=""} },
                new object[] { new MessageUser() {SenderId=-1,ReceiverId=1,MessageContent=""} },
            };
        }

        public static IEnumerable<object[]> CreateCorrectMessageUser()
        {
            return new List<object[]>
            {
                new object[] { new MessageUser() {SenderId=1,ReceiverId=2,MessageContent="dsds"} },
                new object[] { new MessageUser() {SenderId=2,ReceiverId=3,MessageContent="sds"} },
                new object[] { new MessageUser() {SenderId=3,ReceiverId=4,MessageContent="sdsd"} }
            };
        }
        public static IEnumerable<object[]> GetIncorrectMessageUser()
        {
            return new List<object[]>
            {
                new object[] { new MessageUser() {MessageId=0, SenderId=0,ReceiverId=0,MessageContent=""} },
                new object[] { new MessageUser() {MessageId=-1, SenderId=0,ReceiverId=1,MessageContent="sdds"} },
            };
        }

        public static IEnumerable<object[]> GetCorrectMessageUser()
        {
            return new List<object[]>
            {
                new object[] { new MessageUser() {MessageId=1, SenderId=1,ReceiverId=1,MessageContent="sdds"} },
                new object[] { new MessageUser() {MessageId=2, SenderId=2,ReceiverId=2,MessageContent="sdds"} },
                new object[] { new MessageUser() {MessageId=3, SenderId=3,ReceiverId=3,MessageContent="dsds"} },
            };
        }


        public MessageUserServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            MessageUserRepositoryMoq = new Mock<IMessageUsersRepository>();

            repositoryWrapperMoq.Setup(x => x.MessageUsers).Returns(MessageUserRepositoryMoq.Object);

            service = new MessageUsersService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            MessageUserRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectMessageUser))]
        public async Task GetById_correct(MessageUser MessageUser)
        {
            MessageUserRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<MessageUser, bool>>>())).ReturnsAsync(new List<MessageUser> { MessageUser });

            // Act
            var result = await service.GetById(MessageUser.MessageId);

            // Assert
            Assert.Equal(MessageUser.MessageId, result.MessageId);
            MessageUserRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<MessageUser, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectMessageUser))]
        public async Task GetByid_incorrect(MessageUser MessageUser)
        {
            MessageUserRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<MessageUser, bool>>>())).ReturnsAsync(new List<MessageUser> { MessageUser });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(MessageUser.MessageId));

            // Assert
            MessageUserRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<MessageUser, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(CreateCorrectMessageUser))]

        public async Task CreateAsyncNewMessageUserShouldNotCreateNewMessageUser_correct(MessageUser MessageUser)
        {
            var newMessageUser = MessageUser;

            await service.Create(newMessageUser);
            MessageUserRepositoryMoq.Verify(x => x.Create(It.IsAny<MessageUser>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectMessageUser))]

        public async Task CreateAsyncNewMessageUserShouldNotCreateNewMessageUser_incorrect(MessageUser MessageUser)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(MessageUser));
            MessageUserRepositoryMoq.Verify(x => x.Delete(It.IsAny<MessageUser>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectMessageUser))]

        public async Task UpdateAsyncOldMessageUser_correct(MessageUser MessageUser)
        {
            var newMessageUser = MessageUser;

            await service.Update(newMessageUser);
            MessageUserRepositoryMoq.Verify(x => x.Update(It.IsAny<MessageUser>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectMessageUser))]

        public async Task UpdateAsyncOldMessageUser_incorrect(MessageUser MessageUser)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(MessageUser));
            MessageUserRepositoryMoq.Verify(x => x.Update(It.IsAny<MessageUser>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectMessageUser))]

        public async Task DeleteAsyncOldMessageUser_correct(MessageUser MessageUser)
        {
            MessageUserRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<MessageUser, bool>>>())).ReturnsAsync(new List<MessageUser> { MessageUser });

            await service.Delete(MessageUser.MessageId);

            var result = await service.GetById(MessageUser.MessageId);
            MessageUserRepositoryMoq.Verify(x => x.Delete(It.IsAny<MessageUser>()), Times.Once);
            Assert.Equal(MessageUser.MessageId, result.MessageId);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectMessageUser))]

        public async Task DeleteAsyncOldMessageUser_incorrect(MessageUser MessageUser)
        {
            MessageUserRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<MessageUser, bool>>>())).ReturnsAsync(new List<MessageUser> { MessageUser });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(MessageUser.MessageId));
            MessageUserRepositoryMoq.Verify(x => x.Delete(It.IsAny<MessageUser>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

    }
}
