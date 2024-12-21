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
    public class ProductServiceTest
    {
        private readonly ProductsService service;
        private readonly Mock<IProductsRepository> productRepositoryMoq;

        public static IEnumerable<object[]> CreateIncorrectProduct()
        {
            return new List<object[]>
            {
                new object[] { new Product() { Product1 = "", Calories = -111.0m, ProteinPer = -111.0m, FatPer = -111.0m, CarbsPer = -111.0m, VitaminsAndMinerals = "", NutritionalValue = "" } },
                new object[] { new Product() { Product1 = "sdsd", Calories = -111.0m, ProteinPer = -111.0m, FatPer = -111.0m, CarbsPer = -111.0m, VitaminsAndMinerals = "", NutritionalValue = "" } },
                new object[] { new Product() { Product1 = "111", Calories = 111.0m, ProteinPer = -111.0m, FatPer = 111.0m, CarbsPer = -111.0m, VitaminsAndMinerals = "sdsd", NutritionalValue = "" } },
            };
        }

        public static IEnumerable<object[]> CreateCorrectProduct()
        {
            return new List<object[]>
            {
                new object[] { new Product() { Product1 = "sddss", Calories = 111.0m, ProteinPer = 111.0m, FatPer = 111.0m, CarbsPer = 111.0m, VitaminsAndMinerals = "dssd", NutritionalValue = "dsds" } },
                new object[] { new Product() { Product1 = "sddss1", Calories = 121.0m, ProteinPer = 111.0m, FatPer = 111.0m, CarbsPer = 111.0m, VitaminsAndMinerals = "dssd", NutritionalValue = "dsds" } },
                new object[] { new Product() { Product1 = "sddss2", Calories = 111.0m, ProteinPer = 131.0m, FatPer = 111.0m, CarbsPer = 111.0m, VitaminsAndMinerals = "dssd", NutritionalValue = "dsds" } },
            };
        }
        public static IEnumerable<object[]> GetIncorrectProduct()
        {
            return new List<object[]>
            {
                new object[] { new Product() {ProductId=0, Product1 = "", Calories = -111.0m, ProteinPer = -111.0m, FatPer = -111.0m, CarbsPer = -111.0m, VitaminsAndMinerals = "", NutritionalValue = "" } },
                new object[] { new Product() {ProductId=-1, Product1 = "dsds", Calories = -111.0m, ProteinPer = -111.0m, FatPer = -111.0m, CarbsPer = -111.0m, VitaminsAndMinerals = "", NutritionalValue = "sds" } },
            };
        }

        public static IEnumerable<object[]> GetCorrectProduct()
        {
            return new List<object[]>
            {
                new object[] { new Product() {ProductId=1,  Product1 = "sddss", Calories = 111.0m, ProteinPer = 111.0m, FatPer = 111.0m, CarbsPer = 111.0m, VitaminsAndMinerals = "dssd", NutritionalValue = "dsds" } },
                new object[] { new Product() {ProductId=2,  Product1 = "sddss", Calories = 111.0m, ProteinPer = 111.0m, FatPer = 111.0m, CarbsPer = 111.0m, VitaminsAndMinerals = "dssd", NutritionalValue = "dsds" } },
            };
        }


        public ProductServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            productRepositoryMoq = new Mock<IProductsRepository>();

            repositoryWrapperMoq.Setup(x => x.Products).Returns(productRepositoryMoq.Object);

            service = new ProductsService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task GetALL()
        {
            await service.GetAll();
            productRepositoryMoq.Verify(x => x.FindALL());
        }


        [Theory]
        [MemberData(nameof(GetCorrectProduct))]
        public async Task GetById_correct(Product Product)
        {
            productRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(new List<Product> { Product });

            // Act
            var result = await service.GetById(Product.ProductId);

            // Assert
            Assert.Equal(Product.ProductId, result.ProductId);
            productRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectProduct))]
        public async Task GetByid_incorrect(Product Product)
        {
            productRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(new List<Product> { Product });

            // Act
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.GetById(Product.ProductId));

            // Assert
            productRepositoryMoq.Verify(x => x.FindByCondition(It.IsAny<Expression<Func<Product, bool>>>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);

        }

        [Theory]
        [MemberData(nameof(CreateCorrectProduct))]

        public async Task CreateAsyncNewProductShouldNotCreateNewProduct_correct(Product Product)
        {
            var newProduct = Product;

            await service.Create(newProduct);
            productRepositoryMoq.Verify(x => x.Create(It.IsAny<Product>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateIncorrectProduct))]

        public async Task CreateAsyncNewProductShouldNotCreateNewProduct_incorrect(Product Product)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(Product));
            productRepositoryMoq.Verify(x => x.Delete(It.IsAny<Product>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectProduct))]

        public async Task UpdateAsyncOldProduct_correct(Product Product)
        {
            var newProduct = Product;

            await service.Update(newProduct);
            productRepositoryMoq.Verify(x => x.Update(It.IsAny<Product>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectProduct))]

        public async Task UpdateAsyncOldProduct_incorrect(Product Product)
        {
            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Update(Product));
            productRepositoryMoq.Verify(x => x.Update(It.IsAny<Product>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

        [Theory]
        [MemberData(nameof(GetCorrectProduct))]

        public async Task DeleteAsyncOldProduct_correct(Product Product)
        {
            productRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(new List<Product> { Product });

            await service.Delete(Product.ProductId);

            var result = await service.GetById(Product.ProductId);
            productRepositoryMoq.Verify(x => x.Delete(It.IsAny<Product>()), Times.Once);
            Assert.Equal(Product.ProductId, result.ProductId);
        }


        [Theory]
        [MemberData(nameof(GetIncorrectProduct))]

        public async Task DeleteAsyncOldProduct_incorrect(Product Product)
        {
            productRepositoryMoq.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(new List<Product> { Product });

            var result = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Delete(Product.ProductId));
            productRepositoryMoq.Verify(x => x.Delete(It.IsAny<Product>()), Times.Never);
            Assert.IsType<ArgumentNullException>(result);
        }

    }
}