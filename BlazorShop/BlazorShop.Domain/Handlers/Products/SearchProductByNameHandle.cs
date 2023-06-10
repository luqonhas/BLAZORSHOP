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
    public class SearchProductByNameHandle : Notifiable<Notification>, IHandlerQuery<SearchProductByNameQuery>
    {
        private readonly IProductRepository _productRepository;

        public SearchProductByNameHandle(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IQueryResult Handler(SearchProductByNameQuery query)
        {
            query.Validate();

            if (!query.IsValid)
            {
                return new GenericQueryResult(false, "Correctly enter product data", query.Notifications);
            }

            var searchedProduct = _productRepository.SearchByName(query.Name);

            if (searchedProduct == null)
            {
                return new GenericQueryResult(false, "Product not found", query.Notifications);
            }

            return new GenericQueryResult(true, "Products found!", searchedProduct);
        }
    }
}
