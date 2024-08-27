using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;
using Persistence.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;

public class TodoDetailRepository : GenericRepository<TodoDetail>, ITodoDetailRepository
{
   
    public TodoDetailRepository(TodoContext context, IConnectionMultiplexer redisConnection) : base(context, redisConnection)
    {
      
    }

    
}
