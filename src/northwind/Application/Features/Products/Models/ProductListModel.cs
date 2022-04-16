using Application.Features.Products.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Models
{
    public class ProductListModel:BasePageableModel
    {
        public IList<ProductListDto> Items { get; set; }
    }
}
