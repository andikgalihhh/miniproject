using MediatR;

namespace Core.Features.Queries.GetAllTodo
{
    public class GetAllTodoQuery : IRequest<GetAllTodoResponse>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
