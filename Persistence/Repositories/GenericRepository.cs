using Persistence.DatabaseContext;
using Persistence.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    
    private readonly TodoContext _context;
    private readonly IDatabase _cache;

    public GenericRepository(TodoContext context, IConnectionMultiplexer redisConnection)
    {
        _context = context;
        _cache = redisConnection.GetDatabase();
    }

    public List<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public void Add(T itemToAdd)
    {
        //_context.Set<T>().Add(itemToAdd);
        _context.Set<T>().Add(itemToAdd);

        var idProperty = itemToAdd.GetType().GetProperty("Id");
        if (idProperty != null)
        {
            var id = (Guid)idProperty.GetValue(itemToAdd);
            var cacheKey = $"{typeof(T).Name}:{id}";
            var serializedData = JsonSerializer.Serialize(itemToAdd);
            _cache.StringSet(cacheKey, serializedData, TimeSpan.FromMinutes(10));
        }
    }

    public void Remove(T itemToRemove)
    {
        _context.Set<T>().Remove(itemToRemove);
    }
    public int Count()
    {
        return _context.Set<T>().Count();
    }
    public int CountByTodoId(Guid todoId)
    {
        // Check if T has a property called TodoId
        var propertyInfo = typeof(T).GetProperty("TodoId");
        if (propertyInfo == null)
        {
            throw new InvalidOperationException("Type T does not contain a TodoId property.");
        }

        return _context.Set<T>()
                       .Where(item => (Guid)propertyInfo.GetValue(item) == todoId)
                       .Count();
    }

    public T? GetById(Guid id)
    {
        //return _context.Set<T>().Find(id);
        var cacheKey = $"{typeof(T).Name}:{id}";
        var cachedData = _cache.StringGet(cacheKey);

        if (!cachedData.IsNullOrEmpty)
        {
            return JsonSerializer.Deserialize<T>(cachedData);
        }

        var entity = _context.Set<T>().Find(id);

        if (entity != null)
        {
            var serializedData = JsonSerializer.Serialize(entity);
            _cache.StringSet(cacheKey, serializedData, TimeSpan.FromMinutes(10));
        }

        return entity;
    }
    public void BulkInsert(IEnumerable<TodoDetail> todoDetails)
    {
        _context.TodoDetail.AddRange(todoDetails);
        _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }


}