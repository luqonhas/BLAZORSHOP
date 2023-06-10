using BlazorShop.Domain.Commands.ItemsCart;
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
    public class ListItemCartHandle : Notifiable<Notification>, IHandlerQuery<ListItemCartQuery>
    {
        private readonly IItemCartRepository _itemCartRepository;

        public ListItemCartHandle(IItemCartRepository itemCartRepository)
        {
            _itemCartRepository = itemCartRepository;
        }

        public IQueryResult Handler(ListItemCartQuery query)
        {
            var list = _itemCartRepository.List();

            return new GenericQueryResult(true, "Items found!", list);
        }
    }
}
