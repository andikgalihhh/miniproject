using MediatR;
using Persistence.Repositories;

namespace Core.Features.Queries.DeleteTodoDetail;

public class DeleteTodoDetailHandler : IRequestHandler<DeleteTodoDetailQuery, DeleteTodoDetailResponse>
{
    private readonly ITodoDetailRepository _todoDetailRepository;
    public DeleteTodoDetailHandler(ITodoDetailRepository todoDetailRepository)
    {
        _todoDetailRepository = todoDetailRepository;
    }
    public async Task<DeleteTodoDetailResponse> Handle(DeleteTodoDetailQuery query, CancellationToken cancellationToken)
    {
        var todoDetail = _todoDetailRepository.GetById(query.TodoDetailId);

        if (todoDetail is null)
            throw new KeyNotFoundException("Table specification not found.");

        _todoDetailRepository.Remove(todoDetail);
        await _todoDetailRepository.SaveChangesAsync(cancellationToken);

        return new DeleteTodoDetailResponse
        {
            TodoDetailId = todoDetail.TodoDetailId,
        };
    }
}