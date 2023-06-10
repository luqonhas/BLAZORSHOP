using BlazorShop.Domain.Interfaces;
using BlazorShop.Domain.Queries.Users;
using BlazorShop.Shared.Handlers.Contracts;
using BlazorShop.Shared.Queries;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Handlers.Users
{
    public class ListUserHandle : Notifiable<Notification>, IHandlerQuery<ListUserQuery>
    {
        private readonly IUserRepository _userRepository;

        public ListUserHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IQueryResult Handler(ListUserQuery query)
        {
            var list = _userRepository.List();

            return new GenericQueryResult(true, "Users found!", list);
        }
    }
}
