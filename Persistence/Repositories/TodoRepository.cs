using Persistence.DatabaseContext;
using Persistence.Models;
using StackExchange.Redis;

namespace Persistence.Repositories;

public class TodoRepository : GenericRepository<Todo>, ITodoRepository
{
    public TodoRepository(TodoContext context, IConnectionMultiplexer redisConnection) : base(context, redisConnection)
    {
    }

   
}
