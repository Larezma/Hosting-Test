using System;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.Service;
using Domain.Interfaces.Wrapper;

namespace BusinessLogic.Services
{
    public class ProductsService : IProductsService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ProductsService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _repositoryWrapper.Products.FindALL();
        }

        public async Task<Product> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var products = await _repositoryWrapper.Products
                .FindByCondition(x => x.ProductId == id);
            return products.First();
        }

        public async Task Create(Product model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Product1) || !decimal.TryParse(model.Calories.ToString(), out _) || !decimal.TryParse(model.ProteinPer.ToString(), out _) 
                || !decimal.TryParse(model.FatPer.ToString(), out _) || !decimal.TryParse(model.CarbsPer.ToString(), out _) || string.IsNullOrEmpty(model.VitaminsAndMinerals) 
                || string.IsNullOrEmpty(model.NutritionalValue))
            {
                throw new ArgumentNullException("Одно из ключевых полей введенны неправильно !");
            }

            await _repositoryWrapper.Products.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Product model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (int.IsNegative(model.ProductId) || string.IsNullOrEmpty(model.Product1) || !decimal.TryParse(model.Calories.ToString(), out _) || !decimal.TryParse(model.ProteinPer.ToString(), out _)
                || !decimal.TryParse(model.FatPer.ToString(), out _) || !decimal.TryParse(model.CarbsPer.ToString(), out _) || string.IsNullOrEmpty(model.VitaminsAndMinerals)
                || string.IsNullOrEmpty(model.NutritionalValue))
            {
                throw new ArgumentNullException("id не может быть отрицательным!");
            }
            await _repositoryWrapper.Products.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("id не может быть отрицательным либо равен нулю!");
            }
            else if (id > int.MaxValue)
            {
                throw new ArgumentNullException("id не может превышать лимит int!");
            }
            var products = await _repositoryWrapper.Products
                .FindByCondition(x => x.ProductId == id);

            await _repositoryWrapper.Products.Delete(products.First());
            await _repositoryWrapper.Save();
        }
    }
}
