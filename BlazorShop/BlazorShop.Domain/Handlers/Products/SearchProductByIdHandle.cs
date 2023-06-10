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
    public class SearchProductByIdHandle : Notifiable<Notification>, IHandlerQuery<SearchProductByIdQuery>
    {
        private readonly IProductRepository _productRepository;

        public SearchProductByIdHandle(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IQueryResult Handler(SearchProductByIdQuery query)
        {
            query.Validate();

            if (!query.IsValid)
            {
                return new GenericQueryResult(false, "Correctly enter product data", query.Notifications);
            }

            var searchedProduct = _productRepository.SearchById(query.Id);

            if (searchedProduct == null)
            {
                return new GenericQueryResult(false, "Product not found", query.Notifications);
            }

            return new GenericQueryResult(true, "Products found!", searchedProduct);
        }
    }
}
