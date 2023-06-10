using BlazorShop.Domain.Entities;
using BlazorShop.Domain.Interfaces;
using BlazorShop.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BlazorShopContext _ctx;

        public UserRepository(BlazorShopContext ctx)
        {
            _ctx = ctx;
        }



        // Commands:
        public void Add(User user)
        {
            _ctx.Users.Add(user);
            _ctx.SaveChanges();
        }

        public void Delete(Guid? id)
        {
            _ctx.Users.Remove(SearchById(id));
            _ctx.SaveChanges();
        }



        // Queries:
        public IEnumerable<User> List()
        {
            return _ctx.Users
                .AsNoTracking()
                .ToList();
        }

        public User SearchByEmail(string email)
        {
            return _ctx.Users.FirstOrDefault(x => x.Email == email);
        }

        public User SearchById(Guid? id)
        {
            return _ctx.Users.FirstOrDefault(x => x.Id == id);
        }
    }
}
