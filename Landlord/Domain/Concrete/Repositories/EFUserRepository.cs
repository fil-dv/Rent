using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Identity;

namespace Domain.Concrete.Repositories
{
    public class EFPUserRepository : IUserRepository
    {
        EFDbContext _context = new EFDbContext();

        public IEnumerable<User> Users
        {
            get
            {
                return _context.Users;
            }
        }
    }
}
