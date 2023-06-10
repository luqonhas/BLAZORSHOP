using BlazorShop.Domain.Interfaces;
using BlazorShop.Domain.Queries.Carts;
using BlazorShop.Domain.Queries.Products;
using BlazorShop.Shared.Handlers.Contracts;
using BlazorShop.Shared.Queries;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Handlers.Carts
{
    public class SearchCartByUserIdHandle : Notifiable<Notification>, IHandlerQuery<SearchCartByUserIdQuery>
    {
        private readonly ICartRepository _cartRepository;

        public SearchCartByUserIdHandle(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public IQueryResult Handler(SearchCartByUserIdQuery query)
        {
            query.Validate();

            if (!query.IsValid)
            {
                return new GenericQueryResult(false, "Correctly enter cart data", query.Notifications);
            }

            var searchedItem = _cartRepository.SearchByUserId(query.IdUser);

            if (searchedItem == null)
            {
                return new GenericQueryResult(false, "Cart not found", query.Notifications);
            }

            return new GenericQueryResult(true, "Carts found!", searchedItem);
        }
    }
}
