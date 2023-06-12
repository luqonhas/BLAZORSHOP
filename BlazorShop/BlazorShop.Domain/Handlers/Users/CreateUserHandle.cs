using BlazorShop.Domain.Commands.Users;
using BlazorShop.Domain.Commands.Carts;
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
using BlazorShop.Shared.Utils;

namespace BlazorShop.Domain.Handlers.Users
{
    public class CreateUserHandle : Notifiable<Notification>, IHandlerCommand<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;

        public CreateUserHandle(IUserRepository userRepository, ICartRepository cartRepository)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
        }


        public ICommandResult Handler(CreateUserCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Correctly enter user data", command.Notifications);
            }

            var emailExists = _userRepository.SearchByEmail(command.Email.ToLower());

            if (emailExists != null)
            {
                return new GenericCommandResult(false, "Existing e-mail", "Enter another e-mail");
            }

            bool SpacesUserName = command.UserName.Contains(" ");

            if (SpacesUserName)
            {
                return new GenericCommandResult(false, "UserName cannot have spaces", "Enter another userName");
            }

            string encryptedPassword = Password.Encrypt(command.Password);

            User newUser = new User(command.UserName.ToLower(), command.Email.ToLower(), encryptedPassword, command.UserType);

            if (!newUser.IsValid)
            {
                return new GenericCommandResult(false, "Invalid user data", newUser.Notifications);
            }

            _userRepository.Add(newUser);

            var cartId = Guid.Empty;
            var cart = new Cart(newUser.Id);
            _cartRepository.Add(cart);

            if (cart.Id != Guid.Empty)
            {
                cartId = cart.Id;
            }

            if (cartId == Guid.Empty)
            {
                return new GenericCommandResult(false, "Failed to create cart", "Cart creation failed");
            }

            return new GenericCommandResult(true, "User created successfully!", newUser);
        }
    }
}
