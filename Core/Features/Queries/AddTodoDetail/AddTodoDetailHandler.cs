using Core.DTOs;
using Core.Features.Queries.AddTodoDetail;
using MediatR;
using Persistence.Models;
using Persistence.Repositories;

namespace Core.Features.Commands.AddTodoDetail
{
    public class AddTodoDetailHandler : IRequestHandler<AddTodoDetailQuery, AddTodoDetailResponse>
    {
        private readonly ITodoDetailRepository _todoDetailRepository;

        public AddTodoDetailHandler(ITodoDetailRepository todoDetailRepository)
        {
            _todoDetailRepository = todoDetailRepository;
        }

        public async Task<AddTodoDetailResponse> Handle(AddTodoDetailQuery request, CancellationToken cancellationToken)
        {


            // Create TodoDetail objects for bulk insert
            var todoDetails = request.TodoDetails.Select(detail => new TodoDetail
            {
                TodoDetailId = Guid.NewGuid(),
                TodoId = detail.TodoId,
                Activity = detail.Activity,
                Category = Enum.Parse<CategoryEnum>(detail.Category),
                DetailNote = detail.DetailNote
            }).ToList();


            // Perform bulk insert
            _todoDetailRepository.BulkInsert(todoDetails);

            // Prepare response
            var response = new AddTodoDetailResponse
            {
                TodoDetails = todoDetails.Select(detail => new TodoDetailDto
                {
                    
                    TodoId = detail.TodoId,
                    Activity = detail.Activity,
                    Category = detail.Category.ToString(),
                    DetailNote = detail.DetailNote
                }).ToList()
            };  

            return response;
        }
    }
}