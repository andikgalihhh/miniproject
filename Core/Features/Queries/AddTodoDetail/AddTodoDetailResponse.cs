using Core.DTOs;
using Persistence.Models;

namespace Core.Features.Queries.AddTodoDetail;

public class AddTodoDetailResponse
{
    public List<TodoDetailDto> TodoDetails { get; set; }
}

