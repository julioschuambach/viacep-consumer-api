using System.Linq.Expressions;
using ViaCepConsumer.Api.Entities;

namespace ViaCepConsumer.Api.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task Insert(User user);
        Task<User?> Get(Expression<Func<User, bool>> filter);
        Task<IEnumerable<User>> Get();
        Task Save();
    }
}
