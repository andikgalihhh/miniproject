using MediatR;
using Persistence.Models;
using Persistence.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Features.Queries.UpdateTodo;

public class UpdateTodoHandler : IRequestHandler<UpdateTodoQuery, UpdateTodoResponse>
{
    private readonly ITodoRepository _todoRepository;

    public UpdateTodoHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }
    public async Task<UpdateTodoResponse> Handle(UpdateTodoQuery query, CancellationToken cancellationToken)
    {
        var todoSpecification = _todoRepository.GetById(query.Todoid);

        if (todoSpecification is null)
            throw new KeyNotFoundException("Todo specification not found.");

        todoSpecification.Day = query.Day;
        todoSpecification.TodayDate = query.TodayDate;
        todoSpecification.Note = query.Note;

        await _todoRepository.SaveChangesAsync(cancellationToken);

        return new UpdateTodoResponse
        {
            Day = todoSpecification.Day,
            TodayDate = todoSpecification.TodayDate,
            Note = todoSpecification.Note,
          
        };
    }

}

