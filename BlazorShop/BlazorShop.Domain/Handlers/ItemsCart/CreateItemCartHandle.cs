using BlazorShop.Domain.Commands.Categories;
using BlazorShop.Domain.Commands.ItemsCart;
using BlazorShop.Domain.Entities;
using BlazorShop.Domain.Interfaces;
using BlazorShop.Shared.Commands;
using BlazorShop.Shared.Handlers.Contracts;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Handlers.ItemsCart
{
    public class CreateItemCartHandle : Notifiable<Notification>, IHandlerCommand<CreateItemCartCommand>
    {
        private readonly IItemCartRepository _itemCartRepository;

        public CreateItemCartHandle(IItemCartRepository itemCartRepository)
        {
            _itemCartRepository = itemCartRepository;
        }

        public ICommandResult Handler(CreateItemCartCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Correctly enter item data", command.Notifications);

            ItemCart item = new ItemCart(
                command.Quantity,
                command.IdCart,
                command.IdProduct
            );

            if (!item.IsValid)
                return new GenericCommandResult(false, "Correctly enter item data", item.Notifications);

            _itemCartRepository.Add(item);

            return new GenericCommandResult(true, "Item created successfully", item);
        }
    }
}
