using Core.DTOs;
using Core.Features.Queries.UpdateTodo;
using MediatR;
using Persistence.Models;

namespace Core.Features.Queries.UpdateTodoDetail;

public class UpdateTodoDetailQuery : IRequest<UpdateTodoDetailResponse>
{
    public Guid TodoDetailId { get; set; }
    public Todo TodoId { get; set; }
    public string Activity { get; set; }
    public string Category { get; set; }
    public string DetailNote { get; set; }
    public List<TodoDetailDto> TodoDetails { get; set; }
}
