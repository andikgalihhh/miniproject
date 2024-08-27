
using MediatR;
using Persistence.Repositories;

namespace Core.Features.Queries.DeleteTodo;

public class DeleteTodoHandler : IRequestHandler<DeleteTodoQuery, DeleteTodoResponse>
{
    private readonly ITodoRepository _todoRepository;
    public DeleteTodoHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }
    public async Task<DeleteTodoResponse> Handle(DeleteTodoQuery query, CancellationToken cancellationToken)
    {
        var Todo = _todoRepository.GetById(query.TodoId);

        if (Todo is null)
            throw new KeyNotFoundException("Table specification not found.");

        _todoRepository.Remove(Todo);
        await _todoRepository.SaveChangesAsync(cancellationToken);

        return new DeleteTodoResponse
        {   
            TodoId = Todo.TodoId,
        };
    }
}