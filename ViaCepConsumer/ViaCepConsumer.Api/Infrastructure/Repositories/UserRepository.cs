using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ViaCepConsumer.Api.Entities;
using ViaCepConsumer.Api.Infrastructure.Contexts;
using ViaCepConsumer.Api.Infrastructure.Repositories.Interfaces;

namespace ViaCepConsumer.Api.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _dbContext;

        public UserRepository(DatabaseContext dbContext)
            => _dbContext = dbContext;

        public async Task Insert(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await Save();
        }

        public async Task<User?> Get(Expression<Func<User, bool>> filter)
        {
            User? user = await _dbContext.Users
                                         .AsNoTracking()
                                         .Include(x => x.Roles)
                                         .FirstOrDefaultAsync(filter);

            return user;
        }

        public async Task<IEnumerable<User>> Get()
        {
            List<User> users = await _dbContext.Users
                                               .AsNoTracking()
                                               .Include(x => x.Roles)
                                               .ToListAsync();

            return users;
        }

        public async Task Save()
            => await _dbContext.SaveChangesAsync();
    }
}
