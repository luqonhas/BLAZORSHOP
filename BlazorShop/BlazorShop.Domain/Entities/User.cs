using BlazorShop.Shared.Entities;
using BlazorShop.Shared.Enums;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Entities
{
    public class User : Base
    {
        public User(string userName, string email, string password, EnUserType userType)
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(userName, "userName", "The 'userName' field cannot be empty")
                    .IsEmail(email, "email", "Enter a valid email address")
                    .IsGreaterThan(password, 6, "The 'password' field must have at least 6 characters")
                    .IsNotNull(userType, "userType", "The 'userType' field cannot be null")
            );

            if (IsValid)
            {
                UserName = userName;
                Email = email;
                Password = password;
                UserType = userType;
                ModifyDate = null;
            }
        }

        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public EnUserType UserType { get; private set; }
        public DateTime? ModifyDate { get; private set; }

        // Compositions
        public IReadOnlyCollection<Cart> Carts { get; private set; }
        private List<Cart> _carts { get; set; }
    }
}