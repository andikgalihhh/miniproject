using MediatR;
using Persistence.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Features.Queries.GetAllTodo
{
    public class GetAllTodoHandler : IRequestHandler<GetAllTodoQuery, GetAllTodoResponse>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITodoDetailRepository _todoDetailRepository;

        public GetAllTodoHandler(ITodoRepository todoRepository, ITodoDetailRepository todoDetailRepository)
        {
            _todoRepository = todoRepository;
            _todoDetailRepository = todoDetailRepository;
        }

        public async Task<GetAllTodoResponse> Handle(GetAllTodoQuery query, CancellationToken cancellationToken)
        {
            // Get the total count of todos
            var totalCount = _todoRepository.Count();

            // Get the todos for the requested page
            var todos = _todoRepository.GetAll()
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList();

            var todoDtos = todos.Select(todo => new TodoDto
            {
                TodoId = todo.TodoId,
                Day = todo.Day,
                TodayDate = todo.TodayDate,
                Note = todo.Note,
                DetailCount = _todoDetailRepository.CountByTodoId(todo.TodoId)
            }).ToList();

            return new GetAllTodoResponse
            {
                Todos = todoDtos,
                TotalCount = _todoRepository.Count()
            };
        }
    }
}
