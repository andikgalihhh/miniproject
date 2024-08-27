using Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.Queries.AddTodo;

public class AddTodoQuery : IRequest<AddTodoResponse>
{
    public string Day { get; set; }
    public string Note { get; set; }
}
