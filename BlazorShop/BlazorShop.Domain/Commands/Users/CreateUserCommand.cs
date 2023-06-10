using BlazorShop.Domain.Entities;
using BlazorShop.Shared.Commands;
using BlazorShop.Shared.Enums;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Commands.Users
{
    public class CreateUserCommand : Notifiable<Notification>, ICommand
    {
        public CreateUserCommand() { }

        public CreateUserCommand(string userName, string email, string password, EnUserType userType)
        {
            UserName = userName;
            Email = email;
            Password = password;
            UserType = userType;
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EnUserType UserType { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotEmpty(UserName, "userName", "The 'userName' field cannot be empty")
                .IsEmail(Email, "email", "Enter a valid email address")
                .IsGreaterThan(Password, 6, "The 'password' field must have at least 6 characters")
                .IsNotNull(UserType, "userType", "The 'userType' field cannot be null")
            );
        }
    }
}
