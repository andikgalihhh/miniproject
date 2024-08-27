using MediatR;
using Persistence.Models;
using Persistence.Repositories;
using Core.Services;
using Core.Features.Queries.AddTodo;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Core.Features.Commands.AddTodo
{
    public class AddTodoHandler : IRequestHandler<AddTodoQuery, AddTodoResponse>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITodoDetailRepository _todoDetailRepository;
        private readonly RedisService<Todo> _redisService;

        public AddTodoHandler(ITodoRepository todoRepository, ITodoDetailRepository todoDetailRepository, RedisService<Todo> redisService)
        {
            _todoRepository =    todoRepository;
            _todoDetailRepository = todoDetailRepository;
            _redisService = redisService;
        }

        public async Task<AddTodoResponse> Handle(AddTodoQuery command, CancellationToken cancellationToken)
        {
            // Create new Todo
            var newTodo = new Todo
            {   
                TodoId = Guid.NewGuid(),
                Day = command.Day,
                TodayDate = DateTime.Now,
                Note = command.Note,
                DetailCount = 0
            };

            _todoRepository.Add(newTodo);

            //Create TodoDetails in bulk
           //var todoDetails = command.TodoDetails.Select(detail => new TodoDetail
           //{
           //    TodoDetailId = Guid.NewGuid(),
           //    Activity = detail.Activity,
           //    Category = Enum.Parse<CategoryEnum>(detail.Category),
           //    DetailNote = detail.DetailNote,
           //    TodoId = newTodo.TodoId
           //}).ToList();

            //_todoRepository.Add(newTodo);
            //_todoDetailRepository.BulkInsert(todoDetails);

            // Save to Redis
            string redisKey = $"Todo:{newTodo.TodoId}";
            await _redisService.CreateDataAsync(redisKey, _todoRepository.GetById(id: newTodo.TodoId));

            // Prepare Response
            var response = new AddTodoResponse
            {
                TodoId = newTodo.TodoId,
                Day = newTodo.Day,
                TodayDate = newTodo.TodayDate,
                Note = newTodo.Note,
                DetailCount = newTodo.DetailCount
            };

            return response;
        }
    }
}
