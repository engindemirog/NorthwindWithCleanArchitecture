using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Rules
{
    public class ProductBusinessRules
    {
        IProductRepository _productRepository;

        public ProductBusinessRules(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task ProductNameShouldNotBeExisted(string productName)
        {
            Product? result = await _productRepository.GetAsync(p=>p.ProductName==productName);
            if (result != null) throw new BusinessException("Ürün zaten mevcut");
        }


    }
}
