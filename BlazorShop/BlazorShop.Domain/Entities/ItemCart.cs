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
    public class ItemCart : Base
    {
        public ItemCart(int quantity, Guid idCart, Guid idProduct)
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotNull(quantity, "quantity", "The 'quantity' field cannot be null!")
                .IsNotNull(idCart, "idCart", "The 'idCart' field cannot be null!")
                .IsNotNull(idProduct, "idProduct", "The 'idProduct' field cannot be null!")
            );

            if (IsValid)
            {
                Quantity = quantity;
                IdCart = idCart;
                IdProduct = idProduct;
                ModifyDate = null;
            }
        }

        public int Quantity { get; private set; }
        public DateTime? ModifyDate { get; private set; }

        // FK's
        public Guid IdCart { get; set; }
        public Cart Carts { get; set; }

        // FK's
        public Guid IdProduct { get; set; }
        public Product Products { get; set; }
    }
}
