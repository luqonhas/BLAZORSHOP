using BlazorShop.Shared.Entities;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Entities
{
    public class Cart : Base
    {
        public Cart(Guid idUser)
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotNull(idUser, "idUser", "The 'idUser' field cannot be null!")
            );

            if (IsValid)
            {
                IdUser = idUser;
                ModifyDate = null;
            }
        }

        public DateTime? ModifyDate { get; private set; }

        // FK's
        public Guid IdUser { get; private set; }
        public User Users { get; private set; }

        // Compositions
        public IReadOnlyCollection<ItemCart> ItemsCart { get; private set; }
        private List<ItemCart> _items { get; set; }

    }
}
