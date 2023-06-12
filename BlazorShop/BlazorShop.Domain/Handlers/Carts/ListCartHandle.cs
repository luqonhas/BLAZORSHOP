using BlazorShop.Domain.Interfaces;
using BlazorShop.Domain.Queries.Carts;
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
    public class ListCartHandle : Notifiable<Notification>, IHandlerQuery<ListCartQuery>
    {
        private readonly ICartRepository _cartRepository;

        public ListCartHandle(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public IQueryResult Handler(ListCartQuery query)
        {
            var list = _cartRepository.List();

            return new GenericQueryResult(true, "Carts found!", list);
        }
    }
}
