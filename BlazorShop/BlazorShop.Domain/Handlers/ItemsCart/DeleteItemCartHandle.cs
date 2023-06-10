using BlazorShop.Domain.Commands.ItemsCart;
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
    public class DeleteItemCartHandle : Notifiable<Notification>, IHandlerCommand<DeleteItemCartCommand>
    {
        private readonly IItemCartRepository _itemCartRepository;

        public DeleteItemCartHandle(IItemCartRepository itemCartRepository)
        {
            _itemCartRepository = itemCartRepository;
        }

        public ICommandResult Handler(DeleteItemCartCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Correctly inform the item you want to delete", command.Notifications);
            }

            var searchedItem = _itemCartRepository.SearchById(command.Id);

            if (searchedItem == null)
            {
                return new GenericCommandResult(false, "Item not found", command.Notifications);
            }

            _itemCartRepository.Delete(searchedItem.Id);

            return new GenericCommandResult(false, "Item deleted successfully!", "");
        }
    }
}
