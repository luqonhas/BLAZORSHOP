using Flunt.Notifications;
using System;

namespace BlazorShop.Shared.Entities
{
    public abstract class Base : Notifiable<Notification>
    {
        protected Base()
        {
            Id = Guid.NewGuid();
            InsertDate = DateTime.Now;
        }

        public Guid Id { get; protected set; }
        public DateTime InsertDate { get; protected set; }
    }
}
