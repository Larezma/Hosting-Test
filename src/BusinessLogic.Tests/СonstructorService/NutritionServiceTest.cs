using BusinessLogic.Services;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Wrapper;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Xunit.Sdk;

namespace BusinessLogic.Tests.СonstructorService
{
    public class NutritionServiceTest
    {
        private readonly NutritionService service;
        private readonly Mock<INutritionRepository> NutritionRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectNutrition()
        {
            return new List<object[]>
            {
                new object[] { new Nutrition() {Product=0,MeanType="",MeanDeacription="",DateNutrition = new DateTime(2015, 7, 20, 18, 30, 25) } },
                new object[] { new Nutrition() {Product=-1,MeanType="",MeanDeacription="",DateNutrition = new DateTime(2015, 7, 20, 18, 30, 25) } },
                new object[] { new Nutrition() {Product=3,MeanType="dsd",MeanDeacription="",DateNutrition = new DateTime(2015, 7, 20, 18, 30, 25) } },
            };
        }

        public static IEnumerable<object[]> CreateCorrectNutrition()
        {
            return new List<object[]>
            {
                new object[] { new Nutrition() {Product=1,MeanType="ds",MeanDeacription="ds",DateNutrition = new DateTime(2015, 7, 20, 18, 30, 25) } },
                new object[] { new Nutrition() {Product=2,MeanType="sd",MeanDeacription="ds",DateNutrition = new DateTime(2015, 7, 20, 18, 30, 25) } },
                new object[] { new Nutrition() {Product=3,MeanType="dsd",MeanDeacription="ds",DateNutrition = new DateTime(2015, 7, 20, 18, 30, 25) } },
            };
        }
        public static IEnumerable<object[]> GetIncorrectNutrition()
        {
            return new List<object[]>
            {
                new object[] { new Nutrition() {NutritionId=0, Product=0,MeanType="",MeanDeacription="",DateNutrition = new DateTime(2015, 7, 20, 18, 30, 25) } },
                new object[] { new Nutrition() {NutritionId=-1, Product=2,MeanType="",MeanDeacription="",DateNutrition = new DateTime(2015, 7, 20, 18, 30, 25) } },
            };
        }

        public static IEnumerable<object[]> GetCorrectNutrition()
        {
            return new List<object[]>
            {
                new object[] { new Nutrition() {NutritionId=1, Product=1,MeanType="dssds",MeanDeacription="sdsd",DateNutrition = new DateTime(2015, 7, 20, 18, 30, 25) } },
                new object[] { new Nutrition() {NutritionId=2, Product=2,MeanType="dssds",MeanDeacription="sdsds",DateNutrition = new DateTime(2015, 7, 20, 18, 30, 25) } },
                new object[] { new Nutrition() {NutritionId=3, Product=3,MeanType="dss",MeanDeacription="dssd",DateNutrition = new DateTime(2015, 7, 20, 18, 30, 25) } },
            };
        }


        public NutritionServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            NutritionRepositoryMoq = new Mock<INutritionRepository>();

            repositoryWrapperMoq.Setup(x => x.Nutrition).Returns(NutritionRepositoryMoq.Object);

            service = new NutritionService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            NutritionRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectNutrition))]
        public async Task GetById_correct(Nutrition Nutrition)
        {
            NutritionRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Nutrition, bool>>>())).ReturnsAsync(new List<Nutrition> { Nutrition });

            // Act
            var result = await service.GetById(Nutrition.NutritionId);

            // Assert
            Assert.Equal(Nutrition.NutritionId, result.NutritionId);
            NutritionRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Nutrition, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectNutrition))]
        public async Task GetByid_incorrect(Nutrition Nutrition)
        {
            NutritionRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Nutrition, bool>>>())).ReturnsAsync(new List<Nutrition> { Nutrition });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(Nutrition.NutritionId));

            // Assert
            NutritionRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Nutrition, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);

        }

        [Theory]
        [MemberData(nameof(CreateCorrectNutrition))]

        public async Task CreateAsyncNewNutritionShouldNotCreateNewNutrition_correct(Nutrition Nutrition)
        {
            var newNutrition = Nutrition;

            await service.Create(newNutrition);
            NutritionRepositoryMoq.Verify(x => x.Create(It.IsAny<Nutrition>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectNutrition))]

        public async Task CreateAsyncNewNutritionShouldNotCreateNewNutrition_incorrect(Nutrition Nutrition)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(Nutrition));
            NutritionRepositoryMoq.Verify(x => x.Delete(It.IsAny<Nutrition>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectNutrition))]

        public async Task UpdateAsyncOldNutrition_correct(Nutrition Nutrition)
        {
            var newNutrition = Nutrition;

            await service.Update(newNutrition);
            NutritionRepositoryMoq.Verify(x => x.Update(It.IsAny<Nutrition>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectNutrition))]

        public async Task UpdateAsyncOldNutrition_incorrect(Nutrition Nutrition)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(Nutrition));
            NutritionRepositoryMoq.Verify(x => x.Update(It.IsAny<Nutrition>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectNutrition))]

        public async Task DeleteAsyncOldNutrition_correct(Nutrition Nutrition)
        {
            NutritionRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Nutrition, bool>>>())).ReturnsAsync(new List<Nutrition> { Nutrition });

            await service.Delete(Nutrition.NutritionId);

            var result = await service.GetById(Nutrition.NutritionId);
            NutritionRepositoryMoq.Verify(x => x.Delete(It.IsAny<Nutrition>()), Times.Once);
            Assert.Equal(Nutrition.NutritionId, result.NutritionId);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectNutrition))]

        public async Task DeleteAsyncOldNutrition_incorrect(Nutrition Nutrition)
        {
            NutritionRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Nutrition, bool>>>())).ReturnsAsync(new List<Nutrition> { Nutrition });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(Nutrition.NutritionId));
            NutritionRepositoryMoq.Verify(x => x.Delete(It.IsAny<Nutrition>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

    }
}