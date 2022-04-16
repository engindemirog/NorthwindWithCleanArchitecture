using Application.Features.Products.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries
{
    public class GetProductListQuery:IRequest<ProductListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, ProductListModel>
        {
            IProductRepository _productRepository;
            IMapper _mapper;

            public GetProductListQueryHandler(IProductRepository productRepository,IMapper mapper)
            {
                 _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<ProductListModel> Handle(GetProductListQuery request, CancellationToken cancellationToken)
            {
               IPaginate<Product> products =  await _productRepository.GetListAsync(
                    include: p => p.Include(p => p.Category),
                    index: request.PageRequest.Page,
                    size:request.PageRequest.PageSize
                    );

                ProductListModel model = _mapper.Map<ProductListModel>(products);
                return model;
            }
        }
    }
}
