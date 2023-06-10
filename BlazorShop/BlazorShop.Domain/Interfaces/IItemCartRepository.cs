using BlazorShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Interfaces
{
    public interface IItemCartRepository
    {
        // Commands:
        void Add(ItemCart item);

        void Delete(Guid? item);



        // Queries:
        IEnumerable<ItemCart> List();
        ItemCart SearchById(Guid? id);
    }
}
