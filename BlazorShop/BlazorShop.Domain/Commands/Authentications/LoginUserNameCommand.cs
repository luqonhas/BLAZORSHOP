using BlazorShop.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Commands.Authentications
{
    public class LoginUserNameCommand : Notifiable<Notification>, ICommand
    {
        public LoginUserNameCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; set; }
        public string Password { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotNull(UserName, "userName", "The 'userName' field cannot be null")
                .IsGreaterThan(Password, 6, "The 'password' field must have at least 6 characters")
            );
        }
    }
}
