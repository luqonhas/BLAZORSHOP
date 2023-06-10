using BlazorShop.Domain.Interfaces;
using BlazorShop.Domain.Queries.ItemsCart;
using BlazorShop.Shared.Handlers.Contracts;
using BlazorShop.Shared.Queries;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Handlers.ItemsCart
{
    public class SearchItemCartByIdHandle : Notifiable<Notification>, IHandlerQuery<SearchItemCartByIdQuery>
    {
        private readonly IItemCartRepository _itemCartRepository;

        public SearchItemCartByIdHandle(IItemCartRepository itemCartRepository)
        {
            _itemCartRepository = itemCartRepository;
        }

        public IQueryResult Handler(SearchItemCartByIdQuery query)
        {
            query.Validate();

            if (!query.IsValid)
            {
                return new GenericQueryResult(false, "Correctly enter item data", query.Notifications);
            }

            var searchedItem = _itemCartRepository.SearchById(query.Id);

            if (searchedItem == null)
            {
                return new GenericQueryResult(false, "Item not found", query.Notifications);
            }

            return new GenericQueryResult(true, "Items found!", searchedItem);
        }
    }
}
