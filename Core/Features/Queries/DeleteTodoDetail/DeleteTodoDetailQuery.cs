using MediatR;
using Persistence.Models;

namespace Core.Features.Queries.DeleteTodoDetail;

public class DeleteTodoDetailQuery : IRequest<DeleteTodoDetailResponse>
{
    public Guid TodoDetailId { get; set; }
    public Todo TodoId { get; set; }
   
}
