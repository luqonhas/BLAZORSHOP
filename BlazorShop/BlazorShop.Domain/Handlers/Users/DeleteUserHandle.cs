using BlazorShop.Domain.Commands.Users;
using BlazorShop.Domain.Interfaces;
using BlazorShop.Shared.Commands;
using BlazorShop.Shared.Handlers.Contracts;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Handlers.Users
{
    public class DeleteUserHandle : Notifiable<Notification>, IHandlerCommand<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;

        public DeleteUserHandle(IUserRepository userRepository, ICartRepository cartRepository)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
        }

        public ICommandResult Handler(DeleteUserCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Correctly inform the user you want to delete", command.Notifications);
            }

            var searchedUser = _userRepository.SearchById(command.Id);

            if (searchedUser == null)
            {
                return new GenericCommandResult(false, "User not found", command.Notifications);
            }

            _userRepository.Delete(searchedUser.Id);

            var cart = _cartRepository.SearchById(searchedUser.Id);
            if (cart != null)
            {
                _cartRepository.Delete(cart.Id);
            }

            return new GenericCommandResult(false, "User deleted successfully!", "");
        }
    }
}
