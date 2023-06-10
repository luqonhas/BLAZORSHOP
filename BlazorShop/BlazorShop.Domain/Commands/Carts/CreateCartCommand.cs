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
using System.Xml.Linq;

namespace BlazorShop.Domain.Commands.Carts
{
    public class CreateCartCommand : Notifiable<Notification>, ICommand
    {
        public CreateCartCommand() { }

        public CreateCartCommand(Guid idUser)
        {
            IdUser = idUser;
        }

        // FK's
        public Guid IdUser { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
            .Requires()
                .IsNotNull(IdUser, "idUser", "The 'idUser' field cannot be null!")
            );
        }
    }
}
