using Persistence.Models;

namespace Persistence.Repositories;

public interface IGenericRepository<T> where T : class
{
    List<T> GetAll();
    T? GetById(Guid id);
    void Add(T item);
    void Remove(T itemToRemove);
    int Count();
    int CountByTodoId(Guid todoId);
    void BulkInsert(IEnumerable<TodoDetail> todoDetails);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}