
using BlazorShop.Domain.Entities;
using BlazorShop.Shared.Enums;
using BlazorShop.Shared.Queries;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Queries.Users
{
    public class SearchUserByUserNameQuery : Notifiable<Notification>, IQuery
    {
        public string UserName { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(UserName, "UserName", "The 'UserName' field cannot be empty")
                );
        }

        public class SearchUserByUserNameResult
        {
            public Guid Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public EnUserType UserType { get; set; }
            public DateTime? ModifyDate { get; set; }

            // Compositions
            public IReadOnlyCollection<Cart> Carts { get; set; }
            private List<Cart> _carts { get; set; }
        }
    }
}
