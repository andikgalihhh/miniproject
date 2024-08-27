using Core.DTOs;
using Core.Features.Queries.AddTodo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.Queries.UpdateTodo;

public class UpdateTodoQuery : IRequest<UpdateTodoResponse>
{
    public Guid Todoid { get; set; }
    public string Day { get; set; }
    public DateTime TodayDate { get; set; }
    public string Note { get; set; }
    public List<TodoDetailDto> TodoDetails { get; set; }
}

