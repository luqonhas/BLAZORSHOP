using BlazorShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Interfaces
{
    public interface ICartRepository
    {
        // Commands:
        void Add(Cart cart);
        void Delete(Guid? category);



        // Queries:
        IEnumerable<Cart> List();
        Cart SearchById(Guid? id);
        Cart SearchByUserId(Guid? id);
    }
}