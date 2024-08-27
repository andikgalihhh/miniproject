using System.Collections.Generic;

namespace Core.Features.Queries.GetAllTodo
{
    public class GetAllTodoResponse
    {
        public List<TodoDto> Todos { get; set; }
        public int TotalCount { get; set; }
    }

    public class TodoDto
    {
        public Guid TodoId { get; set; }
        public string Day { get; set; }
        public DateTime TodayDate { get; set; }
        public string Note { get; set; }
        public int DetailCount { get; set; }
    }
}