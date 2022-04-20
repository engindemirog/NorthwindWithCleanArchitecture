using Application.Features.Products.Commands.Dtos;
using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Mailing;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommand:IRequest<CreatedProductDto>
    {
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public string QuantityPerUnit { get; set; }


        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreatedProductDto>
        {
            IProductRepository _productRepository;
            IMapper _mapper;
            ProductBusinessRules _productBusinessRules;
            IMailService _mailService;
            public CreateProductCommandHandler(
                IProductRepository productRepository,
                IMapper mapper, 
                ProductBusinessRules productBusinessRules,
                IMailService mailService)
            {
                _productRepository = productRepository;
                _mapper = mapper;
                _productBusinessRules = productBusinessRules;
                _mailService = mailService;
            }

            public async Task<CreatedProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {

                await _productBusinessRules.ProductNameShouldNotBeExisted(request.ProductName);

                Product product = _mapper.Map<Product>(request);
                Product createdProduct =  await _productRepository.AddAsync(product);
                CreatedProductDto createdProductDto = _mapper.Map<CreatedProductDto>(createdProduct);

                Mail mail = new Mail
                {
                    ToFullName="Adem Erbaş",
                    ToEmail = "ademerdas@c.com",
                    Subject ="Yeni ürün eklendi",
                    HtmlBody = "Yeni ürün eklendi"

                };

                _mailService.SendMail(mail);


                return createdProductDto;
            }
        }


    }
}
