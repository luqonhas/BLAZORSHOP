using BlazorShop.Domain.Commands.Carts;
using BlazorShop.Domain.Commands.Categories;
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

namespace BlazorShop.Domain.Handlers.Carts
{
    public class CreateCartHandle : Notifiable<Notification>, IHandlerCommand<CreateCartCommand>
    {
        private readonly ICartRepository _cartRepository;

        public CreateCartHandle(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public ICommandResult Handler(CreateCartCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Correctly enter cart data", command.Notifications);

            Cart cart = new Cart(
                command.IdUser
            );

            if (!cart.IsValid)
                return new GenericCommandResult(false, "Correctly enter cart data", cart.Notifications);

            _cartRepository.Add(cart);

            return new GenericCommandResult(true, "Cart created successfully", cart);
        }
    }
}