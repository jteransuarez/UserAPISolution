using System;
using System.Linq.Expressions;
using UserAPI.Core.Application.IRepositories;
using UserAPI.Core.Domain.Model;
using UserAPI.Infrastructure.DataContext;

namespace UserAPI.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private new readonly UserAPIDataContext _context;
        public UserRepository(UserAPIDataContext context) : base(context)
        {
            _context = context;

            if (GetAll().Count() == 0) 
            {
                var users = new List<User>
                {
                    new User
                    {
                        Username = "username1",
                        FirstName ="Joydip",
                        LastName ="Kanjilal",

                    },
                    new User
                    {
                        Username = "username2",
                        FirstName ="Yashavanth",
                        LastName ="Kanetkar",
                    },
                    new User
                    {
                        Username = "username3",
                        FirstName ="Niyati",
                        LastName ="Sumaran",
                    },
                    new User
                    {
                        Username = "username4",
                        FirstName ="Kalim ",
                        LastName ="Tantalian",
                    }
                };
                _ = AddRangeAsync(users);
            }
            
        }
    }
}
