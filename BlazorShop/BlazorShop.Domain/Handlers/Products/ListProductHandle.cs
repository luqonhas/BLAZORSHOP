using BlazorShop.Domain.Commands.Products;
using BlazorShop.Domain.Interfaces;
using BlazorShop.Domain.Queries.Products;
using BlazorShop.Shared.Handlers.Contracts;
using BlazorShop.Shared.Queries;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Handlers.Products
{
    public class ListProductHandle : Notifiable<Notification>, IHandlerQuery<ListProductQuery>
    {
        private readonly IProductRepository _productRepository;

        public ListProductHandle(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IQueryResult Handler(ListProductQuery query)
        {
            var list = _productRepository.List();

            return new GenericQueryResult(true, "Products found!", list);
        }
    }
}
