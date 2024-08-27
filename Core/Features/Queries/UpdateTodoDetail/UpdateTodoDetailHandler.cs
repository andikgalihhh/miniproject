using MediatR;
using Persistence.Models;
using Persistence.Repositories;

namespace Core.Features.Queries.UpdateTodoDetail;

public class UpdateDetailTodoHandler : IRequestHandler<UpdateTodoDetailQuery, UpdateTodoDetailResponse>
{
    private readonly ITodoDetailRepository _todoDetailRepository;

    public UpdateDetailTodoHandler(ITodoDetailRepository todoDetailRepository)
    {
        _todoDetailRepository = todoDetailRepository;
    }
    public async Task<UpdateTodoDetailResponse> Handle(UpdateTodoDetailQuery query, CancellationToken cancellationToken)
    {
        var todoDetailSpecification = _todoDetailRepository.GetById(query.TodoDetailId);

        if (todoDetailSpecification is null)
            throw new KeyNotFoundException("Todo specification not found.");

        todoDetailSpecification.Activity = query.Activity;
        todoDetailSpecification.DetailNote = query.DetailNote;
        todoDetailSpecification.Category = Enum.Parse<CategoryEnum>(query.Category);

        await _todoDetailRepository.SaveChangesAsync(cancellationToken);

        return new UpdateTodoDetailResponse
        {
            Activity = todoDetailSpecification.Activity,
            DetailNote = todoDetailSpecification.DetailNote,
            Category = todoDetailSpecification.Category.ToString(),

        };
    }

}

