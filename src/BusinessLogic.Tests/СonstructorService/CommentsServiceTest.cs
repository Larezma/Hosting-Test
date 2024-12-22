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
    public class CommentServiceTest
    {
        private readonly CommentsService service;
        private readonly Mock<ICommentsRepository> CommentRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectComment()
        {
            return new List<object[]>
            {
                new object[] { new Comment() {CommentsId=0,ItemId=0,ItemType=0,CommentsText="" } },
                new object[] { new Comment() {CommentsId=-1,ItemId=-1,ItemType=-1,CommentsText="" } },
            };
        }

        public static IEnumerable<object[]> CreateCorrectComment()
        {
            return new List<object[]>
            {
                new object[] { new Comment() {CommentsId=1,ItemId=1,ItemType=1,CommentsText="sd" } },
                new object[] { new Comment() {CommentsId=2,ItemId=2,ItemType=2,CommentsText="ds" } },
            };
        }
        public static IEnumerable<object[]> GetIncorrectComment()
        {
            return new List<object[]>
            {
                new object[] { new Comment() {CommentsId=0, UserId=0,ItemId=0,ItemType=0,CommentsText="" } },
                new object[] { new Comment() {CommentsId=-1, UserId = -1,ItemId=0,ItemType=0,CommentsText="" } },
            };
        }

        public static IEnumerable<object[]> GetCorrectComment()
        {
            return new List<object[]>
            {
                new object[] { new Comment() {CommentsId=1, UserId = 1,ItemId=1,ItemType=1,CommentsText="sd" } },
                new object[] { new Comment() {CommentsId = 2, UserId = 2,ItemId=2,ItemType=2,CommentsText="ds" } },
                new object[] { new Comment() {CommentsId = 3, UserId = 3,ItemId=3,ItemType=3,CommentsText="f" } },
            };
        }


        public CommentServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            CommentRepositoryMoq = new Mock<ICommentsRepository>();

            repositoryWrapperMoq.Setup(x => x.Comments).Returns(CommentRepositoryMoq.Object);

            service = new CommentsService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            CommentRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectComment))]
        public async Task GetById_correct(Comment Comment)
        {
            CommentRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Comment, bool>>>())).ReturnsAsync(new List<Comment> { Comment });

            // Act
            var result = await service.GetById(Comment.CommentsId);

            // Assert
            Assert.Equal(Comment.CommentsId, result.CommentsId);
            CommentRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Comment, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectComment))]
        public async Task GetByid_incorrect(Comment Comment)
        {
            CommentRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Comment, bool>>>())).ReturnsAsync(new List<Comment> { Comment });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(Comment.CommentsId));

            // Assert
            CommentRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Comment, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(CreateCorrectComment))]

        public async Task CreateAsyncNewCommentShouldNotCreateNewComment_correct(Comment Comment)
        {
            var newComment = Comment;

            await service.Create(newComment);
            CommentRepositoryMoq.Verify(x => x.Create(It.IsAny<Comment>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectComment))]

        public async Task CreateAsyncNewCommentShouldNotCreateNewComment_incorrect(Comment Comment)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(Comment));
            CommentRepositoryMoq.Verify(x => x.Delete(It.IsAny<Comment>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectComment))]

        public async Task UpdateAsyncOldComment_correct(Comment Comment)
        {
            var newComment = Comment;

            await service.Update(newComment);
            CommentRepositoryMoq.Verify(x => x.Update(It.IsAny<Comment>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectComment))]

        public async Task UpdateAsyncOldComment_incorrect(Comment Comment)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(Comment));
            CommentRepositoryMoq.Verify(x => x.Update(It.IsAny<Comment>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectComment))]

        public async Task DeleteAsyncOldComment_correct(Comment Comment)
        {
            CommentRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Comment, bool>>>())).ReturnsAsync(new List<Comment> { Comment });

            await service.Delete(Comment.CommentsId);

            var result = await service.GetById(Comment.CommentsId);
            CommentRepositoryMoq.Verify(x => x.Delete(It.IsAny<Comment>()), Times.Once);
            Assert.Equal(Comment.CommentsId, result.CommentsId);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectComment))]

        public async Task DeleteAsyncOldComment_incorrect(Comment Comment)
        {
            CommentRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Comment, bool>>>())).ReturnsAsync(new List<Comment> { Comment });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(Comment.CommentsId));
            CommentRepositoryMoq.Verify(x => x.Delete(It.IsAny<Comment>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

    }
}
