using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.Queries.DeleteTodo
{
    public class DeleteTodoQuery : IRequest<DeleteTodoResponse>
    {
        public Guid TodoId { get; set; }
    }
}
